using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Revenue_track.Model
{
    public class DatabaseSetting : IDatabaseSettings
    {
        public string CollectionName { get; set; }
        public string SecondCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
    public interface IDatabaseSettings 
    {
        string CollectionName { get; set; }
        public string SecondCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
