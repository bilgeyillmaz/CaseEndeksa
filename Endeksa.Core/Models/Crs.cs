using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeksa.Core.Models
{
    [JsonObject("Crs")]
    public class Crs
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("properties")]
        public LandProperties Properties { get; set; }
    }
}
