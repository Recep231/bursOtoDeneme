using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BursOtomasyon.Desktop.Models;

namespace BursOtomasyon.Desktop.Services
{
    public class AIAnalizService
    {
        private readonly string _apiKey;
        private readonly string _apiUrl = "https://api.groq.com/openai/v1/chat/completions";

        public AIAnalizService(string? apiKey = null)
        {
            // Önce dışarıdan verilen, sonra GROQ_API_KEY, sonra OPENAI_API_KEY
            _apiKey = apiKey
                      ?? Environment.GetEnvironmentVariable("GROQ_API_KEY")
                      ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY")
                      ?? "";
        }

        public string GetApiKey()
        {
            return _apiKey;
        }

        public async Task<string> PuanHesaplaAsync(string prompt)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                return "0";
            }

            try
            {
                var requestBody = new
                {
                    model = "llama-3.3-70b-versatile", // Daha güçlü ve tutarlı model
                    messages = new[]
                    {
                        new { role = "system", content = @"Sen bir burs puanlama uzmanısın. 2025 TÜRKİYE EKONOMİSİNE GÖRE değerlendir:

2025 TÜRKİYE VERİLERİ (güncel tahmini sınırlar):
- Asgari Ücret: ~23.000 TL (net)
- Açlık Sınırı: ~40.000 TL
- Yoksulluk Sınırı: ~120.000 TL (4 kişilik aile)

PUANLAMA KURALLARI (BU SINIRLARA GÖRE):
1. ÇOK DÜŞÜK GELİR (0-30.000 TL) = ÇOK YÜKSEK PUAN (80-100) - Açlık sınırının altı, ACİL BURS GEREKLİ
2. DÜŞÜK GELİR (30.000-45.000 TL) = YÜKSEK PUAN (65-79) - Asgari ücret civarı/altı
3. ORTA-DÜŞÜK GELİR (45.000-70.000 TL) = ORTA-YÜKSEK PUAN (50-64)
4. ORTA GELİR (70.000-110.000 TL) = ORTA PUAN (35-49)
5. ORTA-ÜST GELİR (110.000-150.000 TL) = DÜŞÜK PUAN (20-34)
6. YÜKSEK GELİR (150.000+ TL) = ÇOK DÜŞÜK PUAN (0-19) - Bursa ihtiyacı yok

EK FAKTÖRLER:
- YÜKSEK NOT (3.0+) = +10 puan bonus
- ÇOK KARDEŞ (3+) = +8 puan bonus
- AZ KARDEŞ (0-1) = -3 puan

SADECE 0-100 arası bir sayı döndür. Başka hiçbir şey yazma. Örnek: 72" },
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 10,
                    temperature = 0.1 // Çok tutarlı sonuçlar için
                };

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(15); // Timeout ekle
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                    var json = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(_apiUrl, content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        dynamic? result = JsonConvert.DeserializeObject(responseContent);
                        if (result != null && result.choices != null && result.choices[0] != null)
                        {
                            var contentText = (string?)result.choices[0].message?.content;
                            if (!string.IsNullOrWhiteSpace(contentText))
                            {
                                // Önce direkt parse dene
                                var trimmed = contentText.Trim();
                                if (decimal.TryParse(trimmed, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal directPuan))
                                {
                                    return directPuan.ToString(System.Globalization.CultureInfo.InvariantCulture);
                                }
                                
                                // Satır satır kontrol et
                                var lines = trimmed.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                                foreach (var line in lines)
                                {
                                    var cleanLine = line.Trim();
                                    // Sadece sayı içeren satırı bul
                                    var regex = new System.Text.RegularExpressions.Regex(@"^\s*(\d+[.,]?\d*)\s*$");
                                    var match = regex.Match(cleanLine);
                                    if (match.Success)
                                    {
                                        var numberStr = match.Groups[1].Value.Replace(',', '.');
                                        if (decimal.TryParse(numberStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal puan))
                                        {
                                            return puan.ToString(System.Globalization.CultureInfo.InvariantCulture);
                                        }
                                    }
                                }
                                
                                // Son çare: herhangi bir sayı bul
                                var numberRegex = new System.Text.RegularExpressions.Regex(@"(\d+[.,]?\d*)");
                                var numberMatch = numberRegex.Match(trimmed);
                                if (numberMatch.Success)
                                {
                                    var numberStr = numberMatch.Value.Replace(',', '.');
                                    if (decimal.TryParse(numberStr, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out decimal foundPuan))
                                    {
                                        return foundPuan.ToString(System.Globalization.CultureInfo.InvariantCulture);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        // API hatası - log için
                        System.Diagnostics.Debug.WriteLine($"AI API Hatası: {response.StatusCode} - {responseContent}");
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda boş döndür, fallback kullanılacak
                System.Diagnostics.Debug.WriteLine($"AI Puan Hesaplama Hatası: {ex.Message}");
            }

            return "0";
        }

        public async Task<string> DetayliAnalizYapAsync(Ogrenci ogrenci, decimal bursPuani)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                return "AI API anahtarı bulunamadı. Lütfen GROQ_API_KEY ortam değişkenini veya OPENAI_API_KEY değişkenini ayarlayın.\n\n" +
                       "Simüle edilmiş analiz:\n" +
                       SimuleAnaliz(ogrenci, bursPuani);
            }

            try
            {
                // Şehir bilgisini al
                var sehirKatsayiService = new SehirKatsayiService();
                string? sehir = sehirKatsayiService.GetSehirByUniversite(ogrenci.Universite);
                decimal sehirKatsayi = sehirKatsayiService.GetKatsayiBySehir(sehir);
                
                // Bölüm zorluk katsayısını al
                var bolumKatsayiService = new BolumZorlukKatsayiService();
                decimal bolumKatsayi = bolumKatsayiService.GetKatsayiByBolum(ogrenci.Bolum);
                
                var prompt = $@"Aşağıdaki öğrenci bilgilerini DETAYLI ve KAPSAMLI bir şekilde analiz ederek profesyonel bir burs değerlendirme raporu hazırla.

═══════════════════════════════════════════════════════════════
                    ÖĞRENCİ BİLGİLERİ
═══════════════════════════════════════════════════════════════

KİŞİSEL BİLGİLER:
• Ad Soyad: {ogrenci.Ad} {ogrenci.Soyad}
• TC Kimlik No: {ogrenci.TC}
• Sınıf: {ogrenci.Sinif}

AKADEMİK BİLGİLER:
• Not Ortalaması (GNO): {ogrenci.NotOrtalamasi:F2}/4.00
• Üniversite: {ogrenci.Universite}
• Bölüm: {ogrenci.Bolum}
• Bölüm Zorluk Katsayısı: {bolumKatsayi:F2} {(bolumKatsayi >= 1.20m ? "(Yüksek Zorluk)" : bolumKatsayi >= 1.10m ? "(Orta-Yüksek Zorluk)" : bolumKatsayi >= 1.05m ? "(Orta Zorluk)" : "(Standart Zorluk)")}
• Şehir: {sehir ?? "Belirlenemedi"}
• Şehir Yaşam Maliyeti Katsayısı: {sehirKatsayi:F2}

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

HESAPLANAN BURS PUANI: {bursPuani:F2}/100

═══════════════════════════════════════════════════════════════
                    DEĞERLENDİRME KRİTERLERİ
═══════════════════════════════════════════════════════════════

1. NOT ORTALAMASI (GNO) - Akademik Puan Hesaplama:
   • Temel Akademik Puan = GNO × 25
   • Akademik Puan = Temel Akademik Puan × Bölüm Zorluk Katsayısı
   • Örnek: GNO 3.20, Mühendislik (1.15) → 3.20 × 25 × 1.15 = 92 puan
   • Örnek: GNO 3.20, İİBF (1.00) → 3.20 × 25 × 1.00 = 80 puan
   • Bölüm zorluk katsayıları:
     - Tıp Fakültesi: 1.30 (En Yüksek)
     - Mühendislik: 1.15 (Yüksek)
     - Fen-Edebiyat: 1.05 (Orta-Yüksek)
     - İİBF: 1.00 (Standart)
     - Eğitim: 1.00 (Standart)

2. AİLE GELİRİ (0-40 puan) - EN ÖNEMLİ KRİTER:
   (2025 Türkiye: Asgari Ücret ~23.000 TL, Açlık Sınırı ~40.000 TL)
   • 0-30.000 TL → Çok Düşük Gelir (35-40 puan) ⭐⭐ ACİL BURS GEREKLİ
   • 30.000-45.000 TL → Düşük Gelir (28-34 puan) ⭐ YÜKSEK İHTİYAÇ
   • 45.000-70.000 TL → Orta-Düşük Gelir (20-27 puan) - İHTİYAÇ VAR
   • 70.000-110.000 TL → Orta Gelir (12-19 puan)
   • 110.000-150.000 TL → Orta-Üst Gelir (6-11 puan) ⚠️ Burs ihtiyacı düşük
   • 150.000 TL üstü → Yüksek Gelir (0-5 puan) ⚠️ Burs ihtiyacı yok

3. KARDEŞ SAYISI (0-20 puan):
   • 4+ kardeş → Çok Kalabalık Aile (18-20 puan) ⭐ YÜKSEK PUAN
   • 3 kardeş → Kalabalık Aile (15-17 puan) ⭐ YÜKSEK PUAN
   • 2 kardeş → Normal (10-14 puan)
   • 1 kardeş → Az (5-9 puan)
   • 0 kardeş → Tek Çocuk (0-4 puan)

4. KLASİK SORULAR (0-10 puan):
   • Detaylı, samimi, özenli cevaplar (150+ karakter) → Yüksek Puan (8-10)
   • Orta detaylı cevaplar (50-150 karakter) → Orta Puan (5-7)
   • Kısa veya umursamaz cevaplar (<50 karakter) → Düşük Puan (0-4)

5. SINIF (0-10 puan):
   • 4. Sınıf → En Üst Sınıf (9-10 puan)
   • 3. Sınıf → Üst Sınıf (7-8 puan)
   • 2. Sınıf → Orta Sınıf (5-6 puan)
   • 1. Sınıf → Alt Sınıf (3-4 puan)

6. BÖLÜM ZORLUK KATSAYISI:
   • Öğrencinin bölümü: {ogrenci.Bolum ?? "Belirtilmemiş"}
   • Bölüm zorluk katsayısı: {bolumKatsayi:F2}
   • {(bolumKatsayi >= 1.20m ? "Yüksek zorluk seviyesine sahip bir bölüm (Tıp, Diş Hekimliği, Eczacılık vb.)" : bolumKatsayi >= 1.10m ? "Orta-yüksek zorluk seviyesine sahip bir bölüm (Hukuk vb.)" : bolumKatsayi >= 1.05m ? "Orta zorluk seviyesine sahip bir bölüm (Fen-Edebiyat, Ziraat vb.)" : "Standart zorluk seviyesine sahip bir bölüm (İİBF, Eğitim vb.)")}
   • Bu katsayı akademik puan hesaplamasına uygulanmıştır: Akademik Puan = (GNO × 25) × Bölüm Katsayısı

7. ŞEHİR YAŞAM MALİYETİ:
   • Öğrencinin üniversitesi {sehir ?? "bilinmeyen"} şehrinde bulunmaktadır.
   • Bu şehrin yaşam maliyeti katsayısı {sehirKatsayi:F2}'dir.
   • {(sehirKatsayi >= 1.20m ? "Yüksek yaşam maliyeti olan bir şehir" : sehirKatsayi >= 1.00m ? "Orta yaşam maliyeti olan bir şehir" : "Düşük yaşam maliyeti olan bir şehir")}.
   • Bu katsayı final burs puanına çarpılarak uygulanmıştır: Final Puan = (Akademik Puan + Sosyal Puan) × Şehir Katsayısı

═══════════════════════════════════════════════════════════════
                    RAPOR FORMATI
═══════════════════════════════════════════════════════════════

Lütfen aşağıdaki formatta DETAYLI, PROFESYONEL ve OBJEKTİF bir rapor hazırla:

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
1. GENEL DEĞERLENDİRME SKORU
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Nihai Yüzdelik Skor: [0-100 arası sayı]
Uygunluk Durumu: [Burs almaya çok uygun / Orta düzey uygun / Burs almaya uygun değil]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
2. DETAYLI ANALİZ (15-20 cümle, her kriteri ayrıntılıca açıkla)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
• Not ortalamasını (GNO) detaylıca değerlendir ve bölüm zorluk katsayısının akademik puana etkisini açıkla
• Temel akademik puan hesaplamasını (GNO × 25) ve bölüm katsayısı uygulamasını açıkla
• Aile gelir durumunu kapsamlı analiz et (çok önemli! kaç puan aldı, neden, 2025 Türkiye verilerine göre)
• Kardeş sayısının etkisini açıkla (kaç puan aldı, neden)
• KLASİK SORULARA VERİLEN CEVAPLARI DETAYLI YORUMLA:
  - Soru 1 cevabını analiz et: Samimi mi? Detaylı mı? Hedefler net mi? Kaç puan aldı ve neden?
  - Soru 2 cevabını analiz et: Gelecek planları açık mı? Gerçekçi mi? Kaç puan aldı ve neden?
  - Soru 3 cevabını analiz et: Maddi durum açık mı? İhtiyaç belirtilmiş mi? Kaç puan aldı ve neden?
  - Her soruya verilen cevabın içerik kalitesini, samimiyetini ve detayını değerlendir
• Bölüm zorluk katsayısının akademik puana etkisini detaylıca açıkla
• Şehir yaşam maliyeti katsayısının final puana etkisini açıkla
• Genel durumu objektif bir şekilde özetle

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
3. GÜÇLÜ YÖNLER (5-7 madde)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
• [Madde 1: Öğrencinin burs almaya uygun olan güçlü yönü - detaylı açıkla]
• [Madde 2: Öğrencinin burs almaya uygun olan güçlü yönü - detaylı açıkla]
• [Madde 3: Öğrencinin burs almaya uygun olan güçlü yönü - detaylı açıkla]
• [Madde 4: Klasik sorulara verilen cevaplardan çıkan güçlü yönler]
• [Madde 5: Akademik başarı veya hedeflerden çıkan güçlü yönler]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
4. ZAYIF YÖNLER (3-5 madde)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
• [Madde 1: Öğrencinin burs almaya uygun olmayan zayıf yönü - detaylı açıkla]
• [Madde 2: Öğrencinin burs almaya uygun olmayan zayıf yönü - detaylı açıkla]
• [Madde 3: Klasik sorulara verilen cevaplardan çıkan zayıf yönler]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
5. KLASİK SORULAR ANALİZİ (DETAYLI)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Soru 1 Analizi: ""Bu bursu neden hak ettiğini düşünüyorsun?""
• Cevap Kalitesi: [Detaylı değerlendirme - samimiyet, özen, içerik]
• Puan: [0-5 arası] / 5
• Yorum: [3-4 cümlelik detaylı yorum]

Soru 2 Analizi: ""Gelecek hedeflerin nelerdir?""
• Cevap Kalitesi: [Detaylı değerlendirme - netlik, gerçekçilik, planlama]
• Puan: [0-5 arası] / 5
• Yorum: [3-4 cümlelik detaylı yorum]

Soru 3 Analizi: ""Şu anki maddi/ailesel durumunu kısaca açıklar mısın?""
• Cevap Kalitesi: [Detaylı değerlendirme - açıklık, detay, ihtiyaç belirtme]
• Puan: [0-5 arası] / 5
• Yorum: [3-4 cümlelik detaylı yorum]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
6. ÖNERİLER
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Öğrenciye Yönelik:
• [Madde 1: Öğrenciye yönelik detaylı öneri - 2-3 cümle]
• [Madde 2: Öğrenciye yönelik detaylı öneri - 2-3 cümle]
• [Madde 3: Klasik sorulara verilen cevaplara göre öneri]

Değerlendirme Komitesine Yönelik:
• [Madde 1: Komiteye yönelik detaylı öneri - 2-3 cümle]
• [Madde 2: Komiteye yönelik detaylı öneri - 2-3 cümle]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
7. SONUÇ
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
[5-7 cümlelik genel değerlendirme ve karar özeti. Burs verilip verilmeyeceği konusunda net bir tavsiye içermeli. Tüm kriterleri özetle ve final kararı belirt.]

═══════════════════════════════════════════════════════════════

ÖNEMLİ NOTLAR:
• Her kriteri ayrıntılıca değerlendir ve puan dağılımını açıkla.
• Aile geliri kriteri EN ÖNEMLİ kriterdir - mutlaka detaylı analiz et.
• KLASİK SORULARA VERİLEN CEVAPLARI MUTLAKA DETAYLI YORUMLA - bu çok önemli!
• Objektif, profesyonel ve tutarlı bir dil kullan.
• Türkçe yaz ve emoji kullanma.
• Raporu yukarıdaki formatı takip ederek hazırla.
• EN AZ 800-1000 KELİME uzunluğunda detaylı bir rapor hazırla.";

                var requestBody = new
                {
                    model = "llama-3.3-70b-versatile", // En güncel ve güçlü model
                    messages = new[]
                    {
                        new { role = "system", content = @"Sen bir burs değerlendirme uzmanısın ve profesyonel raporlar hazırlıyorsun.

2025 TÜRKİYE EKONOMİK VERİLERİ (güncel tahmini sınırlar):
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
• Asgari Ücret: ~23.000 TL (net)
• Açlık Sınırı: ~40.000 TL (4 kişilik aile)
• Yoksulluk Sınırı: ~120.000 TL (4 kişilik aile)

KRİTİK PUANLAMA KURALLARI (MUTLAKA UYGULA):
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
1. AİLE GELİRİ (EN ÖNEMLİ KRİTER - 40 PUAN):
   • 0-30.000 TL: 35-40 puan (ACİL BURS - açlık sınırının altı)
   • 30.000-45.000 TL: 28-34 puan (YÜKSEK İHTİYAÇ - asgari ücret civarı/altı)
   • 45.000-70.000 TL: 20-27 puan (İHTİYAÇ VAR)
   • 70.000-110.000 TL: 12-19 puan (ORTA)
   • 110.000-150.000 TL: 6-11 puan (DÜŞÜK - burs ihtiyacı az)
   • 150.000+ TL: 0-5 puan (YOK - burs ihtiyacı yok)

2. NOT ORTALAMASI (30 PUAN):
   • 3.50-4.00: 25-30 puan
   • 3.00-3.49: 20-24 puan
   • 2.50-2.99: 15-19 puan
   • 2.00-2.49: 10-14 puan
   • 2.00 altı: 0-9 puan

3. KARDEŞ SAYISI (15 PUAN):
   • 4+ kardeş: 13-15 puan
   • 3 kardeş: 10-12 puan
   • 2 kardeş: 7-9 puan
   • 1 kardeş: 4-6 puan
   • 0 kardeş: 1-3 puan

4. SINIF VE DİĞER (15 PUAN):
   • 4. sınıf: 12-15 puan
   • 3. sınıf: 9-11 puan
   • 2. sınıf: 6-8 puan
   • 1. sınıf: 3-5 puan

TOPLAM: 100 PUAN

ÖNEMLİ:
• Türkçe yaz, emoji kullanma
• Verilen formatı takip et
• Her kriteri açıkça puanla ve toplam skoru hesapla
• Profesyonel ve objektif ol
• GELİR DEĞERLENDİRMESİNDE 2025 TÜRKİYE VERİLERİNİ KULLAN!" },
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 4000,
                    temperature = 0.2 // Çok tutarlı sonuçlar
                };

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(60); // 60 saniye timeout
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                    var json = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(_apiUrl, content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        dynamic? result = JsonConvert.DeserializeObject(responseContent);
                        if (result != null && result.choices != null && result.choices[0] != null)
                        {
                            var contentText = (string?)result.choices[0].message?.content;
                            if (!string.IsNullOrWhiteSpace(contentText))
                            {
                                // Eğer çok kısa bir yanıt gelirse, tekrar dene
                                if (contentText.Trim().Length < 500)
                                {
                                    // Kısa yanıt gelirse tekrar dene
                                    return await AnalizYapAsync(ogrenci, bursPuani);
                                }
                                return contentText;
                            }
                        }
                        // Boş yanıt gelirse normal analizi dene
                        return await AnalizYapAsync(ogrenci, bursPuani);
                    }
                    else
                    {
                        // API hatası durumunda normal analizi dene
                        return await AnalizYapAsync(ogrenci, bursPuani);
                    }
                }
            }
            catch (Exception)
            {
                // Hata durumunda normal analizi dene
                return await AnalizYapAsync(ogrenci, bursPuani);
            }
        }

        public async Task<string> AnalizYapAsync(Ogrenci ogrenci, decimal bursPuani)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                return "AI API anahtarı bulunamadı. Lütfen GROQ_API_KEY ortam değişkenini veya OPENAI_API_KEY değişkenini ayarlayın.\n\n" +
                       "Simüle edilmiş analiz:\n" +
                       SimuleAnaliz(ogrenci, bursPuani);
            }

            try
            {
                // Şehir ve bölüm katsayılarını al
                var sehirKatsayiService = new SehirKatsayiService();
                string? sehir = sehirKatsayiService.GetSehirByUniversite(ogrenci.Universite);
                decimal sehirKatsayi = sehirKatsayiService.GetKatsayiBySehir(sehir);
                
                var bolumKatsayiService = new BolumZorlukKatsayiService();
                decimal bolumKatsayi = bolumKatsayiService.GetKatsayiByBolum(ogrenci.Bolum);

                var prompt = $@"Aşağıdaki öğrenci bilgilerini DETAYLI ve KAPSAMLI bir şekilde analiz ederek profesyonel bir burs değerlendirme raporu hazırla.

═══════════════════════════════════════════════════════════════
                    ÖĞRENCİ BİLGİLERİ
═══════════════════════════════════════════════════════════════

KİŞİSEL BİLGİLER:
• Ad Soyad: {ogrenci.Ad} {ogrenci.Soyad}
• TC Kimlik No: {ogrenci.TC}
• Sınıf: {ogrenci.Sinif}
• Doğum Tarihi: {ogrenci.DogumTarihi:dd.MM.yyyy}

AKADEMİK BİLGİLER:
• Not Ortalaması (GNO): {ogrenci.NotOrtalamasi:F2}/4.00
• Üniversite: {ogrenci.Universite ?? "Belirtilmemiş"}
• Fakülte: {ogrenci.Fakulte ?? "Belirtilmemiş"}
• Bölüm: {ogrenci.Bolum ?? "Belirtilmemiş"}
• Bölüm Zorluk Katsayısı: {bolumKatsayi:F2} {(bolumKatsayi >= 1.20m ? "(Yüksek Zorluk)" : bolumKatsayi >= 1.10m ? "(Orta-Yüksek Zorluk)" : bolumKatsayi >= 1.05m ? "(Orta Zorluk)" : "(Standart Zorluk)")}
• Şehir: {sehir ?? "Belirlenemedi"}
• Şehir Yaşam Maliyeti Katsayısı: {sehirKatsayi:F2}

AİLE DURUMU:
• Aile Aylık Geliri: {ogrenci.AileGeliri:N0} TL
• Kardeş Sayısı: {ogrenci.KardesSayisi}
• Anne Meslek: {ogrenci.AnneMeslek ?? "Belirtilmemiş"}
• Baba Meslek: {ogrenci.BabaMeslek ?? "Belirtilmemiş"}

KLASİK SORULAR VE CEVAPLARI:
• Soru 1: ""Bu bursu neden hak ettiğini düşünüyorsun?""
  Cevap: {(string.IsNullOrWhiteSpace(ogrenci.KlasikSoru1Cevap) ? "Cevap verilmemiş" : ogrenci.KlasikSoru1Cevap)}

• Soru 2: ""Gelecek hedeflerin nelerdir?""
  Cevap: {(string.IsNullOrWhiteSpace(ogrenci.KlasikSoru2Cevap) ? "Cevap verilmemiş" : ogrenci.KlasikSoru2Cevap)}

• Soru 3: ""Şu anki maddi/ailesel durumunu kısaca açıklar mısın?""
  Cevap: {(string.IsNullOrWhiteSpace(ogrenci.KlasikSoru3Cevap) ? "Cevap verilmemiş" : ogrenci.KlasikSoru3Cevap)}

HESAPLANAN BURS PUANI: {bursPuani:F2}/100

═══════════════════════════════════════════════════════════════
                    DEĞERLENDİRME KRİTERLERİ
═══════════════════════════════════════════════════════════════

1. NOT ORTALAMASI (GNO) - Akademik Puan:
   • Temel Akademik Puan = GNO × 25
   • Akademik Puan = Temel Akademik Puan × Bölüm Zorluk Katsayısı
   • Örnek: GNO 3.20, Mühendislik (1.15) → 3.20 × 25 × 1.15 = 92 puan
   • Örnek: GNO 3.20, İİBF (1.00) → 3.20 × 25 × 1.00 = 80 puan

2. AİLE GELİRİ (0-40 puan) - EN ÖNEMLİ KRİTER:
   (2025 Türkiye: Asgari Ücret ~23.000 TL, Açlık Sınırı ~40.000 TL)
   • 0-30.000 TL → Çok Düşük Gelir (35-40 puan) ⭐⭐ ACİL BURS GEREKLİ
   • 30.000-45.000 TL → Düşük Gelir (28-34 puan) ⭐ YÜKSEK İHTİYAÇ
   • 45.000-70.000 TL → Orta-Düşük Gelir (20-27 puan) - İHTİYAÇ VAR
   • 70.000-110.000 TL → Orta Gelir (12-19 puan)
   • 110.000-150.000 TL → Orta-Üst Gelir (6-11 puan) ⚠️ Burs ihtiyacı düşük
   • 150.000 TL üstü → Yüksek Gelir (0-5 puan) ⚠️ Burs ihtiyacı yok

3. KARDEŞ SAYISI (0-20 puan):
   • 4+ kardeş → Çok Kalabalık Aile (18-20 puan) ⭐ YÜKSEK PUAN
   • 3 kardeş → Kalabalık Aile (15-17 puan) ⭐ YÜKSEK PUAN
   • 2 kardeş → Normal (10-14 puan)
   • 1 kardeş → Az (5-9 puan)
   • 0 kardeş → Tek Çocuk (0-4 puan)

4. KLASİK SORULAR (0-15 puan) - ÇOK ÖNEMLİ:
   • Her soruya verilen cevabı DETAYLI ANALİZ ET:
     - İçerik kalitesi (samimiyet, detay, özen)
     - Uzunluk ve kapsamlılık
     - Hedeflerin netliği
     - Maddi durumun açıklığı
   • Detaylı, samimi, özenli cevaplar (150+ karakter) → Yüksek Puan (12-15)
   • Orta detaylı cevaplar (50-150 karakter) → Orta Puan (7-11)
   • Kısa veya umursamaz cevaplar (<50 karakter) → Düşük Puan (0-6)
   • Her soruyu ayrı ayrı değerlendir ve yorumla

5. SINIF (0-10 puan):
   • 4. Sınıf → En Üst Sınıf (9-10 puan)
   • 3. Sınıf → Üst Sınıf (7-8 puan)
   • 2. Sınıf → Orta Sınıf (5-6 puan)
   • 1. Sınıf → Alt Sınıf (3-4 puan)

6. BÖLÜM ZORLUK KATSAYISI:
   • Öğrencinin bölümü: {ogrenci.Bolum ?? "Belirtilmemiş"}
   • Bölüm zorluk katsayısı: {bolumKatsayi:F2}
   • {(bolumKatsayi >= 1.20m ? "Yüksek zorluk seviyesine sahip bir bölüm (Tıp, Diş Hekimliği, Eczacılık vb.)" : bolumKatsayi >= 1.10m ? "Orta-yüksek zorluk seviyesine sahip bir bölüm (Hukuk vb.)" : bolumKatsayi >= 1.05m ? "Orta zorluk seviyesine sahip bir bölüm (Fen-Edebiyat, Ziraat vb.)" : "Standart zorluk seviyesine sahip bir bölüm (İİBF, Eğitim vb.)")}
   • Bu katsayı akademik puan hesaplamasına uygulanmıştır: Akademik Puan = (GNO × 25) × Bölüm Katsayısı

7. ŞEHİR YAŞAM MALİYETİ:
   • Öğrencinin üniversitesi {sehir ?? "bilinmeyen"} şehrinde bulunmaktadır.
   • Bu şehrin yaşam maliyeti katsayısı {sehirKatsayi:F2}'dir.
   • {(sehirKatsayi >= 1.20m ? "Yüksek yaşam maliyeti olan bir şehir" : sehirKatsayi >= 1.00m ? "Orta yaşam maliyeti olan bir şehir" : "Düşük yaşam maliyeti olan bir şehir")}.
   • Bu katsayı final burs puanına çarpılarak uygulanmıştır: Final Puan = (Akademik Puan + Sosyal Puan) × Şehir Katsayısı

═══════════════════════════════════════════════════════════════
                    RAPOR FORMATI
═══════════════════════════════════════════════════════════════

Lütfen aşağıdaki formatta DETAYLI, PROFESYONEL ve OBJEKTİF bir rapor hazırla (EN AZ 800-1000 KELİME):

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
1. GENEL DEĞERLENDİRME SKORU
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Nihai Yüzdelik Skor: [0-100 arası sayı]
Uygunluk Durumu: [Burs almaya çok uygun / Orta düzey uygun / Burs almaya uygun değil]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
2. DETAYLI ANALİZ (15-20 cümle, her kriteri ayrıntılıca açıkla)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
• Not ortalamasını (GNO) detaylıca değerlendir ve bölüm zorluk katsayısının akademik puana etkisini açıkla
• Temel akademik puan hesaplamasını (GNO × 25) ve bölüm katsayısı uygulamasını açıkla
• Aile gelir durumunu kapsamlı analiz et (çok önemli! kaç puan aldı, neden, 2025 Türkiye verilerine göre)
• Kardeş sayısının etkisini açıkla (kaç puan aldı, neden)
• KLASİK SORULARA VERİLEN CEVAPLARI DETAYLI YORUMLA:
  - Soru 1 cevabını analiz et: Samimi mi? Detaylı mı? Hedefler net mi?
  - Soru 2 cevabını analiz et: Gelecek planları açık mı? Gerçekçi mi?
  - Soru 3 cevabını analiz et: Maddi durum açık mı? İhtiyaç belirtilmiş mi?
  - Her soruya kaç puan verildiğini ve nedenini açıkla
• Bölüm zorluk katsayısının akademik puana etkisini detaylıca açıkla
• Şehir yaşam maliyeti katsayısının final puana etkisini açıkla
• Genel durumu objektif bir şekilde özetle

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
3. GÜÇLÜ YÖNLER (5-7 madde)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
• [Madde 1: Öğrencinin burs almaya uygun olan güçlü yönü - detaylı açıkla]
• [Madde 2: Öğrencinin burs almaya uygun olan güçlü yönü - detaylı açıkla]
• [Madde 3: Öğrencinin burs almaya uygun olan güçlü yönü - detaylı açıkla]
• [Madde 4: Klasik sorulara verilen cevaplardan çıkan güçlü yönler]
• [Madde 5: Akademik başarı veya hedeflerden çıkan güçlü yönler]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
4. ZAYIF YÖNLER (3-5 madde)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
• [Madde 1: Öğrencinin burs almaya uygun olmayan zayıf yönü - detaylı açıkla]
• [Madde 2: Öğrencinin burs almaya uygun olmayan zayıf yönü - detaylı açıkla]
• [Madde 3: Klasik sorulara verilen cevaplardan çıkan zayıf yönler]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
5. KLASİK SORULAR ANALİZİ (DETAYLI)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Soru 1 Analizi: ""Bu bursu neden hak ettiğini düşünüyorsun?""
• Cevap Kalitesi: [Detaylı değerlendirme - samimiyet, özen, içerik]
• Puan: [0-5 arası] / 5
• Yorum: [3-4 cümlelik detaylı yorum]

Soru 2 Analizi: ""Gelecek hedeflerin nelerdir?""
• Cevap Kalitesi: [Detaylı değerlendirme - netlik, gerçekçilik, planlama]
• Puan: [0-5 arası] / 5
• Yorum: [3-4 cümlelik detaylı yorum]

Soru 3 Analizi: ""Şu anki maddi/ailesel durumunu kısaca açıklar mısın?""
• Cevap Kalitesi: [Detaylı değerlendirme - açıklık, detay, ihtiyaç belirtme]
• Puan: [0-5 arası] / 5
• Yorum: [3-4 cümlelik detaylı yorum]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
6. ÖNERİLER
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Öğrenciye Yönelik:
• [Madde 1: Öğrenciye yönelik detaylı öneri - 2-3 cümle]
• [Madde 2: Öğrenciye yönelik detaylı öneri - 2-3 cümle]
• [Madde 3: Klasik sorulara verilen cevaplara göre öneri]

Değerlendirme Komitesine Yönelik:
• [Madde 1: Komiteye yönelik detaylı öneri - 2-3 cümle]
• [Madde 2: Komiteye yönelik detaylı öneri - 2-3 cümle]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
7. SONUÇ
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
[5-7 cümlelik genel değerlendirme ve karar özeti. Burs verilip verilmeyeceği konusunda net bir tavsiye içermeli. Tüm kriterleri özetle ve final kararı belirt.]

═══════════════════════════════════════════════════════════════

ÖNEMLİ NOTLAR:
• Her kriteri ayrıntılıca değerlendir ve puan dağılımını açıkla.
• Aile geliri kriteri EN ÖNEMLİ kriterdir - mutlaka detaylı analiz et.
• KLASİK SORULARA VERİLEN CEVAPLARI MUTLAKA DETAYLI YORUMLA - bu çok önemli!
• Objektif, profesyonel ve tutarlı bir dil kullan.
• Türkçe yaz ve emoji kullanma.
• Raporu yukarıdaki formatı takip ederek hazırla.
• EN AZ 800-1000 KELİME uzunluğunda detaylı bir rapor hazırla.";

                var requestBody = new
                {
                    model = "llama-3.3-70b-versatile",
                    messages = new[]
                    {
                        new { role = "system", content = @"Sen bir burs değerlendirme uzmanısın ve profesyonel raporlar hazırlıyorsun.

2025 TÜRKİYE EKONOMİK VERİLERİ (güncel tahmini sınırlar):
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
• Asgari Ücret: ~23.000 TL (net)
• Açlık Sınırı: ~40.000 TL (4 kişilik aile)
• Yoksulluk Sınırı: ~120.000 TL (4 kişilik aile)

KRİTİK PUANLAMA KURALLARI (MUTLAKA UYGULA):
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
1. AİLE GELİRİ (EN ÖNEMLİ KRİTER - 40 PUAN):
   • 0-30.000 TL: 35-40 puan (ACİL BURS - açlık sınırının altı)
   • 30.000-45.000 TL: 28-34 puan (YÜKSEK İHTİYAÇ - asgari ücret civarı/altı)
   • 45.000-70.000 TL: 20-27 puan (İHTİYAÇ VAR)
   • 70.000-110.000 TL: 12-19 puan (ORTA)
   • 110.000-150.000 TL: 6-11 puan (DÜŞÜK - burs ihtiyacı az)
   • 150.000+ TL: 0-5 puan (YOK - burs ihtiyacı yok)

2. NOT ORTALAMASI (30 PUAN):
   • 3.50-4.00: 25-30 puan
   • 3.00-3.49: 20-24 puan
   • 2.50-2.99: 15-19 puan
   • 2.00-2.49: 10-14 puan
   • 2.00 altı: 0-9 puan

3. KARDEŞ SAYISI (20 PUAN):
   • 4+ kardeş: 18-20 puan
   • 3 kardeş: 15-17 puan
   • 2 kardeş: 10-14 puan
   • 1 kardeş: 5-9 puan
   • 0 kardeş: 1-4 puan

4. KLASİK SORULAR (15 PUAN - ÇOK ÖNEMLİ):
   • Her soru 0-5 puan arası değerlendirilir
   • Detaylı, samimi, özenli cevaplar → 4-5 puan
   • Orta detaylı cevaplar → 2-3 puan
   • Kısa veya umursamaz cevaplar → 0-1 puan

5. SINIF (10 PUAN):
   • 4. sınıf: 9-10 puan
   • 3. sınıf: 7-8 puan
   • 2. sınıf: 5-6 puan
   • 1. sınıf: 3-4 puan

TOPLAM: 100 PUAN

ÖNEMLİ:
• Türkçe yaz, emoji kullanma
• Verilen formatı takip et
• Her kriteri açıkça puanla ve toplam skoru hesapla
• KLASİK SORULARA VERİLEN CEVAPLARI MUTLAKA DETAYLI YORUMLA
• Profesyonel ve objektif ol
• GELİR DEĞERLENDİRMESİNDE 2025 TÜRKİYE VERİLERİNİ KULLAN!
• EN AZ 800-1000 KELİME uzunluğunda detaylı rapor hazırla!" },
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 4000,
                    temperature = 0.2
                };

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                    var json = JsonConvert.SerializeObject(requestBody);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync(_apiUrl, content);
                    var responseContent = await response.Content.ReadAsStringAsync();

                    if (response.IsSuccessStatusCode)
                    {
                        dynamic? result = JsonConvert.DeserializeObject(responseContent);
                        if (result != null && result.choices != null && result.choices[0] != null)
                        {
                            // Groq/OpenAI uyumlu: choices[0].message.content
                            var contentText = (string?)result.choices[0].message?.content;
                            return !string.IsNullOrWhiteSpace(contentText)
                                ? contentText
                                : SimuleAnaliz(ogrenci, bursPuani);
                        }
                        return SimuleAnaliz(ogrenci, bursPuani);
                    }
                    else
                    {
                        return $"API Hatası: {response.StatusCode}\n\nYanıt: {responseContent}\n\nSimüle edilmiş analiz:\n{SimuleAnaliz(ogrenci, bursPuani)}";
                    }
                }
            }
            catch (Exception ex)
            {
                return $"Hata oluştu: {ex.Message}\n\nSimüle edilmiş analiz:\n{SimuleAnaliz(ogrenci, bursPuani)}";
            }
        }

        public string SimuleAnaliz(Ogrenci ogrenci, decimal bursPuani)
        {
            // Gerçekçi puan hesaplama
            decimal hesaplananPuan = HesaplaGercekPuan(ogrenci);
            
            // String değerleri int'e çevir
            int kardesSayisi = 0;
            int.TryParse(ogrenci.KardesSayisi, out kardesSayisi);
            
            int sinif = 1;
            int.TryParse(ogrenci.Sinif, out sinif);
            
            string durum;
            string aciklama;
            string gelirDurumu;
            string kardesDurumu;
            string notDurumu;

            // Gelir değerlendirmesi (2025 Türkiye: Asgari Ücret ~23.000 TL, Açlık Sınırı ~40.000 TL)
            if (ogrenci.AileGeliri < 30000)
                gelirDurumu = "Çok düşük gelir - açlık sınırının altında, ACİL BURS GEREKLİ (+35-40 puan)";
            else if (ogrenci.AileGeliri < 45000)
                gelirDurumu = "Düşük gelir - asgari ücret civarı/altı, bursa yüksek ihtiyaç (+28-34 puan)";
            else if (ogrenci.AileGeliri < 70000)
                gelirDurumu = "Orta-düşük gelir - ihtiyaç var (+20-27 puan)";
            else if (ogrenci.AileGeliri < 110000)
                gelirDurumu = "Orta gelir (+12-19 puan)";
            else if (ogrenci.AileGeliri < 150000)
                gelirDurumu = "Orta-üst gelir - bursa ihtiyacı düşük (+6-11 puan)";
            else
                gelirDurumu = "Yüksek gelir - bursa ihtiyacı yok (+0-5 puan)";

            // Kardeş değerlendirmesi
            if (kardesSayisi >= 4)
                kardesDurumu = $"{kardesSayisi} kardeş - kalabalık aile (+16-20 puan)";
            else if (kardesSayisi >= 3)
                kardesDurumu = $"{kardesSayisi} kardeş - orta kalabalık (+12-15 puan)";
            else if (kardesSayisi >= 2)
                kardesDurumu = $"{kardesSayisi} kardeş (+8-11 puan)";
            else
                kardesDurumu = $"{kardesSayisi} kardeş - tek/az kardeş (+0-7 puan)";

            // Not değerlendirmesi
            if (ogrenci.NotOrtalamasi >= 3.5m)
                notDurumu = $"GNO: {ogrenci.NotOrtalamasi:F2} - Mükemmel akademik başarı (+25-30 puan)";
            else if (ogrenci.NotOrtalamasi >= 3.0m)
                notDurumu = $"GNO: {ogrenci.NotOrtalamasi:F2} - İyi akademik başarı (+20-24 puan)";
            else if (ogrenci.NotOrtalamasi >= 2.5m)
                notDurumu = $"GNO: {ogrenci.NotOrtalamasi:F2} - Orta akademik başarı (+15-19 puan)";
            else
                notDurumu = $"GNO: {ogrenci.NotOrtalamasi:F2} - Düşük akademik başarı (+0-14 puan)";

            if (hesaplananPuan >= 70)
            {
                durum = "BURS ALMAYA ÇOK UYGUN";
                aciklama = $"Öğrenci burs almaya yüksek düzeyde uygundur. Düşük aile geliri ({ogrenci.AileGeliri:N0} TL) ve akademik başarısı ({ogrenci.NotOrtalamasi:F2}/4.00) bursa uygunluğunu desteklemektedir.";
            }
            else if (hesaplananPuan >= 50)
            {
                durum = "ORTA DÜZEY UYGUN";
                aciklama = $"Öğrenci orta düzeyde burs almaya uygundur. Aile geliri ({ogrenci.AileGeliri:N0} TL) ve diğer kriterler incelendiğinde değerlendirme yapılabilir.";
            }
            else if (hesaplananPuan >= 30)
            {
                durum = "DÜŞÜK UYGUNLUK";
                aciklama = $"Öğrencinin burs uygunluğu düşüktür. Aile geliri ({ogrenci.AileGeliri:N0} TL) nispeten yüksek veya akademik başarı yetersiz görünmektedir.";
            }
            else
            {
                durum = "BURS ALMAYA UYGUN DEĞİL";
                aciklama = $"Öğrenci burs kriterlerini karşılamamaktadır. Yüksek aile geliri ({ogrenci.AileGeliri:N0} TL) veya düşük akademik başarı nedeniyle bursa uygun değildir.";
            }

            return $@"━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
BURS DEĞERLENDİRME RAPORU
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

1) NİHAİ SKOR: {hesaplananPuan:F0}/100
   Uygunluk Durumu: {durum}

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
2) KRİTER ANALİZİ
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

• Aile Geliri: {ogrenci.AileGeliri:N0} TL/ay
  {gelirDurumu}

• Kardeş Durumu: {kardesDurumu}

• Akademik: {notDurumu}

• Sınıf: {sinif}. sınıf

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
3) DEĞERLENDİRME
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
{aciklama}

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
4) ÖNERİLER
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
{(hesaplananPuan >= 50 ? "• Bu öğrenci burs için değerlendirilebilir.\n• Ek belgeler istenebilir." : "• Öğrencinin durumu yeniden değerlendirilmeli.\n• Alternatif destek programları önerilebilir.")}

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━";
        }

