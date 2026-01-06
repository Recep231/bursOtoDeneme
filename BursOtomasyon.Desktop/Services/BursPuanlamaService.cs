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
            // Önce AI ile puan hesaplama dene (öncelikli)
            // AI çalışmazsa veya timeout olursa temel puan kullanılır
            try
            {
                if (!string.IsNullOrEmpty(_aiService.GetApiKey()))
                {
                    var task = HesaplaBursPuaniAsync(ogrenci);
                    // 15 saniye timeout - AI'ya daha fazla süre ver
                    if (task.Wait(TimeSpan.FromSeconds(15)))
                    {
                        if (task.IsCompletedSuccessfully && task.Result > 0)
                        {
                            // AI puanı geçerli aralıktaysa kullan
                            decimal aiPuan = task.Result;
                            if (aiPuan >= 0 && aiPuan <= 100)
                            {
                                // Şehir yaşam maliyeti katsayısını uygula
                                string? sehirs = _sehirKatsayiService.GetSehirByUniversite(ogrenci.Universite);
                                decimal sehirKatsayis = _sehirKatsayiService.GetKatsayiBySehir(sehirs);
                                
                                ogrenci.Sehir = sehirs;
                                ogrenci.SehirKatsayi = sehirKatsayis;
                                
                                decimal finalPuans = aiPuan * sehirKatsayis;
                                if (finalPuans > 100) finalPuans = 100;
                                if (finalPuans < 0) finalPuans = 0;
                                
                                return Math.Round(finalPuans, 2);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                // AI çalışmazsa temel puanı kullan - sessizce devam et
            }
            
            // AI çalışmadıysa temel puan hesaplama yap (fallback)
            decimal temelPuan = TemelPuanHesapla(ogrenci);
            
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
            // 2024 Türkiye Ekonomik Verileri:
            // - Asgari Ücret: ~28,000 TL (net)
            // - Açlık Sınırı: ~30,000 TL
            // - Yoksulluk Sınırı: ~100,000 TL (4 kişilik aile)
            // ============================================
            decimal sosyalPuan = 0;
            
            // 1. Aile Geliri Puanı (0-40 puan) - EN ÖNEMLİ KRİTER
            // DÜŞÜK gelir = YÜKSEK puan (bursa daha çok ihtiyaç var)
            if (ogrenci.AileGeliri <= 15000)
                sosyalPuan += 40;  // Açlık sınırının çok altı - ACİL BURS GEREKLİ
            else if (ogrenci.AileGeliri <= 25000)
                sosyalPuan += 35;  // Asgari ücretin altı - YÜKSEK İHTİYAÇ
            else if (ogrenci.AileGeliri <= 35000)
                sosyalPuan += 28;  // Asgari ücret civarı - İHTİYAÇ VAR
            else if (ogrenci.AileGeliri <= 50000)
                sosyalPuan += 20;  // Orta-düşük gelir
            else if (ogrenci.AileGeliri <= 70000)
                sosyalPuan += 12;  // Orta gelir
            else if (ogrenci.AileGeliri <= 100000)
                sosyalPuan += 6;   // Orta-üst gelir - Burs ihtiyacı düşük
            else
                sosyalPuan += 0;   // Yüksek gelir - Burs ihtiyacı yok
            
            // 2. Kardeş Sayısı Puanı (0-15 puan)
            // ÇOK kardeş = YÜKSEK puan (maddi yük daha fazla)
            int kardesSayisi = 0;
            int.TryParse(ogrenci.KardesSayisi, out kardesSayisi);
            
            if (kardesSayisi >= 5)
                sosyalPuan += 15;
            else if (kardesSayisi >= 4)
                sosyalPuan += 13;
            else if (kardesSayisi >= 3)
                sosyalPuan += 10;
            else if (kardesSayisi >= 2)
                sosyalPuan += 7;
            else if (kardesSayisi >= 1)
                sosyalPuan += 4;
            else
                sosyalPuan += 1; // Tek çocuk
            
            // 3. Klasik Sorular Puanı (0-8 puan)
            // Detaylı cevaplar = yüksek puan
            int klasikToplamUzunluk = 
                (ogrenci.KlasikSoru1Cevap ?? string.Empty).Length +
                (ogrenci.KlasikSoru2Cevap ?? string.Empty).Length +
                (ogrenci.KlasikSoru3Cevap ?? string.Empty).Length;
            
            if (klasikToplamUzunluk >= 500)
                sosyalPuan += 8;
            else if (klasikToplamUzunluk >= 300)
                sosyalPuan += 6;
            else if (klasikToplamUzunluk >= 150)
                sosyalPuan += 4;
            else if (klasikToplamUzunluk >= 50)
                sosyalPuan += 2;
            
            // 4. Sınıf Puanı (0-7 puan)
            // Üst sınıflar biraz daha yüksek puan
            int sinif = 1;
            int.TryParse(ogrenci.Sinif, out sinif);
            
            if (sinif >= 4)
                sosyalPuan += 7;
            else if (sinif >= 3)
                sosyalPuan += 5;
            else if (sinif >= 2)
                sosyalPuan += 4;
            else
                sosyalPuan += 2;
            
            // Sosyal puanı 0-70 aralığına sınırla
            // Maksimum: 40 (gelir) + 15 (kardeş) + 8 (klasik) + 7 (sınıf) = 70
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

        public async Task<decimal> HesaplaBursPuaniAsync(Ogrenci ogrenci)
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
                
                // Temel akademik puan hesapla (AI'ya referans için)
                decimal temelAkademikPuan = ogrenci.NotOrtalamasi * 25m * bolumKatsayi;
                if (temelAkademikPuan > 100) temelAkademikPuan = 100;
                
                var prompt = $@"Aşağıdaki öğrenci bilgilerini analiz ederek burs puanını hesapla. 

═══════════════════════════════════════════════════════════════
                    ÖĞRENCİ BİLGİLERİ
═══════════════════════════════════════════════════════════════

AKADEMİK BİLGİLER:
• Not Ortalaması (GNO): {ogrenci.NotOrtalamasi:F2}/4.00
• Üniversite: {ogrenci.Universite ?? "Belirtilmemiş"}
• Bölüm: {ogrenci.Bolum ?? "Belirtilmemiş"}
• Bölüm Zorluk Katsayısı: {bolumKatsayi:F2}
• Temel Akademik Puan (GNO × 25 × Bölüm Katsayısı): {temelAkademikPuan:F2}

AİLE DURUMU:
• Aile Aylık Geliri: {ogrenci.AileGeliri:N0} TL
• Kardeş Sayısı: {ogrenci.KardesSayisi}
• Anne Meslek: {ogrenci.AnneMeslek ?? "Belirtilmemiş"}
• Baba Meslek: {ogrenci.BabaMeslek ?? "Belirtilmemiş"}

KLASİK SORULAR VE CEVAPLARI:
• Soru 1: ""Bu bursu neden hak ettiğini düşünüyorsun?""
  Cevap: {(string.IsNullOrWhiteSpace(ogrenci.KlasikSoru1Cevap) ? "Cevap verilmemiş" : ogrenci.KlasikSoru1Cevap)}
  Uzunluk: {(string.IsNullOrWhiteSpace(ogrenci.KlasikSoru1Cevap) ? "0" : ogrenci.KlasikSoru1Cevap.Length.ToString())} karakter

• Soru 2: ""Gelecek hedeflerin nelerdir?""
  Cevap: {(string.IsNullOrWhiteSpace(ogrenci.KlasikSoru2Cevap) ? "Cevap verilmemiş" : ogrenci.KlasikSoru2Cevap)}
  Uzunluk: {(string.IsNullOrWhiteSpace(ogrenci.KlasikSoru2Cevap) ? "0" : ogrenci.KlasikSoru2Cevap.Length.ToString())} karakter

• Soru 3: ""Şu anki maddi/ailesel durumunu kısaca açıklar mısın?""
  Cevap: {(string.IsNullOrWhiteSpace(ogrenci.KlasikSoru3Cevap) ? "Cevap verilmemiş" : ogrenci.KlasikSoru3Cevap)}
  Uzunluk: {(string.IsNullOrWhiteSpace(ogrenci.KlasikSoru3Cevap) ? "0" : ogrenci.KlasikSoru3Cevap.Length.ToString())} karakter

DİĞER BİLGİLER:
• Sınıf: {ogrenci.Sinif}
• Şehir: {sehir ?? "Belirlenemedi"}
• Şehir Katsayısı: {sehirKatsayi:F2} (Bu katsayı final puana uygulanacak)

═══════════════════════════════════════════════════════════════
                    PUANLAMA KRİTERLERİ
═══════════════════════════════════════════════════════════════

2024 TÜRKİYE EKONOMİK VERİLERİ:
• Asgari Ücret: 28.000 TL (net)
• Açlık Sınırı: 30.000 TL
• Yoksulluk Sınırı: 100.000 TL (4 kişilik aile)

1. AKADEMİK PUAN (0-30 puan):
   • Temel Akademik Puan = GNO × 25 × Bölüm Katsayısı
   • Bu öğrenci için: {ogrenci.NotOrtalamasi:F2} × 25 × {bolumKatsayi:F2} = {temelAkademikPuan:F2}
   • Akademik Puan = Temel Akademik Puan'ın %30'u (maksimum 30 puan)
   • Hesaplama: {temelAkademikPuan:F2} × 0.30 = {(temelAkademikPuan * 0.30m):F2} puan

2. AİLE GELİRİ (0-40 puan) - EN ÖNEMLİ KRİTER:
   • 0-15.000 TL → 35-40 puan (ACİL BURS - açlık sınırının çok altı!)
   • 15.000-25.000 TL → 28-34 puan (YÜKSEK İHTİYAÇ - asgari ücretin altı)
   • 25.000-35.000 TL → 20-27 puan (İHTİYAÇ VAR - asgari ücret civarı)
   • 35.000-50.000 TL → 12-19 puan (ORTA)
   • 50.000-80.000 TL → 6-11 puan (DÜŞÜK - burs ihtiyacı az)
   • 80.000+ TL → 0-5 puan (YOK - burs ihtiyacı yok)
   • Bu öğrenci: {ogrenci.AileGeliri:N0} TL → [HESAPLA]

3. KARDEŞ SAYISI (0-20 puan):
   • 4+ kardeş → 18-20 puan
   • 3 kardeş → 15-17 puan
   • 2 kardeş → 10-14 puan
   • 1 kardeş → 5-9 puan
   • 0 kardeş → 1-4 puan
   • Bu öğrenci: {ogrenci.KardesSayisi} kardeş → [HESAPLA]

4. KLASİK SORULAR (0-15 puan) - ÇOK ÖNEMLİ:
   • Her soru 0-5 puan arası değerlendirilir
   • Detaylı, samimi, özenli cevaplar (150+ karakter) → 4-5 puan
   • Orta detaylı cevaplar (50-150 karakter) → 2-3 puan
   • Kısa veya umursamaz cevaplar (<50 karakter) → 0-1 puan
   • Her soruyu ayrı ayrı değerlendir ve topla
   • Soru 1: [HESAPLA] / 5
   • Soru 2: [HESAPLA] / 5
   • Soru 3: [HESAPLA] / 5
   • Toplam Klasik Sorular Puanı: [TOPLA]

5. SINIF (0-10 puan):
   • 4. sınıf → 9-10 puan
   • 3. sınıf → 7-8 puan
   • 2. sınıf → 5-6 puan
   • 1. sınıf → 3-4 puan
   • Bu öğrenci: {ogrenci.Sinif}. sınıf → [HESAPLA]

TOPLAM PUAN = Akademik Puan + Aile Geliri Puanı + Kardeş Sayısı Puanı + Klasik Sorular Puanı + Sınıf Puanı
TOPLAM PUAN = [HESAPLA] (0-100 arası)

═══════════════════════════════════════════════════════════════

ÖNEMLİ:
• Yukarıdaki kriterlere göre DETAYLI hesaplama yap
• Her kriteri ayrı ayrı değerlendir
• KLASİK SORULARA VERİLEN CEVAPLARI İÇERİK KALİTESİNE GÖRE DEĞERLENDİR (sadece uzunluk değil!)
• Aile geliri EN ÖNEMLİ kriterdir
• 2024 Türkiye ekonomik verilerini kullan
• SADECE 0-100 arası bir sayı döndür. Başka hiçbir şey yazma. Örnek: 72.5";

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
                        // AI puanı direkt kullan, sadece 0-100 aralığında tut
                        if (directPuan > 100) directPuan = 100;
                        if (directPuan < 0) directPuan = 0;
                        return Math.Round(directPuan, 2);
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
                            // AI puanı direkt kullan, sadece 0-100 aralığında tut
                            if (aiPuan > 100) aiPuan = 100;
                            if (aiPuan < 0) aiPuan = 0;
                            return Math.Round(aiPuan, 2);
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


