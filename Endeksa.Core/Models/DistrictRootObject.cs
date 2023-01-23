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
        public DistrictCrs Crs { get; set; }
    }
    [JsonObject("Crs")]
    public class DistrictCrs
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("properties")]
        public DistrictProperties Properties { get; set; }
    }
    [JsonObject("Feature")]
    public class DistrictFeature
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("geometry")]
        public object Geometry { get; set; }
        [JsonProperty("properties")]
        public DistrictProperties Properties { get; set; }
    }
    [JsonObject("Properties")]
    public class DistrictProperties
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
