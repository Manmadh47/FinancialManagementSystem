using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Revenue_track.Model
{
    public class Holidays
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("location")]
        public string location { get; set; }

        [BsonElement("country")]
        public string country { get; set; }

        [BsonElement("holiday_date")]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime holiday_date { get; set; }

        [BsonElement("year")]
        public string year { get; set; }
    }
}
