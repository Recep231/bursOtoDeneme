using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using BursOtomasyon.Web.Models;

namespace BursOtomasyon.Web.Services
{
    public class AIAnalizService
    {
        private readonly HttpClient _httpClient;
        private readonly string? _apiKey;
        private readonly string _apiUrl = "https://api.groq.com/openai/v1/chat/completions";

        public AIAnalizService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            // Öncelik sırası: appsettings.json -> GROQ_API_KEY env -> OPENAI_API_KEY env (yedek)
            _apiKey = configuration["GroqApi:ApiKey"]
                      ?? Environment.GetEnvironmentVariable("GROQ_API_KEY")
                      ?? Environment.GetEnvironmentVariable("OPENAI_API_KEY");

            if (!string.IsNullOrEmpty(_apiKey))
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
        }

        public async Task<string> AskAssistantAsync(string question)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                return "AI API anahtarı bulunamadı. Lütfen appsettings.json dosyasına GroqApi:ApiKey ekleyin veya GROQ_API_KEY ortam değişkenini ayarlayın.";
            }

            try
            {
                var requestBody = new
                {
                    model = "llama-3.1-70b",
                    messages = new[]
                    {
                        new { role = "system", content = "Sen burs otomasyonu konusunda uzman bir asistansın. Öğrenci burs başvurusu, gereklilikler, not ortalaması, gelir durumu, kardeş sayısı, sınıf, üniversite/fakülte/bölüm, başvuru durumu gibi konularda detaylı, gerekçeli ve açıklayıcı cevaplar ver.\nAnalizlerinde derinlik ve insani yorum kullan." },
                        new { role = "user", content = question }
                    },
                    max_tokens = 1000,
                    temperature = 0.5
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_apiUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    using var doc = JsonDocument.Parse(responseContent);
                    var root = doc.RootElement;

                    if (root.TryGetProperty("choices", out var choices) &&
                        choices.GetArrayLength() > 0 &&
                        choices[0].TryGetProperty("message", out var message) &&
                        message.TryGetProperty("content", out var contentElement))
                    {
                        return contentElement.GetString() ?? "Cevap alınamadı.";
                    }

                    return "Cevap alınamadı.";
                }
                else
                {
                    return $"API Hatası: {response.StatusCode} - {responseContent}";
                }
            }
            catch (Exception ex)
            {
                return $"Hata oluştu: {ex.Message}";
            }
        }

        public async Task<string> AnalizYapAsync(OgrenciModel ogrenci, decimal bursPuani)
        {
            if (string.IsNullOrEmpty(_apiKey))
            {
                return "AI API anahtarı bulunamadı. Lütfen appsettings.json dosyasına GroqApi:ApiKey ekleyin veya GROQ_API_KEY ortam değişkenini ayarlayın.\n\n" +
                       "Ücretsiz API key almak için: https://console.groq.com/";
            }

            try
            {
                var prompt = $@"Aşağıdaki öğrenci bilgilerini DETAYLI ve OBJEKTİF bir şekilde analiz ederek burs uygunluk durumunu değerlendir:

ÖĞRENCİ BİLGİLERİ:
- Ad Soyad: {ogrenci.Ad} {ogrenci.Soyad}
- Not Ortalaması: {ogrenci.NotOrtalamasi}/4.00 (NOT: 4.00 üzerinden değerlendir. 3.00 ve üzeri iyi, 2.50-2.99 orta, 2.50 altı düşük sayılır. Lütfen bu ölçeği doğru kullan!)
- Aile Aylık Geliri: {ogrenci.AileGeliri} TL (NOT: Türkiye'de asgari ücret ve ortalama gelir düzeyini dikkate al. 5000 TL altı çok düşük, 5000-10000 TL düşük, 10000-20000 TL orta, 20000+ TL yüksek sayılır)
- Kardeş Sayısı: {ogrenci.KardesSayisi} (NOT: Kardeş sayısı fazla olması maddi yükü artırır, bu pozitif bir faktördür)
- Sınıf: {ogrenci.Sinif}
- Üniversite: {ogrenci.Universite}
- Fakülte: {ogrenci.Fakulte}
- Bölüm: {ogrenci.Bolum}
- Anne Mesleği: {(string.IsNullOrEmpty(ogrenci.AnneMeslek) ? "Belirtilmemiş" : ogrenci.AnneMeslek)}
- Baba Mesleği: {(string.IsNullOrEmpty(ogrenci.BabaMeslek) ? "Belirtilmemiş" : ogrenci.BabaMeslek)}

KLASİK SORULARA VERİLEN CEVAPLAR (ÇOK ÖNEMLİ - BU CEVAPLARI MUTLAKA ANALİZ ET):
1. Bu bursu neden hak ettiğini düşünüyorsun?
Cevap: {(string.IsNullOrEmpty(ogrenci.KlasikSoru1Cevap) ? "CEVAP VERİLMEMİŞ" : ogrenci.KlasikSoru1Cevap)}

2. Gelecek hedeflerin nelerdir?
Cevap: {(string.IsNullOrEmpty(ogrenci.KlasikSoru2Cevap) ? "CEVAP VERİLMEMİŞ" : ogrenci.KlasikSoru2Cevap)}

3. Şu anki maddi/ailesel durumunu kısaca açıklar mısın?
Cevap: {(string.IsNullOrEmpty(ogrenci.KlasikSoru3Cevap) ? "CEVAP VERİLMEMİŞ" : ogrenci.KlasikSoru3Cevap)}

DEĞERLENDİRME TALİMATLARI:
- Klasik sorulara verilen cevapları MUTLAKA analiz et. Cevap uzunluğu, içerik kalitesi, samimiyet, detay seviyesi gibi faktörleri değerlendir.
- Çok kısa (toplam 100 karakterden az), umursamaz, kopyala-yapıştır gibi görünen cevaplar NEGATİF etki yaratmalı.
- Detaylı, samimi, düşünülmüş, özenli cevaplar POZİTİF etki yaratmalı.
- Not ortalamasını DOĞRU değerlendir: 3.00/4.00 iyi bir nottur, 'çok düşük' demek yanlış olur!
- Aile gelirini Türkiye şartlarına göre değerlendir.
- Tüm faktörleri dengeli bir şekilde değerlendir.

LÜTFEN ŞU FORMATTA ANALİZ YAP:
1) Nihai Yüzdelik Skor (0-100 arası): Tüm faktörleri (not ortalaması, aile geliri, kardeş sayısı, sınıf, bölüm, özellikle klasik sorulara verilen cevapların kalitesi ve samimiyeti) ağırlıklı olarak değerlendir. Yüzde olarak tek sayı ver (örn: 75).

2) Uygunluk Durumu: 'Burs almaya çok uygun', 'Orta düzey uygun' veya 'Burs almaya uygun değil' şeklinde belirt.

3) Detaylı Açıklama: 4-5 cümleyle neden bu skoru verdiğini POZİTİF ve YAPICI bir şekilde açıkla. Özellikle:
   - Not ortalamasını nasıl değerlendirdiğini belirt (3.00+ ise 'iyi bir başarı', 2.50-2.99 ise 'geliştirilebilir', 2.50 altı ise 'çaba gösterilebilir' gibi pozitif ifadeler kullan)
   - Aile gelir durumunu nasıl yorumladığını belirt (düşük gelir durumunu 'maddi zorluklar' olarak ifade et, umutsuzluk verme)
   - Klasik sorulara verilen cevapları DETAYLI analiz et (uzunluk, içerik kalitesi, samimiyet, özen seviyesi). Eğer cevaplar kısa/umursamaz ise 'daha detaylı açıklama yapılabilir' gibi yapıcı ifadeler kullan, asla 'kötü' veya 'yetersiz' gibi sert ifadeler kullanma
   - Diğer faktörleri de değerlendir
   - Her zaman öğrenciyi destekleyici ve motive edici bir dil kullan

