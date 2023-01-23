﻿using Newtonsoft.Json;
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
        public CityCrs Crs { get; set; }
    }
    [JsonObject("Crs")]
    public class CityCrs
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("properties")]
        public CityProperties Properties { get; set; }
    }
    [JsonObject("Feature")]
    public class CityFeature
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("geometry")]
        public CityGeometry Geometry { get; set; }
        [JsonProperty("properties")]
        public CityProperties Properties { get; set; }
    }
    [JsonObject("Geometry")]
    public class CityGeometry
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("coordinates")]
        public List<List<List<object>>> Coordinates { get; set; }
    }
    [JsonObject("Properties")]
    public class CityProperties
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
