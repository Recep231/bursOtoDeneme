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
                    model = "llama-3.1-8b-instant",
                    messages = new[]
                    {
                        new { role = "system", content = "Sen bir burs puanlama uzmanısın. SADECE sayısal puan döndürürsün (0-100 arası). Başka metin, açıklama, noktalama işareti EKLEME. Sadece sayıyı yaz. Örnek: 75 veya 82.5" },
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 20, // Daha kısa yanıt için
                    temperature = 0.2 // Daha tutarlı sonuçlar için
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

KLASİK SORULAR:
• Soru 1 Cevabı: {(string.IsNullOrWhiteSpace(ogrenci.KlasikSoru1Cevap) ? "Cevap verilmemiş" : ogrenci.KlasikSoru1Cevap.Length > 200 ? ogrenci.KlasikSoru1Cevap.Substring(0, 200) + "..." : ogrenci.KlasikSoru1Cevap)}
• Soru 2 Cevabı: {(string.IsNullOrWhiteSpace(ogrenci.KlasikSoru2Cevap) ? "Cevap verilmemiş" : ogrenci.KlasikSoru2Cevap.Length > 200 ? ogrenci.KlasikSoru2Cevap.Substring(0, 200) + "..." : ogrenci.KlasikSoru2Cevap)}
• Soru 3 Cevabı: {(string.IsNullOrWhiteSpace(ogrenci.KlasikSoru3Cevap) ? "Cevap verilmemiş" : ogrenci.KlasikSoru3Cevap.Length > 200 ? ogrenci.KlasikSoru3Cevap.Substring(0, 200) + "..." : ogrenci.KlasikSoru3Cevap)}

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

2. AİLE GELİRİ (0-30 puan) - EN ÖNEMLİ KRİTER:
   • 5.000 TL altı → Çok Düşük Gelir (25-30 puan) ⭐ YÜKSEK PUAN
   • 5.000-10.000 TL → Düşük Gelir (20-24 puan) ⭐ YÜKSEK PUAN
   • 10.000-15.000 TL → Orta-Düşük Gelir (15-19 puan)
   • 15.000-25.000 TL → Orta Gelir (10-14 puan)
   • 25.000-40.000 TL → Yüksek Gelir (5-9 puan) ⚠️ DÜŞÜK PUAN
   • 40.000 TL üstü → Çok Yüksek Gelir (0-4 puan) ⚠️ ÇOK DÜŞÜK PUAN

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
2. DETAYLI ANALİZ (8-10 cümle)
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
• Not ortalamasını (GNO) detaylıca değerlendir ve bölüm zorluk katsayısının akademik puana etkisini açıkla
• Temel akademik puan hesaplamasını (GNO × 25) ve bölüm katsayısı uygulamasını açıkla
• Aile gelir durumunu kapsamlı analiz et (çok önemli! kaç puan aldı, neden)
• Kardeş sayısının etkisini açıkla (kaç puan aldı, neden)
• Klasik sorulara verilen cevapları değerlendir (kaç puan aldı, neden)
• Bölüm zorluk katsayısının akademik puana etkisini detaylıca açıkla
• Şehir yaşam maliyeti katsayısının final puana etkisini açıkla
• Genel durumu objektif bir şekilde özetle

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
3. GÜÇLÜ YÖNLER
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
• [Madde 1: Öğrencinin burs almaya uygun olan güçlü yönü]
• [Madde 2: Öğrencinin burs almaya uygun olan güçlü yönü]
• [Madde 3: Öğrencinin burs almaya uygun olan güçlü yönü]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
4. ZAYIF YÖNLER
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
• [Madde 1: Öğrencinin burs almaya uygun olmayan zayıf yönü]
• [Madde 2: Öğrencinin burs almaya uygun olmayan zayıf yönü]
• [Madde 3: Öğrencinin burs almaya uygun olmayan zayıf yönü]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
5. ÖNERİLER
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
Öğrenciye Yönelik:
• [Madde 1: Öğrenciye yönelik detaylı öneri]
• [Madde 2: Öğrenciye yönelik detaylı öneri]

Değerlendirme Komitesine Yönelik:
• [Madde 1: Komiteye yönelik detaylı öneri]
• [Madde 2: Komiteye yönelik detaylı öneri]

━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
6. SONUÇ
━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
[3-4 cümlelik genel değerlendirme ve karar özeti. Burs verilip verilmeyeceği konusunda net bir tavsiye içermeli.]

═══════════════════════════════════════════════════════════════

ÖNEMLİ NOTLAR:
• Her kriteri ayrıntılıca değerlendir ve puan dağılımını açıkla.
• Aile geliri kriteri EN ÖNEMLİ kriterdir - mutlaka detaylı analiz et.
• Objektif, profesyonel ve tutarlı bir dil kullan.
• Türkçe yaz ve emoji kullanma.
• Raporu yukarıdaki formatı takip ederek hazırla.";

                var requestBody = new
                {
                    model = "llama-3.1-70b-versatile", // En güçlü model detaylı analiz için
                    messages = new[]
                    {
                        new { role = "system", content = "Sen bir burs değerlendirme uzmanısın ve profesyonel raporlar hazırlıyorsun. ÖNEMLİ KURALLAR: 1) Düşük aile geliri = yüksek burs puanı, yüksek aile geliri = düşük burs puanı. 2) Çok kardeş = yüksek puan. 3) Yüksek not = yüksek puan. 4) Detaylı cevaplar = yüksek puan. 5) Her kriteri detaylıca analiz et ve puan dağılımını açıkla. 6) Objektif, tutarlı ve profesyonel bir dil kullan. 7) Türkçe yaz ve emoji kullanma. 8) Verilen formatı mutlaka takip et." },
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 2000, // Çok daha uzun ve detaylı rapor için
                    temperature = 0.3 // Daha tutarlı ve objektif sonuçlar için
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
                                // Eğer çok kısa bir yanıt gelirse, normal analizi dene
                                if (contentText.Trim().Length < 100)
                                {
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
            catch (Exception ex)
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
                var prompt = $@"Aşağıdaki öğrenci bilgilerini analiz ederek burs uygunluk durumunu değerlendir:

Öğrenci Bilgileri:
- Ad Soyad: {ogrenci.Ad} {ogrenci.Soyad}
- Not Ortalaması: {ogrenci.NotOrtalamasi}/4.00
- Aile Aylık Geliri: {ogrenci.AileGeliri} TL
- Kardeş Sayısı: {ogrenci.KardesSayisi}
- Sınıf: {ogrenci.Sinif}
- Üniversite: {ogrenci.Universite}
- Bölüm: {ogrenci.Bolum}
- Hesaplanan Burs Puanı: {bursPuani:F2}/100

ÖNEMLİ DEĞERLENDİRME KRİTERLERİ:
1. NOT ORTALAMASI: Yüksek not (3.0+) = YÜKSEK PUAN. Düşük not (2.0 altı) = DÜŞÜK PUAN.
2. AİLE GELİRİ: DÜŞÜK gelir (10.000 TL altı) = YÜKSEK PUAN. YÜKSEK gelir (25.000 TL üstü) = DÜŞÜK PUAN. Bu çok önemli!
3. KARDEŞ SAYISI: ÇOK kardeş (3+) = YÜKSEK PUAN. Az kardeş (0-1) = DÜŞÜK PUAN.
4. SINIF: Üst sınıflar (3-4) = Biraz daha yüksek puan.

Lütfen şu formatta analiz yap:
1) Nihai Yüzdelik Skor (0-100 arası): Yukarıdaki kriterlere göre objektif değerlendir. Yüksek gelirli öğrenciler düşük puan almalı, düşük gelirli öğrenciler yüksek puan almalı.
2) Uygunluk Durumu: 'Burs almaya çok uygun', 'Orta düzey uygun' veya 'Burs almaya uygun değil'.
3) Kısa Açıklama: 2-3 cümleyle neden bu skoru verdiğini açıkla. Gelir durumunu mutlaka belirt.
4) Öneriler: Varsa 1-2 maddelik öneri ver.

