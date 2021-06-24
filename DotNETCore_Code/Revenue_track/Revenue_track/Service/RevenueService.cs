using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Revenue_track.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;

namespace Revenue_track.Service
{
    public class RevenueService
    {
        private readonly IMongoCollection<Revenue> _revenue;
        private IConfiguration config
        {
            get;
        }

        public RevenueService(IConfiguration configuration)
        {
            config = configuration;
            string connectionstring = config.GetValue<String>("Datasettings:ConnectionString");
            string databasename = config.GetValue<String>("Datasettings:DatabaseName");
            string collectionname = config.GetValue<String>("Datasettings:CollectionName");
            var client = new MongoClient(connectionstring);
            var database = client.GetDatabase(databasename);

            _revenue = database.GetCollection<Revenue>(collectionname);

        }
        Dictionary<string, int> defaultMonths = new Dictionary<string, int>()
        {
             {"January" , 1},{"February" , 2},{"March" , 3},{"April" , 4},{"May" , 5},{"June" , 6},{"July" , 7},
            {"August" , 8}, {"September" , 9}, {"October" , 10}, {"November" , 11}, {"December" , 12}
        };
        public List<Revenue> returnDuplicates = new List<Revenue>();

        public int uidMonth, monthValue, namesQueryMonth;
        public List<Revenue> Get() =>
            _revenue.Find(revenue => true).ToList();


        public IEnumerable<string> GetYear()
        {
            var years =(from c in _revenue.AsQueryable<Revenue>()
                 select c.year)
                .Distinct();
            return years.ToList();
        }
        public IEnumerable<string> GetMonth(string year_param)
        {
            var months =
                (from c in _revenue.AsQueryable<Revenue>()
                 where c.year == year_param
                 select c.month)
                .Distinct();
            return months.ToList();
        }

        public IEnumerable<Revenue> GetByYearAndMonth(string year_param,string month_param)
        {
            var queryByYear = from e in _revenue.AsQueryable<Revenue>()
                        where e.year == year_param && e.month.ToLower()==month_param.ToLower()
                              select e;
            return queryByYear.ToList();
        }
        public IEnumerable<Revenue> GetByLatestRecords()
        {
            Dictionary<string,int> monthValues = new Dictionary<string,int>();
            
             var years =(from c in _revenue.AsQueryable<Revenue>()
                 select c.year)
                 .Distinct().ToList();

            int maxYear = years.ConvertAll(x => int.Parse(x)).Max();

            var months = (from e in _revenue.AsQueryable<Revenue>()
                              where e.year == maxYear.ToString()
                              select e.month).Distinct().ToList();
           foreach(var month in months)
            {
                foreach(var defMonth in defaultMonths)
                {
                    if (month.ToLower() == defMonth.Key.ToLower())
                    {
                        monthValues.Add(defMonth.Key, defMonth.Value);
                    }
                }
            }
            string maxMonth = monthValues.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
            
            var queryByDate = from e in _revenue.AsQueryable<Revenue>()
                             where e.year == maxYear.ToString() &&  e.month.ToLower()==maxMonth.ToLower()
                              select e;
            return queryByDate;
        }

        public Revenue Get(string id) =>
            _revenue.Find<Revenue>(revenue => revenue.id == id).FirstOrDefault();

        public List<Revenue> Create(Revenue revenue)
        {

            var uidQuery= (from e in _revenue.AsQueryable<Revenue>()
                      where e.uid.ToLower() == revenue.uid.ToString().ToLower()
                      select e).ToList().OrderByDescending(d=>d.end_date).FirstOrDefault();

            var namesQuery = (from e in _revenue.AsQueryable<Revenue>()
                        where e.employee_name.ToLower()
                        == revenue.employee_name.ToLower()
                        select e).ToList().OrderByDescending(d => d.end_date).FirstOrDefault();

            if (uidQuery == null && namesQuery == null)
            {
                _revenue.InsertOne(revenue);
            }
            else
            {
                if (uidQuery != null)
                {
                    if (revenue.employee_name.Replace(" ", string.Empty).ToLower() == uidQuery.employee_name.Replace(" ", string.Empty).ToLower())
                    {
                        if (revenue.start_date > uidQuery.end_date)
                        {
                                _revenue.InsertOne(revenue);
                        }
                        else
                        {
                            returnDuplicates.Add(revenue);
                        }
                    }
                    else {
                        var n = 0;
                        n++;
                        returnDuplicates.Add(revenue); }
                }
                else if (uidQuery == null)
                {
                    if (revenue.uid.Replace(" ", string.Empty).ToLower() == namesQuery.uid.Replace(" ", string.Empty).ToLower())
                    {
                        if (revenue.start_date > namesQuery.end_date)
                        {
                            _revenue.InsertOne(revenue);
                        }
                        else
                        {
                            returnDuplicates.Add(revenue);
                        }
                    }
                    else { var n = 0; 
                        n++; 
                        returnDuplicates.Add(revenue); }
                }
             }
            return returnDuplicates;
        }

        public List<Revenue> CreateForecast(Revenue revenue)
        {
            var uidQuery = (from e in _revenue.AsQueryable<Revenue>()
                            where e.uid.ToLower() == revenue.uid.ToString().ToLower()
                            select e).ToList().OrderByDescending(d => d.end_date).FirstOrDefault();

            var namesQuery = (from e in _revenue.AsQueryable<Revenue>()
                              where e.employee_name.ToLower()
                              == revenue.employee_name.ToLower()
                              select e).ToList().OrderByDescending(d => d.end_date).FirstOrDefault();

            foreach (var defMonth in defaultMonths)
            {
                if (revenue.month.ToLower() == defMonth.Key.ToLower())
                {
                    monthValue = defMonth.Value;
                }
            }

            if (uidQuery == null && namesQuery == null)
            {
                _revenue.InsertOne(revenue);
            }
            else
            {
                if (uidQuery != null)
                {
                    foreach (var defMonth in defaultMonths)
                    {
                        if (uidQuery.month.ToLower() == defMonth.Key.ToLower())
                        {
                            uidMonth = defMonth.Value;
                        }
                    }

                    if (int.Parse(revenue.year) >= int.Parse(uidQuery.year) && monthValue > uidMonth)
                    {
                        _revenue.InsertOne(revenue);
                    }
                    else
                    {
                        returnDuplicates.Add(revenue);
                    }
                }
                else if (uidQuery == null)
                {
                    foreach (var defMonth in defaultMonths)
                    {
                        if (namesQuery.month.ToLower() == defMonth.Key.ToLower())
                        {
                            namesQueryMonth = defMonth.Value;
                        }
                    }
                    if (int.Parse(revenue.year) >= int.Parse(namesQuery.year) && monthValue > namesQueryMonth)
                    {
                        _revenue.InsertOne(revenue);
                    }
                    else
                    {
                        returnDuplicates.Add(revenue);
                    }
                }
            }
        return returnDuplicates;
     }

         public void Update(string id, Revenue revenueIn) =>
            _revenue.ReplaceOne(revenue => revenue.id == id, revenueIn);

        public void Remove(Revenue revenueIn) =>
            _revenue.DeleteOne(revenue => revenue.id == revenueIn.id);

        public void Remove(string id) =>
            _revenue.DeleteOne(revenue => revenue.id == id);

       
    }
}

