using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeksa.Core.Models
{
    [NotMapped]
    public class Properties
    {
        //public int Id { get; set; }
        [JsonProperty("ilceAd")]
        public string llceAd { get; set; }
        [JsonProperty("mevkii")]
        public string Mevkii { get; set; }
        [JsonProperty("ilId")]
        public int IlId { get; set; }
        [JsonProperty("durum")]
        public string Durum { get; set; }
        [JsonProperty("parselId")]
        public int ParselId { get; set; }
        [JsonProperty("zeminKmdurum")]
        public string ZeminKmdurum { get; set; }
        [JsonProperty("zeminId")]
        public int ZeminId { get; set; }
        [JsonProperty("parselNo")]
        public string ParselNo { get; set; }
        [JsonProperty("nitelik")]
        public string Nitelik { get; set; }
        [JsonProperty("mahalleAd")]
        public string MahalleAd { get; set; }
        [JsonProperty("gittigiParselListe")]
        public string GittigiParselListe { get; set; }
        [JsonProperty("gittigiParselSebep")]
        public string GittigiParselSebep { get; set; }
        [JsonProperty("alan")]
        public string Alan { get; set; }
        [JsonProperty("adaNo")]
        public string AdaNo { get; set; }
        [JsonProperty("ilceId")]
        public int IlceId { get; set; }
        [JsonProperty("ilAd")]
        public string IlAd { get; set; }
        [JsonProperty("mahalleId")]
        public int MahalleId { get; set; }
        [JsonProperty("pafta")]
        public string Pafta { get; set; }
    }
}
