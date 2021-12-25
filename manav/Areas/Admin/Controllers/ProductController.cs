using manav.Areas.Admin.Models;
using manav.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace manav.Areas.Admin.Controllers
{
    [Area(nameof(Admin))]
    [Authorize(Roles = "Admin,ADMIN")]
    public class ProductController : Controller
    {
        private readonly dbContext _db;

        public ProductController(dbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var model = _db.Urunler.Include(x => x.Kategori).ToList();
            return View(model);
        }

        public IActionResult Duzenle(int id)
        {
            var urun = _db.Urunler.Where(x => x.UrunID == id).FirstOrDefault();
            UrunDuzenleDTO dto = new UrunDuzenleDTO()
            {
                UrunAd = urun.UrunAd,
                UrunAciklama = urun.UrunAciklama,
                UrunFiyat = urun.UrunFiyat,
                UrunResim = urun.UrunResim
            };
            ViewBag.id = urun.UrunID;
            return View(dto);
        }

        [HttpPost]
        public IActionResult Duzenle(UrunDuzenleDTO dto, int id)
        {
            var urun = _db.Urunler.Where(x => x.UrunID == id).FirstOrDefault();
            urun.UrunAd = dto.UrunAd;
            urun.UrunAciklama = dto.UrunAciklama;
            urun.UrunFiyat = dto.UrunFiyat;
            urun.UrunResim = dto.UrunResim;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Ekle()
        {
            ViewBag.kategoriler = _db.Kategoriler.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Ekle(UrunEkleDTO urun)
        {
            Urun ur = new Urun()
            {
                UrunAd = urun.UrunAdı,
                UrunFiyat = urun.UrunFiyat,
                UrunAciklama = urun.UrunAciklama,
                UrunResim = urun.UrunResim,
                Kategori = _db.Kategoriler.Where(x => x.KategoriID == (urun.Kategori)).FirstOrDefault()
            };
            _db.Urunler.Add(ur);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Sil(int id)
        {
            _db.Urunler.Remove(_db.Urunler.Find(id));
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
