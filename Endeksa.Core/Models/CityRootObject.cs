using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeksa.Core.Models
{
    public class CityRootObject
    {
        public List<CityFeature> features { get; set; }
        public string type { get; set; }
        public CityCrs crs { get; set; }
    }
    [JsonObject("Crs")]
    public class CityCrs
    {
        public string type { get; set; }
        public CityProperties properties { get; set; }
    }
    [JsonObject("Feature")]
    public class CityFeature
    {
        public string type { get; set; }
        public CityGeometry geometry { get; set; }
        public CityProperties properties { get; set; }
    }
    [JsonObject("Geometry")]
    public class CityGeometry
    {
        public string type { get; set; }
        public List<List<List<object>>> coordinates { get; set; }
    }
    [JsonObject("Properties")]
    public class CityProperties
    {
        public string text { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }
}
