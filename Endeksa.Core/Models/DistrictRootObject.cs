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
        [JsonProperty("features")]
        public List<DistrictFeature> Features { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("crs")]
        public Crs Crs { get; set; }
    }

    [JsonObject("Feature")]
    public class DistrictFeature
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("geometry")]
        public object Geometry { get; set; }
        [JsonProperty("properties")]
        public LandProperties Properties { get; set; }
    }
}
