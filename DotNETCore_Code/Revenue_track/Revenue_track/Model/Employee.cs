using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revenue_track.Model
{
    public class Employee
    {
       
       
        public List<Demo> demos { get; set; }
        public Employee()
        {
            demos = new List<Demo>();

        }
    }
}
