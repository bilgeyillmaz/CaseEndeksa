using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Endeksa.Core.Models
{
    public class Root:BaseEntity
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("geometry")]
        [NotMapped]
        public Geometry Geometry { get; set; }

        [JsonProperty("properties")]
        [NotMapped]
        public Properties Properties { get; set; }
        //public int GeometryId { get; set; }
        //public int PropertiesId { get; set; }
        public string GeometryType { get; set; }
        //public List<List<List<double>>> Coordinates { get; set; }
        public string Coordinates { get; set; } 
        public string IlceAd { get; set; }
        public string Mevkii { get; set; }
        public int IlId { get; set; }
        public string Durum { get; set; }
        public int ParselId { get; set; }
        public string ZeminKmdurum { get; set; }
        public int ZeminId { get; set; }
        public string ParselNo { get; set; }
        public string Nitelik { get; set; }
        public string MahalleAd { get; set; }
        public string GittigiParselListe { get; set; }
        public string GittigiParselSebep { get; set; }
        public string Alan { get; set; }
        public string AdaNo { get; set; }
        public int IlceId { get; set; }
        public string IlAd { get; set; }
        public int MahalleId { get; set; }
        public string Pafta { get; set; }
    }
}
