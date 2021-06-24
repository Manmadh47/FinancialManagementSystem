using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Revenue_track.Model;

namespace Revenue_track.Service
{
    public class HolidaysService
    {
        private readonly IMongoCollection<Holidays> holidays;
        private IConfiguration config
        {
            get;
        }
        public HolidaysService(IConfiguration configuration)
        {
            config = configuration;
            string connectionstring = config.GetValue<String>("Datasettings:ConnectionString");
            string databasename = config.GetValue<String>("Datasettings:DatabaseName");
            string secondCollectionName = config.GetValue<String>("Datasettings:SecondCollectionName");
            var client = new MongoClient(connectionstring);
            var database = client.GetDatabase(databasename);

            holidays = database.GetCollection<Holidays>(secondCollectionName);
        }

        public List<Holidays> Get() =>
           holidays.Find(holidays => true).ToList();

        public Holidays Get(string id) =>
            holidays.Find<Holidays>(holidayFun => holidayFun.id == id).FirstOrDefault();

        public Holidays Create(Holidays holiday)
        {
            holidays.InsertOne(holiday);
            return holiday;
        }

        public void Update(string id, Holidays holiday) =>
            holidays.ReplaceOne(holidayFn => holidayFn.id == id, holiday);

        public void Remove(Holidays holiday) =>
            holidays.DeleteOne(holidayFn => holidayFn.id == holiday.id);

        public void Remove(string id) =>
            holidays.DeleteOne(holidayFn => holidayFn.id == id);

    }

}
