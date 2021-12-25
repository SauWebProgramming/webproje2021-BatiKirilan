using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace manav.Areas.Admin.Models
{
    public class UrunEkleDTO
    {
        public string UrunAdı { get; set; }
        public string UrunAciklama { get; set; }
        public double UrunFiyat { get; set; }
        public string UrunResim { get; set; }
        public int Kategori { get; set; }
    }
}
