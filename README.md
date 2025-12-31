# 🎓 Öğrenci Burs Yönetim Sistemi

Bu proje, öğrenci burs başvurularını yönetmek için geliştirilmiş kapsamlı bir sistemdir. Masaüstü uygulaması (WinForms + DevExpress) ve web başvuru sayfası içerir.

## 📋 İçindekiler

- [Özellikler](#özellikler)
- [Teknolojiler](#teknolojiler)
- [Kurulum](#kurulum)
- [Kullanım](#kullanım)
- [Proje Yapısı](#proje-yapısı)
- [Veritabanı](#veritabanı)
- [AI Entegrasyonu](#ai-entegrasyonu)

## ✨ Özellikler

### Masaüstü Uygulaması
- ✅ DevExpress RibbonControl ile modern arayüz
- ✅ Öğrenci yönetimi (Ekle, Güncelle, Sil, Listele)
- ✅ Başvuru yönetimi (Listele, Detay, Onayla, Reddet)
- ✅ Otomatik burs puanlama sistemi (0-100 puan)
- ✅ AI analiz entegrasyonu (OpenAI API)
- ✅ AI analiz raporları görüntüleme
- ✅ Admin yönetim paneli

### Web Başvuru Sayfası
- ✅ Öğrenci burs başvuru formu
- ✅ Otomatik burs puanı hesaplama
- ✅ Modern ve responsive tasarım
- ✅ Form validasyonu

## 🛠 Teknolojiler

### Masaüstü Uygulaması
- **.NET 6.0** - Framework
- **C# WinForms** - UI Framework
- **DevExpress WinForms** - UI Components (RibbonControl)
- **SQL Server** - Veritabanı
- **OpenAI API** - AI Analiz

### Web Uygulaması
- **ASP.NET Core 6.0** - Web Framework
- **MVC Pattern** - Mimari
- **SQL Server** - Veritabanı

## 📦 Kurulum

### Gereksinimler
- Visual Studio 2022 veya üzeri
- SQL Server (Express veya üzeri)
- .NET 6.0 SDK
- DevExpress WinForms lisansı (veya trial)

### Adımlar

1. **Veritabanını Oluştur**
   ```sql
   -- Database/Schema.sql dosyasını SQL Server Management Studio'da çalıştırın
   ```

2. **Bağlantı Stringlerini Güncelle**
   - `BursOtomasyon.Desktop/Data/DatabaseHelper.cs` dosyasında connection string'i düzenleyin
   - `BursOtomasyon.Web/Services/*.cs` dosyalarında connection string'i düzenleyin

3. **DevExpress NuGet Paketlerini Yükle**
   ```bash
   # Masaüstü projesinde DevExpress paketleri zaten .csproj dosyasında tanımlı
   # Visual Studio'da restore işlemi otomatik yapılacaktır
   ```

4. **AI API Anahtarını Ayarla (Opsiyonel)**
   - OpenAI API anahtarınızı environment variable olarak ayarlayın:
     ```bash
     set OPENAI_API_KEY=your_api_key_here
     ```
   - Veya `AIAnalizService.cs` dosyasında direkt olarak ayarlayabilirsiniz

5. **Projeleri Derle**
   ```bash
   dotnet build BursOtomasyon.Desktop/BursOtomasyon.Desktop.csproj
   dotnet build BursOtomasyon.Web/BursOtomasyon.Web.csproj
   ```

## 🚀 Kullanım

### Masaüstü Uygulaması

1. **Uygulamayı Başlat**
   ```bash
   cd BursOtomasyon.Desktop
   dotnet run
   ```

2. **RibbonControl Menüleri**
   - **Öğrenciler**: Öğrenci ekleme, güncelleme, silme ve listeleme
   - **Başvurular**: Başvuru listeleme, detay görüntüleme, onaylama/reddetme
   - **AI Analiz & Rapor**: AI analiz yapma, burs puanı hesaplama, rapor görüntüleme
   - **Yönetim**: Admin girişi ve yönetimi

3. **Varsayılan Admin Bilgileri**
   - Kullanıcı Adı: `admin`
   - Şifre: `admin123`

### Web Başvuru Sayfası

1. **Web Uygulamasını Başlat**
   ```bash
   cd BursOtomasyon.Web
   dotnet run
   ```

2. **Tarayıcıda Aç**
   - Varsayılan URL: `https://localhost:5001` veya `http://localhost:5000`

3. **Başvuru Formunu Doldur**
   - Tüm zorunlu alanları doldurun
   - Form gönderildiğinde otomatik olarak burs puanı hesaplanır
   - Başvuru veritabanına kaydedilir

## 📁 Proje Yapısı

```
BursOtomasyoon/
├── Database/
│   └── Schema.sql                 # Veritabanı şema dosyası
├── BursOtomasyon.Desktop/         # Masaüstü uygulaması
│   ├── Forms/                     # Form dosyaları
│   │   ├── MainForm.cs           # Ana form (RibbonControl)
│   │   ├── OgrenciEkleForm.cs    # Öğrenci ekleme formu
│   │   ├── OgrenciListeForm.cs   # Öğrenci listeleme formu
│   │   ├── BasvuruListeForm.cs   # Başvuru listeleme formu
│   │   ├── AIAnalizForm.cs       # AI analiz formu
│   │   └── ...
│   ├── Models/                    # Veri modelleri
│   ├── Services/                  # İş mantığı servisleri
│   ├── Data/                      # Veritabanı helper
│   └── Program.cs                 # Uygulama giriş noktası
├── BursOtomasyon.Web/             # Web uygulaması
│   ├── Controllers/               # MVC Controller'lar
│   ├── Models/                    # ViewModel'ler
│   ├── Services/                  # İş mantığı servisleri
│   ├── Views/                     # Razor view'lar
│   └── Program.cs                 # Web uygulama giriş noktası
└── README.md                      # Bu dosya
```

## 🗄 Veritabanı

### Tablolar

1. **Ogrenciler**
   - Öğrenci bilgileri ve hesaplanan burs puanı

2. **Basvurular**
   - Başvuru kayıtları ve AI yorumları

3. **Adminler**
   - Admin kullanıcı bilgileri

### Varsayılan Veriler
- Admin kullanıcısı otomatik oluşturulur (admin/admin123)

## 🤖 AI Entegrasyonu

Sistem OpenAI API kullanarak öğrenci başvurularını analiz eder:

- **Analiz Kriterleri:**
  - Not ortalaması
  - Aile gelir durumu
  - Kardeş sayısı
  - Sınıf durumu
  - Hesaplanan burs puanı

- **AI Çıktıları:**
  - "Burs almaya çok uygun"
  - "Orta düzey uygun"
  - "Burs almaya uygun değil"
  - Detaylı açıklama ve öneriler

**Not:** API anahtarı yoksa sistem simüle edilmiş analiz üretir.

## 📊 Burs Puanlama Sistemi

Burs puanı 0-100 arası hesaplanır:

- **Not Ortalaması** (0-40 puan): 4.00 = 40 puan
- **Aile Geliri** (0-30 puan): Düşük gelir = Yüksek puan
- **Kardeş Sayısı** (0-20 puan): Fazla kardeş = Yüksek puan
- **Sınıf Durumu** (0-10 puan): Üst sınıf = Yüksek puan

## 🔧 Yapılandırma

### Veritabanı Bağlantı Stringi
```csharp
// Desktop: BursOtomasyon.Desktop/Data/DatabaseHelper.cs
ConnectionString = "Server=localhost;Database=BursOtomasyonDB;Integrated Security=True;TrustServerCertificate=True;"

// Web: BursOtomasyon.Web/Services/*.cs
_connectionString = "Server=localhost;Database=BursOtomasyonDB;Integrated Security=True;TrustServerCertificate=True;"
```

### OpenAI API Anahtarı
```bash
# Environment Variable olarak
set OPENAI_API_KEY=your_api_key_here

# Veya kod içinde
AIAnalizService service = new AIAnalizService("your_api_key_here");
```

## 📝 Lisans

Bu proje üniversite dersi için geliştirilmiştir.

## 👨‍💻 Geliştirici Notları

- DevExpress lisansı gereklidir (trial kullanılabilir)
- SQL Server Express ücretsiz olarak kullanılabilir
- OpenAI API kullanımı ücretlidir (opsiyonel)
- Proje .NET 6.0 ile geliştirilmiştir

## 🐛 Bilinen Sorunlar

- DevExpress paketleri NuGet'ten otomatik yüklenmeyebilir, manuel yükleme gerekebilir
- SQL Server bağlantı sorunlarında TrustServerCertificate=True kullanıldı

## 📞 Destek

Sorularınız için proje sahibi ile iletişime geçin.

---

**Not:** Bu proje eğitim amaçlı geliştirilmiştir. Production ortamında kullanmadan önce güvenlik önlemleri alınmalıdır (şifre hashleme, SQL injection koruması, vb.).

