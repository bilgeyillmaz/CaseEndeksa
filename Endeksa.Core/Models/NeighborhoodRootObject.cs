using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeksa.Core.Models
{
    [JsonObject("Crs")]
    public class NeighborhoodCrs
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("properties")]
        public NeighborhoodProperties Properties { get; set; }
    }

    [JsonObject("Feature")]
    public class NeighborhoodFeature
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("geometry")]
        public NeighborhoodGeometry Geometry { get; set; }
        [JsonProperty("properties")]
        public NeighborhoodProperties Properties { get; set; }
    }

    [JsonObject("Geometry")]
    public class NeighborhoodGeometry
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("coordinates")]
        public List<List<List<double>>> Coordinates { get; set; }
    }

    [JsonObject("Properties")]
    public class NeighborhoodProperties
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    [JsonObject("Root")]
    public class NeighborhoodRootObject
    {
        [JsonProperty("features")]    
        public List<NeighborhoodFeature> Features { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("crs")]
        public NeighborhoodCrs Crs { get; set; }
    }
}
