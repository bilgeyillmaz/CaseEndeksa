using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeksa.Core.Models
{
    [JsonObject("Root")]
    public class DistrictRootObject
    {
        public List<DistrictFeature> features { get; set; }
        public string type { get; set; }
        public DistrictCrs crs { get; set; }
    }
    [JsonObject("Crs")]
    public class DistrictCrs
    {
        public string type { get; set; }
        public DistrictProperties properties { get; set; }
    }
    [JsonObject("Feature")]
    public class DistrictFeature
    {
        public string type { get; set; }
        public object geometry { get; set; }
        public DistrictProperties properties { get; set; }
    }
    [JsonObject("Properties")]
    public class DistrictProperties
    {
        public string text { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }
}
