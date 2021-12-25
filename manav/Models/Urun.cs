using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace manav.Models
{
    public class Urun
    {
        public int UrunID { get; set; }
        public string UrunAciklama { get; set; }
        public string UrunAd { get; set; }
        public double UrunFiyat { get; set; }

        public string UrunResim { get; set; }
        public int KategoriID { get; set; }

        [ForeignKey(nameof(KategoriID))]
        public Kategori Kategori { get; set; }
    }
}