4) Öneriler: Öğrenciye yönelik 2-3 maddelik YAPICI ve MOTİVE EDİCİ öneri ver. Her öneri pozitif ve umut verici olsun.

Sadece bu dört maddeyi döndür; başka metin ekleme.";

                var requestBody = new
                {
                    model = "llama-3.1-70b",
                    messages = new[]
                    {
                        new { role = "system", content = "Sen deneyimli, pozitif ve yapıcı bir burs değerlendirme uzmanısın. Öğrenci bilgilerini objektif, adil, detaylı ve YAPICI bir şekilde analiz edersin. Her zaman öğrenciyi destekleyici ve motive edici bir dil kullanırsın. Not ortalamasını 4.00 üzerinden doğru değerlendirirsin (3.00+ iyi, 2.50-2.99 orta, 2.50 altı düşük). Aile gelirini Türkiye şartlarına göre değerlendirirsin. Klasik sorulara verilen cevapları içerik kalitesi, samimiyet, detay seviyesi açısından derinlemesine analiz edersin. Olumsuz durumları bile pozitif ve yapıcı bir şekilde ifade edersin. Türkçe cevap verirsin. Asla sert, kırıcı veya umutsuzluk veren ifadeler kullanmazsın." },
                        new { role = "user", content = prompt }
                    },
                    max_tokens = 800,
                    temperature = 0.7
                };

                var json = JsonSerializer.Serialize(requestBody);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync(_apiUrl, content);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    using var doc = JsonDocument.Parse(responseContent);
                    var root = doc.RootElement;
                    
                    if (root.TryGetProperty("choices", out var choices) && 
                        choices.GetArrayLength() > 0 &&
                        choices[0].TryGetProperty("message", out var message) &&
                        message.TryGetProperty("content", out var contentElement))
                    {
                        var aiResponse = contentElement.GetString();
                        if (!string.IsNullOrEmpty(aiResponse))
                        {
                            return aiResponse;
                        }
                    }
                    
                    return "AI analizi alınamadı. Lütfen tekrar deneyin veya yöneticiye başvurun.";
                }
                else
                {
                    return $"AI API Hatası: {response.StatusCode}. Lütfen API anahtarınızı kontrol edin veya yöneticiye başvurun.";
                }
            }
            catch (Exception ex)
            {
                return $"Hata oluştu: {ex.Message}. Lütfen tekrar deneyin veya yöneticiye başvurun.";
            }
        }

    }
}

