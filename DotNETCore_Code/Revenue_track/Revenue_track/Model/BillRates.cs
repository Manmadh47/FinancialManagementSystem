using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Revenue_track.Model
{
    public class BillRates
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string id { get; set; }

        [BsonElement("bill_rate")]
        [BsonRepresentation(BsonType.Decimal128)]
        public decimal bill_rate { get; set; }
    }
}
