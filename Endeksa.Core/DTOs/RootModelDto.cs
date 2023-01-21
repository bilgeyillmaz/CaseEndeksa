using Endeksa.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endeksa.Core.DTOs
{
    public class RootModelDto
    {
        public string Type { get; set; }
        public string GeometryType { get; set; }
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