        private decimal HesaplaGercekPuan(Ogrenci ogrenci)
        {
            decimal puan = 0;

            // String değerleri int'e çevir
            int kardesSayisi = 0;
            int.TryParse(ogrenci.KardesSayisi, out kardesSayisi);
            
            int sinif = 1;
            int.TryParse(ogrenci.Sinif, out sinif);

            // 1. Aile Geliri Puanı (40 puan - EN ÖNEMLİ)
            // 2025 Türkiye: Asgari Ücret ~23.000 TL, Açlık Sınırı ~40.000 TL
            if (ogrenci.AileGeliri < 30000)
                puan += 38;  // Açlık sınırının altında - ACİL BURS
            else if (ogrenci.AileGeliri < 45000)
                puan += 33;  // Asgari ücret civarı/altı - YÜKSEK İHTİYAÇ
            else if (ogrenci.AileGeliri < 70000)
                puan += 25;  // Orta-düşük gelir - İHTİYAÇ VAR
            else if (ogrenci.AileGeliri < 110000)
                puan += 17;  // Orta gelir
            else if (ogrenci.AileGeliri < 150000)
                puan += 9;   // Orta-üst gelir - burs ihtiyacı düşük
            else
                puan += 3;   // Yüksek gelir - burs ihtiyacı yok

            // 2. Not Ortalaması Puanı (30 puan)
            if (ogrenci.NotOrtalamasi >= 3.5m)
                puan += 28;
            else if (ogrenci.NotOrtalamasi >= 3.0m)
                puan += 22;
            else if (ogrenci.NotOrtalamasi >= 2.5m)
                puan += 16;
            else if (ogrenci.NotOrtalamasi >= 2.0m)
                puan += 10;
            else
                puan += 5;

            // 3. Kardeş Sayısı Puanı (20 puan)
            if (kardesSayisi >= 4)
                puan += 18;
            else if (kardesSayisi >= 3)
                puan += 14;
            else if (kardesSayisi >= 2)
                puan += 10;
            else if (kardesSayisi >= 1)
                puan += 5;
            else
                puan += 2;

            // 4. Sınıf Puanı (10 puan)
            if (sinif >= 4)
                puan += 9;
            else if (sinif >= 3)
                puan += 7;
            else if (sinif >= 2)
                puan += 5;
            else
                puan += 3;

            return Math.Min(puan, 100);
        }
    }
}

