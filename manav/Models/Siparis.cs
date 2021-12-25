using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace manav.Models
{
    public class Siparis
    {
        public int SiparisID { get; set; }
        public string SiparisDetayi { get; set; }
        public double SiparisTutari { get; set; }

        public AppUser Müsteri {get; set; }

        public string MüsteriAdresi { get; set; }

    }
}
