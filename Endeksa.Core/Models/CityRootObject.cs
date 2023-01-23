using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeksa.Core.Models
{
    [JsonObject("Root")]
    public class CityRootObject
    {
        [JsonProperty("features")]
        public List<CityFeature> Features { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("crs")]
        public Crs Crs { get; set; }
    }
    [JsonObject("Feature")]
    public class CityFeature
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("geometry")]
        public CityGeometry Geometry { get; set; }
        [JsonProperty("properties")]

        public LandProperties Properties { get; set; }
    }
    [JsonObject("Geometry")]
    public class CityGeometry
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("coordinates")]
        public List<List<List<object>>> Coordinates { get; set; }
    }
}
