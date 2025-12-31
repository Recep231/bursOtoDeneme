using Microsoft.AspNetCore.Mvc;
using BursOtomasyon.Web.Models;
using BursOtomasyon.Web.Services;

namespace BursOtomasyon.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly BasvuruService _basvuruService;
        private readonly OgrenciService _ogrenciService;
        private readonly BursPuanlamaService _puanlamaService;

        public HomeController()
        {
            _basvuruService = new BasvuruService();
            _ogrenciService = new OgrenciService();
            _puanlamaService = new BursPuanlamaService();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BasvuruYap()
        {
            // Üniversite listesini yükle
            var universiteList = GetUniversiteListesi();
            ViewBag.UniversiteListesi = universiteList;
            
            // Fakülte listesini yükle
            ViewBag.FakulteListesi = GetFakulteListesi();
            
            // Bölüm listesini yükle
            ViewBag.BolumListesi = GetBolumListesi();
            
            return View();
        }

        private List<string> GetUniversiteListesi()
        {
            // Genişletilmiş üniversite listesi
            return new List<string>
            {
                // İstanbul
                "İstanbul Üniversitesi",
                "İstanbul Teknik Üniversitesi",
                "Boğaziçi Üniversitesi",
                "Galatasaray Üniversitesi",
                "Sabancı Üniversitesi",
                "Koç Üniversitesi",
                "Yeditepe Üniversitesi",
                "Bahçeşehir Üniversitesi",
                "Marmara Üniversitesi",
                "Yıldız Teknik Üniversitesi",
                "İstanbul Medeniyet Üniversitesi",
                "İstanbul Kültür Üniversitesi",
                "İstanbul Aydın Üniversitesi",
                "İstanbul Bilgi Üniversitesi",
                "İstanbul Okan Üniversitesi",
                "İstanbul Şehir Üniversitesi",
                
                // Ankara
                "Ankara Üniversitesi",
                "Orta Doğu Teknik Üniversitesi",
                "Hacettepe Üniversitesi",
                "Gazi Üniversitesi",
                "Bilkent Üniversitesi",
                "Başkent Üniversitesi",
                "Ankara Hacı Bayram Veli Üniversitesi",
                "Ankara Yıldırım Beyazıt Üniversitesi",
                "Atılım Üniversitesi",
                "Çankaya Üniversitesi",
                
                // İzmir
                "Ege Üniversitesi",
                "Dokuz Eylül Üniversitesi",
                "İzmir Yüksek Teknoloji Enstitüsü",
                "İzmir Katip Çelebi Üniversitesi",
                "İzmir Demokrasi Üniversitesi",
                "Yaşar Üniversitesi",
                "İzmir Ekonomi Üniversitesi",
                
                // Diğer Büyük Şehirler
                "Çukurova Üniversitesi",
                "Akdeniz Üniversitesi",
                "Selçuk Üniversitesi",
                "Ondokuz Mayıs Üniversitesi",
                "Karadeniz Teknik Üniversitesi",
                "Dicle Üniversitesi",
                "Gaziantep Üniversitesi",
                "Pamukkale Üniversitesi",
                "Erciyes Üniversitesi",
                "Uludağ Üniversitesi",
                "Süleyman Demirel Üniversitesi",
                "Mersin Üniversitesi",
                "Kocaeli Üniversitesi",
                "Sakarya Üniversitesi",
                "Eskişehir Osmangazi Üniversitesi",
                "Anadolu Üniversitesi",
                "Eskişehir Teknik Üniversitesi",
                "Çanakkale Onsekiz Mart Üniversitesi",
                "Muğla Sıtkı Koçman Üniversitesi",
                "Balıkesir Üniversitesi",
                "Trakya Üniversitesi",
                
                // Doğu Anadolu
                "Fırat Üniversitesi",
                "Atatürk Üniversitesi",
                "Malatya İnönü Üniversitesi",
                "Kahramanmaraş Sütçü İmam Üniversitesi",
                "Van Yüzüncü Yıl Üniversitesi",
                "Ağrı İbrahim Çeçen Üniversitesi",
                "Bingöl Üniversitesi",
                "Bitlis Eren Üniversitesi",
                "Muş Alparslan Üniversitesi",
                "Erzincan Binali Yıldırım Üniversitesi",
                "Kars Kafkas Üniversitesi",
                "Iğdır Üniversitesi",
                "Ardahan Üniversitesi",
                
                // Güneydoğu Anadolu
                "Şanlıurfa Harran Üniversitesi",
                "Mardin Artuklu Üniversitesi",
                "Batman Üniversitesi",
                "Siirt Üniversitesi",
                "Şırnak Üniversitesi",
                "Hakkari Üniversitesi",
                "Adıyaman Üniversitesi",
                "Osmaniye Korkut Ata Üniversitesi",
                "Hatay Mustafa Kemal Üniversitesi",
                
                // Karadeniz
                "Artvin Çoruh Üniversitesi",
                "Rize Recep Tayyip Erdoğan Üniversitesi",
                "Giresun Üniversitesi",
                "Ordu Üniversitesi",
                "Gümüşhane Üniversitesi",
                "Bayburt Üniversitesi",
                "Kastamonu Üniversitesi",
                "Zonguldak Bülent Ecevit Üniversitesi",
                "Bartın Üniversitesi",
                "Sinop Üniversitesi",
                "Amasya Üniversitesi",
                "Tokat Gaziosmanpaşa Üniversitesi",
                "Sivas Cumhuriyet Üniversitesi",
                "Çorum Hitit Üniversitesi",
                
                // İç Anadolu
                "Kırıkkale Üniversitesi",
                "Afyon Kocatepe Üniversitesi",
                "Kütahya Dumlupınar Üniversitesi",
                "Nevşehir Hacı Bektaş Veli Üniversitesi",
                "Aksaray Üniversitesi",
                "Niğde Ömer Halisdemir Üniversitesi",
                "Yozgat Bozok Üniversitesi",
                "Kırşehir Ahi Evran Üniversitesi",
                "Bolu Abant İzzet Baysal Üniversitesi",
                "Düzce Üniversitesi",
                "Karabük Üniversitesi",
                "Çankırı Karatekin Üniversitesi",
                
                // Ege ve Akdeniz
                "Aydın Adnan Menderes Üniversitesi",
                "Manisa Celal Bayar Üniversitesi",
                "Uşak Üniversitesi",
                "Burdur Mehmet Akif Ersoy Üniversitesi",
                "Isparta Uygulamalı Bilimler Üniversitesi",
                "Bilecik Şeyh Edebali Üniversitesi",
                "Yalova Üniversitesi",
                "Tekirdağ Namık Kemal Üniversitesi",
                "Kırklareli Üniversitesi",
                "Tunceli Üniversitesi"
            }.OrderBy(u => u).ToList();
        }

        private List<string> GetFakulteListesi()
        {
            return new List<string>
            {
                "Tıp Fakültesi",
                "Mühendislik Fakültesi",
                "Fen-Edebiyat Fakültesi",
                "İktisadi ve İdari Bilimler Fakültesi",
                "Eğitim Fakültesi",
                "Hukuk Fakültesi",
                "İletişim Fakültesi",
                "Güzel Sanatlar Fakültesi",
                "Spor Bilimleri Fakültesi",
                "Ziraat Fakültesi",
                "Orman Fakültesi",
                "Su Ürünleri Fakültesi",
                "Veteriner Fakültesi",
                "Eczacılık Fakültesi",
                "Diş Hekimliği Fakültesi",
                "Mimarlık Fakültesi",
                "İlahiyat Fakültesi",
                "Sağlık Bilimleri Fakültesi",
                "Teknoloji Fakültesi",
                "Uygulamalı Bilimler Fakültesi"
            }.OrderBy(f => f).ToList();
        }

        private List<string> GetBolumListesi()
        {
            return new List<string>
            {
                // Tıp ve Sağlık
                "Tıp",
                "Diş Hekimliği",
                "Eczacılık",
                "Veteriner Hekimliği",
                "Hemşirelik",
                "Fizyoterapi ve Rehabilitasyon",
                "Beslenme ve Diyetetik",
                "Ebelik",
                "Sağlık Yönetimi",
                
                // Mühendislik
                "Bilgisayar Mühendisliği",
                "Yazılım Mühendisliği",
                "Elektrik-Elektronik Mühendisliği",
                "Elektrik Mühendisliği",
                "Elektronik Mühendisliği",
                "Makine Mühendisliği",
                "Endüstri Mühendisliği",
                "İnşaat Mühendisliği",
                "Kimya Mühendisliği",
                "Gıda Mühendisliği",
                "Çevre Mühendisliği",
                "Petrol ve Doğalgaz Mühendisliği",
                "Maden Mühendisliği",
                "Jeoloji Mühendisliği",
                "Jeofizik Mühendisliği",
                "Harita Mühendisliği",
                "Mimarlık",
                "Şehir ve Bölge Planlama",
                "Endüstriyel Tasarım",
                "Otomotiv Mühendisliği",
                "Uçak Mühendisliği",
                "Havacılık Mühendisliği",
                "Uzay Mühendisliği",
                "Mekatronik Mühendisliği",
                "Biyomedikal Mühendisliği",
                "Nanoteknoloji Mühendisliği",
                "Enerji Sistemleri Mühendisliği",
                
                // Fen-Edebiyat
                "Matematik",
                "Fizik",
                "Kimya",
                "Biyoloji",
                "Moleküler Biyoloji ve Genetik",
                "Biyokimya",
                "İstatistik",
                "Aktüerya",
                "Astronomi ve Uzay Bilimleri",
                "Coğrafya",
                "Tarih",
                "Türk Dili ve Edebiyatı",
                "Felsefe",
                "Sosyoloji",
                "Psikoloji",
                "Antropoloji",
                "Arkeoloji",
                "Sanat Tarihi",
                "Dilbilim",
                "Çeviribilim",
                "Mütercim Tercümanlık",
                
                // İİBF
                "İktisat",
                "İşletme",
                "Maliye",
                "Muhasebe",
                "Muhasebe ve Finansman",
                "Finans",
                "Bankacılık",
                "Bankacılık ve Finans",
                "Sigortacılık",
                "Uluslararası İlişkiler",
                "Siyaset Bilimi",
                "Siyaset Bilimi ve Kamu Yönetimi",
                "Kamu Yönetimi",
                "Çalışma Ekonomisi ve Endüstri İlişkileri",
                "Ekonometri",
                "Yönetim Bilişim Sistemleri",
                "İnsan Kaynakları Yönetimi",
                "Lojistik Yönetimi",
                "Turizm İşletmeciliği",
                "Otel İşletmeciliği",
                
                // Eğitim
                "Sınıf Öğretmenliği",
                "Okul Öncesi Öğretmenliği",
                "Rehberlik ve Psikolojik Danışmanlık",
                "Türkçe Öğretmenliği",
                "Matematik Öğretmenliği",
                "Fen Bilgisi Öğretmenliği",
                "Sosyal Bilgiler Öğretmenliği",
                "Tarih Öğretmenliği",
                "Coğrafya Öğretmenliği",
                "İngilizce Öğretmenliği",
                "Almanca Öğretmenliği",
                "Fransızca Öğretmenliği",
                "Beden Eğitimi ve Spor Öğretmenliği",
                "Müzik Öğretmenliği",
                "Resim Öğretmenliği",
                "Görsel Sanatlar Öğretmenliği",
                "Bilgisayar ve Öğretim Teknolojileri Öğretmenliği",
                
                // Hukuk
                "Hukuk",
                
                // İletişim
                "Gazetecilik",
                "Radyo, Televizyon ve Sinema",
                "Halkla İlişkiler",
                "Halkla İlişkiler ve Tanıtım",
                "Reklamcılık",
                
                // Güzel Sanatlar
                "Resim",
                "Heykel",
                "Seramik",
                "Grafik Tasarım",
                "Müzik",
                "Sahne Sanatları",
                "Tiyatro",
                "Sinema ve Televizyon",
                
                // Spor
                "Antrenörlük",
                "Beden Eğitimi ve Spor",
                "Rekreasyon",
                
                // Ziraat
                "Bitkisel Üretim",
                "Hayvansal Üretim",
                "Tarım Ekonomisi",
                "Tarımsal Yapılar",
                "Toprak Bilimi",
                "Bahçe Bitkileri",
                "Tarla Bitkileri",
                
                // Orman
                "Orman Mühendisliği",
                "Orman Endüstrisi Mühendisliği",
                
                // Su Ürünleri
                "Su Ürünleri Mühendisliği"
            }.OrderBy(b => b).ToList();
        }

        [HttpPost]
        public IActionResult BasvuruYap(BasvuruViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            try
            {
                // Profil fotoğrafını diske kaydet (varsa)
                string? profilFotoYolu = null;
                if (model.ProfilFoto != null && model.ProfilFoto.Length > 0)
                {
                    var uploadsRoot = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profile-photos");
                    if (!Directory.Exists(uploadsRoot))
                    {
                        Directory.CreateDirectory(uploadsRoot);
                    }

                    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(model.ProfilFoto.FileName)}";
                    var fullPath = Path.Combine(uploadsRoot, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        model.ProfilFoto.CopyTo(stream);
                    }

                    profilFotoYolu = "/profile-photos/" + fileName;
                }

                var ogrenci = new OgrenciModel
                {
                    Ad = model.Ad,
                    Soyad = model.Soyad,
                    TC = model.TC,
                    DogumTarihi = model.DogumTarihi,
                    Telefon = model.Telefon,
                    Email = model.Email,
                    Universite = model.Universite,
                    Fakulte = model.Fakulte,
                    Bolum = model.Bolum,
                    Sinif = model.Sinif,
                    NotOrtalamasi = model.NotOrtalamasi,
                    KardesSayisi = model.KardesSayisi,
                    AileGeliri = model.AileGeliri,
                    AnneMeslek = model.AnneMeslek,
                    BabaMeslek = model.BabaMeslek,
                    ProfilFotoYolu = profilFotoYolu,
                    KlasikSoru1Cevap = model.KlasikSoru1Cevap,
                    KlasikSoru2Cevap = model.KlasikSoru2Cevap,
                    KlasikSoru3Cevap = model.KlasikSoru3Cevap
                };

                // Burs puanını hesapla
                ogrenci.BursPuani = _puanlamaService.HesaplaBursPuani(ogrenci);

                // Öğrenciyi kaydet
                var ogrenciId = _ogrenciService.Add(ogrenci);

                if (ogrenciId > 0)
                {
                    // Başvuruyu oluştur
                    try
                    {
                        var basvuruEklendi = _basvuruService.Add(ogrenciId);
                        
                        if (basvuruEklendi)
                        {
                            ViewBag.Success = true;
                            ViewBag.Message = "Başvurunuz başarıyla alındı! Başvurunuz değerlendirme sürecine alınmıştır.";
                        }
                        else
                        {
                            ViewBag.Error = "Öğrenci kaydedildi ancak başvuru oluşturulamadı. Lütfen yöneticiye başvurun. Öğrenci ID: " + ogrenciId;
                        }
                    }
                    catch (Exception basvuruEx)
                    {
                        ViewBag.Error = $"Öğrenci kaydedildi (ID: {ogrenciId}) ancak başvuru oluşturulurken hata oluştu: {basvuruEx.Message}";
                    }
                }
                else
                {
                    ViewBag.Error = "Öğrenci kaydedilemedi. Lütfen tekrar deneyin.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Bir hata oluştu: " + ex.Message;
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AIAnaliz(int ogrenciId)
        {
            try
            {
                var ogrenci = _ogrenciService.GetById(ogrenciId);
                if (ogrenci == null)
                {
                    ViewBag.Error = "Öğrenci bulunamadı.";
                    return View("Index");
                }

                ViewBag.Ogrenci = ogrenci;
                ViewBag.BursPuani = ogrenci.BursPuani > 0 
                    ? ogrenci.BursPuani 
                    : _puanlamaService.HesaplaBursPuani(ogrenci);
                
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Bir hata oluştu: " + ex.Message;
                return View("Index");
            }
        }
    }
}

