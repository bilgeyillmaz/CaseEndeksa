using Endeksa.Core.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeksa.Core.DTOs
{
    public class RootDto
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
        [JsonProperty("properties")]
        public Properties Properties { get; set; }
    }
}
