# Groq AI API Entegrasyonu - Kullanım Kılavuzu

## Ücretsiz API Key Alma

1. **Groq Console'a Git**: https://console.groq.com/
2. **Hesap Oluştur**: Ücretsiz hesap oluşturun (e-posta ile kayıt)
3. **API Keys Sekmesine Git**: Sol menüden "API Keys" seçeneğine tıklayın
4. **Yeni API Key Oluştur**: "Create API Key" butonuna tıklayın
5. **API Key'i Kopyala**: Oluşturulan API key'i kopyalayın

## API Key'i Yapılandırma

1. `BursOtomasyon.Web/appsettings.json` dosyasını açın
2. `GroqApi` bölümündeki `ApiKey` alanına API key'inizi yapıştırın:

```json
{
  "GroqApi": {
    "ApiKey": ""
  }
}
```

## API Endpoint'leri

### 1. Öğrenci ID ile Analiz Yapma

**Endpoint**: `POST /api/AIAnaliz/analiz-yap`

**Request Body**:
```json
{
  "ogrenciID": 1
}
```

**Response**:
```json
{
  "success": true,
  "ogrenciId": 1,
  "ogrenciAdi": "Ahmet Yılmaz",
  "bursPuani": 75.50,
  "analiz": "Uygunluk Durumu: Burs almaya çok uygun\n\nAçıklama: ..."
}
```

### 2. Detaylı Öğrenci Bilgileri ile Analiz Yapma

**Endpoint**: `POST /api/AIAnaliz/analiz-yap-detayli`

**Request Body**:
```json
{
  "ad": "Ahmet",
  "soyad": "Yılmaz",
  "notOrtalamasi": 3.5,
  "aileGeliri": 8000,
  "kardesSayisi": "2",
  "sinif": "3",
  "universite": "İstanbul Üniversitesi",
  "fakulte": "Mühendislik Fakültesi",
  "bolum": "Bilgisayar Mühendisliği",
  "anneMeslek": "Öğretmen",
  "babaMeslek": "Mühendis"
}
```

**Response**:
```json
{
  "success": true,
  "bursPuani": 75.50,
  "analiz": "Uygunluk Durumu: Burs almaya çok uygun\n\nAçıklama: ..."
}
```

## JavaScript ile Kullanım Örneği

```javascript
// Öğrenci ID ile analiz yapma
async function analizYap(ogrenciId) {
    const response = await fetch('/api/AIAnaliz/analiz-yap', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify({ ogrenciID: ogrenciId })
    });
    
    const result = await response.json();
    console.log(result.analiz);
    return result;
}

// Detaylı analiz yapma
async function detayliAnalizYap(ogrenciBilgileri) {
    const response = await fetch('/api/AIAnaliz/analiz-yap-detayli', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(ogrenciBilgileri)
    });
    
    const result = await response.json();
    return result;
}
```

## C# HttpClient ile Kullanım Örneği

```csharp
using System.Net.Http.Json;

var client = new HttpClient();
var request = new { ogrenciID = 1 };

var response = await client.PostAsJsonAsync(
    "https://localhost:5001/api/AIAnaliz/analiz-yap", 
    request
);

var result = await response.Content.ReadFromJsonAsync<dynamic>();
Console.WriteLine(result.analiz);
```

## Notlar

- Groq API ücretsizdir ve hızlıdır
- API key olmadan da çalışır (simüle edilmiş analiz döner)
- Rate limit: Groq'un ücretsiz planında dakikada belirli sayıda istek yapabilirsiniz
- Model: `llama-3.1-8b-instant` kullanılmaktadır (hızlı ve ücretsiz)

