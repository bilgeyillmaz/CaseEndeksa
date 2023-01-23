using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeksa.Core.Models
{
    [JsonObject("Feature")]
    public class LandFeature
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
        [JsonProperty("properties")]
        public LandProperties Properties { get; set; }
    }
}
