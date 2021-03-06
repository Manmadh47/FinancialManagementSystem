using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revenue_track.Model
{
    public class Revenue
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("location")]
        public string location { get; set; }
        [BsonElement("city")]
        public string city { get; set; }

        [BsonElement("track")]
        public string track { get; set; }

        [BsonElement("project_id")]
        public string project_id { get; set; }

        [BsonElement("UID")]
        public string uid { get; set; }

        [BsonElement("employee_name")]
        public string employee_name { get; set; }

        [BsonElement("engagement_type")]
        public string engagement_type { get; set; }

        [BsonElement("employee_type")]
        public string employee_type { get; set; }

        [BsonElement("bill_rate")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal bill_rate { get; set; }

        [BsonElement("available_hours")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal available_hours { get; set; }

        [BsonElement("nb_hours")]
        public decimal nb_hours { get; set; }

        [BsonElement("nb_category")]
        public string nb_category { get; set; }

        [BsonElement("billed_hours")]
        public decimal billed_hours { get; set; }

        [BsonElement("total_revenue")]
        public decimal total_revenue { get; set; }

        [BsonElement("cost_rate")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal cost_rate { get; set; }
        [BsonElement("cost_hours")]
        public decimal cost_hours { get; set; }

        [BsonElement("total_expenses")]
        public decimal total_expenses { get; set; }

        [BsonElement("month")]
        public string month { get; set; }

        [BsonElement("year")]
        public string year { get; set; }

        [BsonElement("start_date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime start_date { get; set; }

        [BsonElement("end_date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime end_date { get; set; }

        [BsonElement("remarks")]
        public string remarks { get; set; }

    }
}
