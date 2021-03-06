using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revenue_track.Model
{
    public class Demo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("project")]
        public Project project { get; set; }
        [BsonElement("location")]
        public string Location { get; set; }
        [BsonElement("city")]
        public string City { get; set; }
        [BsonElement("track")]
        public string Track { get; set; }
         [BsonElement("engagement_type")]
        public string EngagementType { get; set; }
        //[BsonElement("bill_rate")]
        //public decimal BillRate { get; set; }
        //[BsonElement("available_hours")]
        //public decimal AvailableHours { get; set; }
        [BsonElement("nb_hours")]
        public decimal NBHours { get; set; }
        [BsonElement("nb_category")]
        public string NBCategory { get; set; }
        //[BsonElement("billed_hours")]
        //public decimal BilledHours { get; set; }

        [BsonElement("total_revenue")]
        public decimal TotalRevenue { get; set; }
        //[BsonElement("cost_rate")]
        //public decimal CostRate { get; set; }

        //[BsonElement("cost_hour")]
        //public decimal CostHours { get; set; }

        [BsonElement("month")]
        public string Month { get; set; }
        //[BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        //public DateTime StartDate { get; set; }
        //[BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        //public DateTime EndDate { get; set; }
        [BsonElement("remarks")]
        public string Remarks { get; set; }
    }
}