Sadece bu dört maddeyi döndür; başka metin ekleme.";

                var requestBody = new
                {
                    model = "llama-3.1-8b-instant",
                    messages = new[]
                    {
                        new { role = "system", content = "Sen bir burs değerlendirme uzmanısın. ÖNEMLİ: Düşük aile geliri = yüksek burs puanı, yüksek aile geliri = düşük burs puanı. Çok kardeş = yüksek puan. Yüksek not = yüksek puan. Bu kriterlere göre objektif ve tutarlı değerlendirmeler yap. Türkçe cevap ver." },
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 400,
                    temperature = 0.5
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
            string durum;
            string aciklama;

            if (bursPuani >= 70)
            {
                durum = "Burs almaya çok uygun";
                aciklama = $"Öğrencinin not ortalaması ({ogrenci.NotOrtalamasi}/4.00) ve aile gelir durumu ({ogrenci.AileGeliri} TL) burs almaya uygun görünmektedir. {ogrenci.KardesSayisi} kardeş durumu da değerlendirmeye olumlu katkı sağlamaktadır.";
            }
            else if (bursPuani >= 50)
            {
                durum = "Orta düzey uygun";
                aciklama = $"Öğrencinin durumu genel olarak orta seviyededir. Not ortalaması ({ogrenci.NotOrtalamasi}/4.00) ve aile geliri ({ogrenci.AileGeliri} TL) dikkate alındığında, diğer başvurularla karşılaştırılarak değerlendirilmesi önerilir.";
            }
            else
            {
                durum = "Burs almaya uygun değil";
                aciklama = $"Öğrencinin not ortalaması ({ogrenci.NotOrtalamasi}/4.00) veya aile gelir durumu ({ogrenci.AileGeliri} TL) burs kriterlerini karşılamamaktadır. Daha yüksek not ortalaması veya daha düşük aile geliri durumunda tekrar değerlendirilebilir.";
            }

            return $"Uygunluk Durumu: {durum}\n\nAçıklama: {aciklama}\n\nHesaplanan Burs Puanı: {bursPuani}/100";
        }
    }
}

