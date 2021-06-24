using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Revenue_track.Model;

namespace Revenue_track.Service
{
    public class BillRatesService
    {
        private readonly IMongoCollection<BillRates> billRateDb; 
        
        private IConfiguration config
        {
            get;
        }

        public BillRatesService(IConfiguration configuration)
        {
            config = configuration;
            string connectionstring = config.GetValue<String>("Datasettings:ConnectionString");
            string databasename = config.GetValue<String>("Datasettings:DatabaseName");
            string thirdCollectionName = config.GetValue<String>("Datasettings:ThirdCollectionName");
            var client = new MongoClient(connectionstring);
            var database = client.GetDatabase(databasename);

            billRateDb = database.GetCollection<BillRates>(thirdCollectionName);
        }
        public List<BillRates> Get() =>
           billRateDb.Find(bill => true).ToList();

        public BillRates Get(string id) =>
            billRateDb.Find<BillRates>(billFun => billFun.id == id).FirstOrDefault();

        public BillRates Create(BillRates billParam)
        {
            billRateDb.InsertOne(billParam);
            return billParam;
        }

        public void Update(string id, BillRates billRow) =>
            billRateDb.ReplaceOne(billFun => billFun.id == id, billRow);

        public void Remove(BillRates billRow) =>
            billRateDb.DeleteOne(billFun => billFun.id == billRow.id);

        public void Remove(string id) =>
            billRateDb.DeleteOne(billFun => billFun.id == id);
    }
}
