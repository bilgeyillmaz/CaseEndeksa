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
        public string type { get; set; }
        public string geometryType { get; set; }
        public string coordinates { get; set; }
        public string ilceAd { get; set; }
        public string mevkii { get; set; }
        public int ilId { get; set; }
        public string durum { get; set; }
        public int parselId { get; set; }
        public string zeminKmdurum { get; set; }
        public int zeminId { get; set; }
        public string parselNo { get; set; }
        public string nitelik { get; set; }
        public string mahalleAd { get; set; }
        public string gittigiParselListe { get; set; }
        public string gittigiParselSebep { get; set; }
        public string alan { get; set; }
        public string adaNo { get; set; }
        public int ilceId { get; set; }
        public string ilAd { get; set; }
        public int mahalleId { get; set; }
        public string pafta { get; set; }
    }
}
