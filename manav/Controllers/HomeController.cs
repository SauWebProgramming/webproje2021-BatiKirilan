using manav.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace manav.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly dbContext _db;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, dbContext db, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _db = db;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var urunler = _db.Urunler.OrderBy(x => x.UrunAd).ToList();
            ViewBag.cart = new List<string>();
            double totalprice = 0;
            if (HttpContext.Session.GetString("Cart") != null)
            {
                var cart = HttpContext.Session.GetString("Cart");
                var sepet = JsonSerializer.Deserialize<List<SepetUrun>>(cart);
                ViewBag.cart = sepet;
                foreach (var item in sepet)
                {
                    totalprice += item.Fiyat;
                }
                ViewBag.toplam = totalprice;
            }
            
            return View(urunler);
        }
        
        public IActionResult SepeteEkle(int id)
        {
            List<SepetUrun> cart = new List<SepetUrun>();
            SepetUrun sepet = new SepetUrun();
            if (HttpContext.Session.GetString("Cart") != null)
            {
                var eskisepet = HttpContext.Session.GetString("Cart");
                var oldcart = JsonSerializer.Deserialize<List<SepetUrun>>(eskisepet);
                cart.AddRange(oldcart);
            }
            var urun = _db.Urunler.Where(x => x.UrunID == id).FirstOrDefault();
            sepet.Ad = urun.UrunAd;
            sepet.Fiyat =urun.UrunFiyat;
            cart.Add(sepet);
            HttpContext.Session.SetString("Cart", JsonSerializer.Serialize(cart));
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public IActionResult CultureManagement(string culture, string returnUrl)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName, CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.Now.AddDays(30) });
            return LocalRedirect(returnUrl);
        }

        public IActionResult Sepetibosalt()
        {
            HttpContext.Session.Remove("Cart");
            return RedirectToAction("Index");
        }
        
        public IActionResult Sepetionayla()
        {
            if (HttpContext.Session.GetString("Cart") != null)
            {
                var sepet = HttpContext.Session.GetString("Cart");
                var sepeturun = JsonSerializer.Deserialize<List<SepetUrun>>(sepet);
                double toplam = 0;
                foreach (var item in sepeturun)
                {
                    toplam += item.Fiyat;
                }
                ViewBag.cart = sepeturun;
                ViewBag.toplam = toplam;
                return View();
            }
            return RedirectToAction("Index");
        }
        
        [HttpPost]
        public IActionResult Sepetionayla(string adres)
        {
            if (HttpContext.Session.GetString("Cart") != null)
            {
                var sepet = HttpContext.Session.GetString("Cart");
                var sepeturun = JsonSerializer.Deserialize<List<SepetUrun>>(sepet);
                double toplam = 0;
                var aciklama ="";
                foreach (var item in sepeturun)
                {
                    aciklama += item.Ad+" ";
                    toplam += item.Fiyat;
                }
                Siparis siparis = new Siparis()
                {
                    Müsteri = _db.Users.Where(x=>x.UserName == User.Identity.Name).FirstOrDefault(),
                    MüsteriAdresi = adres,
                    SiparisDetayi = aciklama,
                    SiparisTutari = toplam
                };
                _db.Siparisler.Add(siparis);
                _db.SaveChanges();
                HttpContext.Session.Remove("Cart");
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
