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
        public string type { get; set; }
        public NeighborhoodProperties properties { get; set; }
    }

    [JsonObject("Feature")]
    public class NeighborhoodFeature
    {
        public string type { get; set; }
        public NeighborhoodGeometry geometry { get; set; }
        public NeighborhoodProperties properties { get; set; }
    }

    [JsonObject("Geometry")]
    public class NeighborhoodGeometry
    {
        public string type { get; set; }
        public List<List<List<double>>> coordinates { get; set; }
    }

    [JsonObject("Properties")]
    public class NeighborhoodProperties
    {
        public string text { get; set; }
        public int id { get; set; }
        public string name { get; set; }
    }

    [JsonObject("Root")]
    public class NeighborhoodRootObject
    {
        public List<NeighborhoodFeature> features { get; set; }
        public string type { get; set; }
        public NeighborhoodCrs crs { get; set; }
    }
}
