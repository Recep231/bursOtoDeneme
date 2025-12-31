using System;
using System.Threading.Tasks;
using BursOtomasyon.Desktop.Models;

namespace BursOtomasyon.Desktop.Services
{
    public class BursPuanlamaService
    {
        private AIAnalizService _aiService;
        private SehirKatsayiService _sehirKatsayiService;
        private BolumZorlukKatsayiService _bolumKatsayiService;

        public BursPuanlamaService()
        {
            _aiService = new AIAnalizService();
            _sehirKatsayiService = new SehirKatsayiService();
            _bolumKatsayiService = new BolumZorlukKatsayiService();
        }

        public decimal HesaplaBursPuani(Ogrenci ogrenci)
        {
            // Önce temel puan hesaplama yap (her zaman çalışır)
            decimal temelPuan = TemelPuanHesapla(ogrenci);
            
            // AI ile geliştirilmiş puan hesaplama (opsiyonel, non-blocking)
            // AI çalışmazsa veya timeout olursa temel puan kullanılır
            try
            {
                if (!string.IsNullOrEmpty(_aiService.GetApiKey()))
                {
                    var task = HesaplaBursPuaniAsync(ogrenci, temelPuan);
                    // 10 saniye timeout - daha uzun süre ver
                    if (task.Wait(TimeSpan.FromSeconds(10)))
                    {
                        if (task.IsCompletedSuccessfully && task.Result > 0)
                        {
                            // AI puanı geçerli aralıktaysa kullan
                            decimal aiPuan = task.Result;
                            if (aiPuan >= 0 && aiPuan <= 100)
                            {
                                temelPuan = aiPuan;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // AI çalışmazsa temel puanı kullan - sessizce devam et
            }
            
            // Şehir yaşam maliyeti katsayısını uygula
            string? sehir = _sehirKatsayiService.GetSehirByUniversite(ogrenci.Universite);
            decimal sehirKatsayi = _sehirKatsayiService.GetKatsayiBySehir(sehir);
            
            // Öğrenci modeline şehir bilgisini kaydet (opsiyonel, admin paneli için)
            ogrenci.Sehir = sehir;
            ogrenci.SehirKatsayi = sehirKatsayi;
            
            // Final puan = (Akademik Puan + Sosyal Puan) × Şehir Katsayısı
            decimal finalPuan = temelPuan * sehirKatsayi;
            
            // Puanı 0-100 aralığına sınırla (katsayı uygulandıktan sonra)
            if (finalPuan > 100) finalPuan = 100;
            if (finalPuan < 0) finalPuan = 0;
            
            return Math.Round(finalPuan, 2);
        }
        
        private decimal TemelPuanHesapla(Ogrenci ogrenci)
        {
            // ============================================
            // AKADEMİK PUAN HESAPLAMASI
            // ============================================
            // 1. Temel Akademik Puan = GNO × 25
            decimal temelAkademikPuan = ogrenci.NotOrtalamasi * 25m;
            
            // 2. Bölüm Zorluk Katsayısını Uygula
            decimal bolumKatsayi = _bolumKatsayiService.GetKatsayiByBolum(ogrenci.Bolum);
            decimal akademikPuan = temelAkademikPuan * bolumKatsayi;
            
            // Öğrenci modeline bölüm katsayısını kaydet (opsiyonel, admin paneli için)
            ogrenci.BolumKatsayi = bolumKatsayi;
            
            // Akademik puanı 0-100 aralığına sınırla (bölüm katsayısı uygulandıktan sonra)
            if (akademikPuan > 100) akademikPuan = 100;
            if (akademikPuan < 0) akademikPuan = 0;
            
            // ============================================
            // SOSYAL PUAN HESAPLAMASI
            // ============================================
            decimal sosyalPuan = 0;
            
            // 1. Aile Geliri Puanı (0-30 puan)
            // DÜŞÜK gelir = YÜKSEK puan
            if (ogrenci.AileGeliri <= 5000)
                sosyalPuan += 30;
            else if (ogrenci.AileGeliri <= 10000)
                sosyalPuan += 25;
            else if (ogrenci.AileGeliri <= 15000)
                sosyalPuan += 20;
            else if (ogrenci.AileGeliri <= 20000)
                sosyalPuan += 15;
            else if (ogrenci.AileGeliri <= 25000)
                sosyalPuan += 10;
            else
                sosyalPuan += 5;
            
            // 2. Kardeş Sayısı Puanı (0-20 puan)
            // ÇOK kardeş = YÜKSEK puan
            if (ogrenci.KardesSayisi == "4" || ogrenci.KardesSayisi == "4+")
                sosyalPuan += 20;
            else if (ogrenci.KardesSayisi == "3")
                sosyalPuan += 15;
            else if (ogrenci.KardesSayisi == "2")
                sosyalPuan += 10;
            else if (ogrenci.KardesSayisi == "1")
                sosyalPuan += 5;
            
            // 3. Klasik Sorular Puanı (0-10 puan)
            // Detaylı cevaplar = yüksek puan
            int klasikToplamUzunluk = 
                (ogrenci.KlasikSoru1Cevap ?? string.Empty).Length +
                (ogrenci.KlasikSoru2Cevap ?? string.Empty).Length +
                (ogrenci.KlasikSoru3Cevap ?? string.Empty).Length;
            
            if (klasikToplamUzunluk >= 500)
                sosyalPuan += 10;
            else if (klasikToplamUzunluk >= 300)
                sosyalPuan += 7;
            else if (klasikToplamUzunluk >= 150)
                sosyalPuan += 5;
            else if (klasikToplamUzunluk >= 50)
                sosyalPuan += 2;
            
            // 4. Sınıf Puanı (0-10 puan)
            // Üst sınıflar biraz daha yüksek puan
            switch (ogrenci.Sinif)
            {
                case "4":
                    sosyalPuan += 10;
                    break;
                case "3":
                    sosyalPuan += 8;
                    break;
                case "2":
                    sosyalPuan += 6;
                    break;
                case "1":
                    sosyalPuan += 4;
                    break;
                case "Hazırlık":
                    sosyalPuan += 2;
                    break;
            }
            
            // Sosyal puanı 0-70 aralığına sınırla (akademik puan 0-100 olabilir)
            if (sosyalPuan > 70) sosyalPuan = 70;
            if (sosyalPuan < 0) sosyalPuan = 0;
            
            // ============================================
            // TOPLAM PUAN = AKADEMİK PUAN + SOSYAL PUAN
            // ============================================
            decimal toplamPuan = akademikPuan + sosyalPuan;
            
            // Toplam puanı 0-100 aralığına sınırla
            if (toplamPuan > 100) toplamPuan = 100;
            if (toplamPuan < 0) toplamPuan = 0;
            
            return Math.Round(toplamPuan, 2);
        }

        public async Task<decimal> HesaplaBursPuaniAsync(Ogrenci ogrenci, decimal temelPuan)
        {
            if (string.IsNullOrEmpty(_aiService.GetApiKey()))
            {
                return 0; // AI yoksa 0 döndür, temel puan kullanılacak
            }

            try
            {
                // Şehir ve bölüm katsayılarını al
                string? sehir = _sehirKatsayiService.GetSehirByUniversite(ogrenci.Universite);
                decimal sehirKatsayi = _sehirKatsayiService.GetKatsayiBySehir(sehir);
                decimal bolumKatsayi = _bolumKatsayiService.GetKatsayiByBolum(ogrenci.Bolum);
                
                var prompt = $@"Aşağıdaki öğrenci bilgilerini analiz ederek burs puanını hesapla. Temel puan: {temelPuan:F2}

Öğrenci Bilgileri:
- Not Ortalaması: {ogrenci.NotOrtalamasi:F2}/4.00
- Aile Aylık Geliri: {ogrenci.AileGeliri:N0} TL
- Kardeş Sayısı: {ogrenci.KardesSayisi}
- Sınıf: {ogrenci.Sinif}
- Üniversite: {ogrenci.Universite ?? "Belirtilmemiş"}
- Bölüm: {ogrenci.Bolum ?? "Belirtilmemiş"}
- Şehir: {sehir ?? "Belirlenemedi"}
- Şehir Katsayısı: {sehirKatsayi:F2}
- Bölüm Katsayısı: {bolumKatsayi:F2}
- Klasik Soru 1 Uzunluğu: {(string.IsNullOrWhiteSpace(ogrenci.KlasikSoru1Cevap) ? "0" : ogrenci.KlasikSoru1Cevap.Length.ToString())} karakter
- Klasik Soru 2 Uzunluğu: {(string.IsNullOrWhiteSpace(ogrenci.KlasikSoru2Cevap) ? "0" : ogrenci.KlasikSoru2Cevap.Length.ToString())} karakter
- Klasik Soru 3 Uzunluğu: {(string.IsNullOrWhiteSpace(ogrenci.KlasikSoru3Cevap) ? "0" : ogrenci.KlasikSoru3Cevap.Length.ToString())} karakter

ÖNEMLİ KRİTERLER:
- Yüksek not (3.0+) = artı puan
- DÜŞÜK gelir (10.000 TL altı) = YÜKSEK puan, YÜKSEK gelir (25.000 TL üstü) = DÜŞÜK puan
- Çok kardeş (3+) = artı puan
- Detaylı klasik cevaplar (150+ karakter toplam) = artı puan
- Şehir katsayısı ve bölüm katsayısı zaten temel puana uygulanmıştır

SADECE SAYI DÖNDÜR (0-100 arası). Temel puanı referans al ama öğrencinin durumuna göre ±20% aralığında ayarla. Sadece sayıyı yaz, başka metin ekleme.";

                var response = await _aiService.PuanHesaplaAsync(prompt);
                
                if (string.IsNullOrWhiteSpace(response))
                {
                    return 0;
                }
                
                // Yanıttan sayıyı çıkar - daha agresif parsing
                var cleanedResponse = response.Trim();
                
                // Önce direkt parse dene
                if (decimal.TryParse(cleanedResponse, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal directPuan))
                {
                    if (directPuan >= 0 && directPuan <= 100)
                    {
                        return ValidateAIPuan(directPuan, temelPuan);
                    }
                }
                
                // Regex ile sayı bul
                var regex = new System.Text.RegularExpressions.Regex(@"(\d+[.,]?\d*)");
                var matches = regex.Matches(cleanedResponse);
                
                foreach (System.Text.RegularExpressions.Match match in matches)
                {
                    var numberStr = match.Value.Replace(',', '.');
                    if (decimal.TryParse(numberStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal aiPuan))
                    {
                        if (aiPuan >= 0 && aiPuan <= 100)
                        {
                            return ValidateAIPuan(aiPuan, temelPuan);
                        }
                    }
                }
            }
            catch (Exception)
            {
                // Hata durumunda 0 döndür, temel puan kullanılacak
            }
            
            return 0; // AI çalışmazsa 0, temel puan kullanılacak
        }
        
        private decimal ValidateAIPuan(decimal aiPuan, decimal temelPuan)
        {
            // AI puanı temel puanın %70-130 aralığında tut (daha esnek)
            decimal minPuan = Math.Max(0, temelPuan * 0.7m);
            decimal maxPuan = Math.Min(100, temelPuan * 1.3m);
            
            if (aiPuan < minPuan) aiPuan = minPuan;
            if (aiPuan > maxPuan) aiPuan = maxPuan;
            if (aiPuan > 100) aiPuan = 100;
            if (aiPuan < 0) aiPuan = 0;
            
            return Math.Round(aiPuan, 2);
        }
    }
}


