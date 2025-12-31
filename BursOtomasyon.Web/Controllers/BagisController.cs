using Microsoft.AspNetCore.Mvc;
using BursOtomasyon.Web.Models;
using BursOtomasyon.Web.Services;

namespace BursOtomasyon.Web.Controllers
{
    public class BagisController : Controller
    {
        private readonly BagisciService _bagisciService;
        private readonly BagisService _bagisService;

        public BagisController()
        {
            _bagisciService = new BagisciService();
            _bagisService = new BagisService();
        }

        // Bağışçı Kayıt Sayfası
        public IActionResult Kayit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Kayit(BagisciModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var bagisciId = _bagisciService.Add(model);
                if (bagisciId > 0)
                {
                    ViewBag.Success = true;
                    ViewBag.Message = "Kayıt başarılı! Artık bağış yapabilirsiniz.";
                    return View();
                }
                else
                {
                    ViewBag.Error = "Kayıt başarısız. Lütfen tekrar deneyin.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Bir hata oluştu: {ex.Message}";
            }

            return View(model);
        }

        // Bağışçı Giriş Sayfası
        public IActionResult Giris()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Giris(BagisciGirisViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var bagisci = _bagisciService.GirisYap(model.Email, model.Sifre);
                if (bagisci != null)
                {
                    HttpContext.Session.SetInt32("BagisciID", bagisci.BagisciID);
                    HttpContext.Session.SetString("BagisciAdi", $"{bagisci.Ad} {bagisci.Soyad}");
                    return RedirectToAction("BagisYap");
                }
                else
                {
                    ViewBag.Error = "E-posta veya şifre hatalı!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Bir hata oluştu: {ex.Message}";
            }

            return View(model);
        }

        // Bağış Yapma Sayfası (Giriş gerektirmez, herkes bağış yapabilir)
        public IActionResult BagisYap()
        {
            return View();
        }

        [HttpPost]
        public IActionResult BagisYap(BagisViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Önce bağışçıyı kontrol et veya oluştur
                var bagisci = _bagisciService.GetByEmail(model.Email);
                int bagisciId;

                if (bagisci == null)
                {
                    // Yeni bağışçı oluştur
                    var yeniBagisci = new BagisciModel
                    {
                        Ad = model.Ad,
                        Soyad = model.Soyad,
                        Email = model.Email,
                        Telefon = model.Telefon,
                        Sifre = Guid.NewGuid().ToString().Substring(0, 8) // Geçici şifre
                    };
                    bagisciId = _bagisciService.Add(yeniBagisci);
                }
                else
                {
                    bagisciId = bagisci.BagisciID;
                }

                // Bağışı kaydet
                var bagisId = _bagisService.Add(bagisciId, model.Tutar, model.Aciklama);
                if (bagisId > 0)
                {
                    ViewBag.Success = true;
                    ViewBag.Message = $"Bağışınız başarıyla kaydedildi! Bağış tutarınız: {model.Tutar:N2} TL. IBAN numaranıza göre ödeme yapabilirsiniz.";
                    // Formu temizle
                    model = new BagisViewModel();
                }
                else
                {
                    ViewBag.Error = "Bağış kaydedilemedi. Lütfen tekrar deneyin.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Bir hata oluştu: {ex.Message}";
            }

            return View(model);
        }

        // Çıkış
        public IActionResult Cikis()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Giris");
        }
    }
}

