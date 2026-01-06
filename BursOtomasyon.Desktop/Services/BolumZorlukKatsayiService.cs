using System;
using System.Collections.Generic;
using System.Linq;

namespace BursOtomasyon.Desktop.Services
{
    /// <summary>
    /// Örgün öğretim bölümlerinin akademik zorluk katsayılarını yönetir.
    /// Not: Sistem sadece örgün öğretim öğrencilerini kapsamaktadır.
    /// </summary>
    public class BolumZorlukKatsayiService
    {
        // Bölüm adı -> Zorluk Katsayısı mapping
        private readonly Dictionary<string, decimal> _bolumKatsayiMap;

        public BolumZorlukKatsayiService()
        {
            _bolumKatsayiMap = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
            InitializeMappings();
        }

        private void InitializeMappings()
        {
            // ============================================
            // TIP FAKÜLTESİ - En Yüksek Zorluk (1.30)
            // ============================================
            _bolumKatsayiMap["Tıp"] = 1.30m;
            _bolumKatsayiMap["Tıp Fakültesi"] = 1.30m;
            _bolumKatsayiMap["Tıbbi Bilimler"] = 1.30m;
            _bolumKatsayiMap["Genel Tıp"] = 1.30m;
            _bolumKatsayiMap["Cerrahi"] = 1.30m;
            _bolumKatsayiMap["İç Hastalıkları"] = 1.30m;
            _bolumKatsayiMap["Pediatri"] = 1.30m;
            _bolumKatsayiMap["Kardiyoloji"] = 1.30m;
            _bolumKatsayiMap["Nöroloji"] = 1.30m;
            _bolumKatsayiMap["Ortopedi"] = 1.30m;
            _bolumKatsayiMap["Göz Hastalıkları"] = 1.30m;
            _bolumKatsayiMap["Kulak Burun Boğaz"] = 1.30m;
            _bolumKatsayiMap["Dermatoloji"] = 1.30m;
            _bolumKatsayiMap["Radyoloji"] = 1.30m;
            _bolumKatsayiMap["Anestezi"] = 1.30m;
            _bolumKatsayiMap["Acil Tıp"] = 1.30m;
            _bolumKatsayiMap["Aile Hekimliği"] = 1.30m;
            _bolumKatsayiMap["Fizyoterapi ve Rehabilitasyon"] = 1.30m;
            _bolumKatsayiMap["Fizyoterapi"] = 1.30m;
            _bolumKatsayiMap["Beslenme ve Diyetetik"] = 1.30m;
            _bolumKatsayiMap["Diyetisyenlik"] = 1.30m;
            _bolumKatsayiMap["Hemşirelik"] = 1.30m;
            _bolumKatsayiMap["Ebelik"] = 1.30m;
            _bolumKatsayiMap["Eczacılık"] = 1.30m;
            _bolumKatsayiMap["Eczacılık Fakültesi"] = 1.30m;
            _bolumKatsayiMap["Veteriner Hekimliği"] = 1.30m;
            _bolumKatsayiMap["Veterinerlik"] = 1.30m;
            _bolumKatsayiMap["Veteriner"] = 1.30m;
            _bolumKatsayiMap["Diş Hekimliği"] = 1.30m;
            _bolumKatsayiMap["Diş Hekimliği Fakültesi"] = 1.30m;

            // ============================================
            // MÜHENDİSLİK FAKÜLTESİ - Yüksek Zorluk (1.15)
            // ============================================
            _bolumKatsayiMap["Bilgisayar Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Bilgisayar"] = 1.15m;
            _bolumKatsayiMap["Yazılım Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Yazılım"] = 1.15m;
            _bolumKatsayiMap["Elektrik Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Elektrik"] = 1.15m;
            _bolumKatsayiMap["Elektrik-Elektronik Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Elektronik Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Elektronik"] = 1.15m;
            _bolumKatsayiMap["Makine Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Makine"] = 1.15m;
            _bolumKatsayiMap["Endüstri Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Endüstri"] = 1.15m;
            _bolumKatsayiMap["İnşaat Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["İnşaat"] = 1.15m;
            _bolumKatsayiMap["Kimya Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Kimya"] = 1.15m;
            _bolumKatsayiMap["Gıda Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Gıda"] = 1.15m;
            _bolumKatsayiMap["Çevre Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Çevre"] = 1.15m;
            _bolumKatsayiMap["Petrol ve Doğalgaz Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Petrol"] = 1.15m;
            _bolumKatsayiMap["Maden Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Maden"] = 1.15m;
            _bolumKatsayiMap["Jeoloji Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Jeoloji"] = 1.15m;
            _bolumKatsayiMap["Jeofizik Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Jeofizik"] = 1.15m;
            _bolumKatsayiMap["Harita Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Harita"] = 1.15m;
            _bolumKatsayiMap["Mimarlık"] = 1.15m;
            _bolumKatsayiMap["Mimarlık Fakültesi"] = 1.15m;
            _bolumKatsayiMap["Şehir ve Bölge Planlama"] = 1.15m;
            _bolumKatsayiMap["Endüstriyel Tasarım"] = 1.15m;
            _bolumKatsayiMap["Otomotiv Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Otomotiv"] = 1.15m;
            _bolumKatsayiMap["Uçak Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Uçak"] = 1.15m;
            _bolumKatsayiMap["Havacılık Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Havacılık"] = 1.15m;
            _bolumKatsayiMap["Uzay Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Uzay"] = 1.15m;
            _bolumKatsayiMap["Mekatronik Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Mekatronik"] = 1.15m;
            _bolumKatsayiMap["Biyomedikal Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Biyomedikal"] = 1.15m;
            _bolumKatsayiMap["Nanoteknoloji Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Nanoteknoloji"] = 1.15m;
            _bolumKatsayiMap["Enerji Sistemleri Mühendisliği"] = 1.15m;
            _bolumKatsayiMap["Enerji"] = 1.15m;
            _bolumKatsayiMap["Mühendislik"] = 1.15m; // Genel eşleşme
            _bolumKatsayiMap["Mühendislik Fakültesi"] = 1.15m;

            // ============================================
            // FEN-EDEBİYAT FAKÜLTESİ - Orta-Yüksek Zorluk (1.05)
            // ============================================
            _bolumKatsayiMap["Matematik"] = 1.05m;
            _bolumKatsayiMap["Matematik ve Bilgisayar Bilimleri"] = 1.05m;
            _bolumKatsayiMap["Fizik"] = 1.05m;
            _bolumKatsayiMap["Fizik Mühendisliği"] = 1.05m;
            _bolumKatsayiMap["Kimya"] = 1.05m;
            _bolumKatsayiMap["Biyoloji"] = 1.05m;
            _bolumKatsayiMap["Moleküler Biyoloji ve Genetik"] = 1.05m;
            _bolumKatsayiMap["Genetik"] = 1.05m;
            _bolumKatsayiMap["Biyokimya"] = 1.05m;
            _bolumKatsayiMap["İstatistik"] = 1.05m;
            _bolumKatsayiMap["Aktüerya"] = 1.05m;
            _bolumKatsayiMap["Aktüerya Bilimleri"] = 1.05m;
            _bolumKatsayiMap["Astronomi ve Uzay Bilimleri"] = 1.05m;
            _bolumKatsayiMap["Astronomi"] = 1.05m;
            _bolumKatsayiMap["Coğrafya"] = 1.05m;
            _bolumKatsayiMap["Tarih"] = 1.05m;
            _bolumKatsayiMap["Türk Dili ve Edebiyatı"] = 1.05m;
            _bolumKatsayiMap["Türk Dili"] = 1.05m;
            _bolumKatsayiMap["Edebiyat"] = 1.05m;
            _bolumKatsayiMap["Felsefe"] = 1.05m;
            _bolumKatsayiMap["Sosyoloji"] = 1.05m;
            _bolumKatsayiMap["Psikoloji"] = 1.05m;
            _bolumKatsayiMap["Antropoloji"] = 1.05m;
            _bolumKatsayiMap["Arkeoloji"] = 1.05m;
            _bolumKatsayiMap["Sanat Tarihi"] = 1.05m;
            _bolumKatsayiMap["Dilbilim"] = 1.05m;
            _bolumKatsayiMap["Çeviribilim"] = 1.05m;
            _bolumKatsayiMap["Mütercim Tercümanlık"] = 1.05m;
            _bolumKatsayiMap["Fen-Edebiyat"] = 1.05m; // Genel eşleşme
            _bolumKatsayiMap["Fen-Edebiyat Fakültesi"] = 1.05m;

            // ============================================
            // İİBF (İKTİSADİ VE İDARİ BİLİMLER) - Standart (1.00)
            // ============================================
            _bolumKatsayiMap["İktisat"] = 1.00m;
            _bolumKatsayiMap["İşletme"] = 1.00m;
            _bolumKatsayiMap["İşletme Yönetimi"] = 1.00m;
            _bolumKatsayiMap["Maliye"] = 1.00m;
            _bolumKatsayiMap["Muhasebe"] = 1.00m;
            _bolumKatsayiMap["Muhasebe ve Finansman"] = 1.00m;
            _bolumKatsayiMap["Finans"] = 1.00m;
            _bolumKatsayiMap["Finansal Yönetim"] = 1.00m;
            _bolumKatsayiMap["Bankacılık"] = 1.00m;
            _bolumKatsayiMap["Bankacılık ve Finans"] = 1.00m;
            _bolumKatsayiMap["Sigortacılık"] = 1.00m;
            _bolumKatsayiMap["Uluslararası İlişkiler"] = 1.00m;
            _bolumKatsayiMap["Siyaset Bilimi"] = 1.00m;
            _bolumKatsayiMap["Siyaset Bilimi ve Kamu Yönetimi"] = 1.00m;
            _bolumKatsayiMap["Kamu Yönetimi"] = 1.00m;
            _bolumKatsayiMap["Çalışma Ekonomisi ve Endüstri İlişkileri"] = 1.00m;
            _bolumKatsayiMap["Çalışma Ekonomisi"] = 1.00m;
            _bolumKatsayiMap["Ekonometri"] = 1.00m;
            _bolumKatsayiMap["Yönetim Bilişim Sistemleri"] = 1.00m;
            _bolumKatsayiMap["İnsan Kaynakları Yönetimi"] = 1.00m;
            _bolumKatsayiMap["Lojistik Yönetimi"] = 1.00m;
            _bolumKatsayiMap["Turizm İşletmeciliği"] = 1.00m;
            _bolumKatsayiMap["Turizm"] = 1.00m;
            _bolumKatsayiMap["Otel İşletmeciliği"] = 1.00m;
            _bolumKatsayiMap["İİBF"] = 1.00m; // Genel eşleşme
            _bolumKatsayiMap["İktisadi ve İdari Bilimler"] = 1.00m;
            _bolumKatsayiMap["İktisadi ve İdari Bilimler Fakültesi"] = 1.00m;

            // ============================================
            // EĞİTİM FAKÜLTESİ - Standart (1.00)
            // ============================================
            _bolumKatsayiMap["Sınıf Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["Okul Öncesi Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["Rehberlik ve Psikolojik Danışmanlık"] = 1.00m;
            _bolumKatsayiMap["Rehberlik"] = 1.00m;
            _bolumKatsayiMap["Türkçe Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["Matematik Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["Fen Bilgisi Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["Sosyal Bilgiler Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["Tarih Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["Coğrafya Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["İngilizce Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["Almanca Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["Fransızca Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["Beden Eğitimi ve Spor Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["Beden Eğitimi"] = 1.00m;
            _bolumKatsayiMap["Müzik Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["Resim Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["Görsel Sanatlar Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["Bilgisayar ve Öğretim Teknolojileri Öğretmenliği"] = 1.00m;
            _bolumKatsayiMap["Eğitim"] = 1.00m; // Genel eşleşme
            _bolumKatsayiMap["Eğitim Fakültesi"] = 1.00m;

            // ============================================
            // HUKUK FAKÜLTESİ - Orta-Yüksek Zorluk (1.10)
            // ============================================
            _bolumKatsayiMap["Hukuk"] = 1.10m;
            _bolumKatsayiMap["Hukuk Fakültesi"] = 1.10m;

            // ============================================
            // İLETİŞİM FAKÜLTESİ - Standart (1.00)
            // ============================================
            _bolumKatsayiMap["Gazetecilik"] = 1.00m;
            _bolumKatsayiMap["Radyo, Televizyon ve Sinema"] = 1.00m;
            _bolumKatsayiMap["Radyo Televizyon"] = 1.00m;
            _bolumKatsayiMap["Sinema"] = 1.00m;
            _bolumKatsayiMap["Halkla İlişkiler"] = 1.00m;
            _bolumKatsayiMap["Halkla İlişkiler ve Tanıtım"] = 1.00m;
            _bolumKatsayiMap["Reklamcılık"] = 1.00m;
            _bolumKatsayiMap["İletişim"] = 1.00m;
            _bolumKatsayiMap["İletişim Fakültesi"] = 1.00m;
            _bolumKatsayiMap["Medya"] = 1.00m;

            // ============================================
            // GÜZEL SANATLAR FAKÜLTESİ - Standart (1.00)
            // ============================================
            _bolumKatsayiMap["Güzel Sanatlar"] = 1.00m;
            _bolumKatsayiMap["Güzel Sanatlar Fakültesi"] = 1.00m;
            _bolumKatsayiMap["Resim"] = 1.00m;
            _bolumKatsayiMap["Heykel"] = 1.00m;
            _bolumKatsayiMap["Seramik"] = 1.00m;
            _bolumKatsayiMap["Grafik Tasarım"] = 1.00m;
            _bolumKatsayiMap["Grafik"] = 1.00m;
            _bolumKatsayiMap["Endüstriyel Tasarım"] = 1.00m;
            _bolumKatsayiMap["Müzik"] = 1.00m;
            _bolumKatsayiMap["Sahne Sanatları"] = 1.00m;
            _bolumKatsayiMap["Tiyatro"] = 1.00m;
            _bolumKatsayiMap["Sinema ve Televizyon"] = 1.00m;

            // ============================================
            // SPOR BİLİMLERİ FAKÜLTESİ - Standart (1.00)
            // ============================================
            _bolumKatsayiMap["Spor Bilimleri"] = 1.00m;
            _bolumKatsayiMap["Spor Bilimleri Fakültesi"] = 1.00m;
            _bolumKatsayiMap["Antrenörlük"] = 1.00m;
            _bolumKatsayiMap["Beden Eğitimi ve Spor"] = 1.00m;
            _bolumKatsayiMap["Rekreasyon"] = 1.00m;

            // ============================================
            // ZİRAAT FAKÜLTESİ - Orta Zorluk (1.05)
            // ============================================
            _bolumKatsayiMap["Ziraat"] = 1.05m;
            _bolumKatsayiMap["Ziraat Fakültesi"] = 1.05m;
            _bolumKatsayiMap["Tarım"] = 1.05m;
            _bolumKatsayiMap["Bitkisel Üretim"] = 1.05m;
            _bolumKatsayiMap["Hayvansal Üretim"] = 1.05m;
            _bolumKatsayiMap["Tarım Ekonomisi"] = 1.05m;
            _bolumKatsayiMap["Tarımsal Yapılar"] = 1.05m;
            _bolumKatsayiMap["Toprak Bilimi"] = 1.05m;
            _bolumKatsayiMap["Bahçe Bitkileri"] = 1.05m;
            _bolumKatsayiMap["Tarla Bitkileri"] = 1.05m;

            // ============================================
            // ORMAN FAKÜLTESİ - Orta Zorluk (1.05)
            // ============================================
            _bolumKatsayiMap["Orman"] = 1.05m;
            _bolumKatsayiMap["Orman Fakültesi"] = 1.05m;
            _bolumKatsayiMap["Orman Mühendisliği"] = 1.05m;
            _bolumKatsayiMap["Orman Endüstrisi Mühendisliği"] = 1.05m;

            // ============================================
            // SU ÜRÜNLERİ FAKÜLTESİ - Orta Zorluk (1.05)
            // ============================================
            _bolumKatsayiMap["Su Ürünleri"] = 1.05m;
            _bolumKatsayiMap["Su Ürünleri Fakültesi"] = 1.05m;
            _bolumKatsayiMap["Su Ürünleri Mühendisliği"] = 1.05m;

            // ============================================
            // VETERİNER FAKÜLTESİ - Yüksek Zorluk (1.25)
            // ============================================
            // Yukarıda Tıp kategorisinde 1.30 olarak tanımlandı
            // Ayrı bir kategori olarak da eklenebilir ama zaten var

            // ============================================
            // DİĞER BÖLÜMLER - Varsayılan (1.00)
            // ============================================
            // Eşleşme bulunamazsa 1.00 döndürülecek
        }

        /// <summary>
        /// Verilen bölüm adına göre zorluk katsayısını döndürür.
        /// Eşleşme bulunamazsa varsayılan olarak 1.00 döndürür.
        /// </summary>
        /// <param name="bolum">Bölüm adı</param>
        /// <returns>Bölüm zorluk katsayısı (1.00 - 1.30 arası)</returns>
        public decimal GetKatsayiByBolum(string? bolum)
        {
            if (string.IsNullOrWhiteSpace(bolum))
                return 1.00m;

            var bolumTrim = bolum.Trim();
            var bolumLower = bolumTrim.ToLowerInvariant();

            // 1. Tam eşleşme kontrolü
            if (_bolumKatsayiMap.TryGetValue(bolumTrim, out decimal katsayi))
                return katsayi;

            // 2. Anahtar kelime bazlı eşleşme (daha akıllı)
            // Tıp ve Sağlık Bilimleri (1.30)
            if (bolumLower.Contains("tıp") || bolumLower.Contains("tip") || 
                bolumLower.Contains("hekimli") || bolumLower.Contains("eczacı") ||
                bolumLower.Contains("hemşire") || bolumLower.Contains("ebelik") ||
                bolumLower.Contains("fizyoterapi") || bolumLower.Contains("beslenme") ||
                bolumLower.Contains("diyetetik") || bolumLower.Contains("veteriner") ||
                bolumLower.Contains("diş"))
                return 1.30m;

            // Mühendislik (1.15)
            if (bolumLower.Contains("mühendis") || bolumLower.Contains("muhendis") ||
                bolumLower.Contains("mimarlık") || bolumLower.Contains("mimarlik") ||
                bolumLower.Contains("yazılım") || bolumLower.Contains("yazilim") ||
                bolumLower.Contains("bilgisayar") && !bolumLower.Contains("öğretmen"))
                return 1.15m;

            // Hukuk (1.10)
            if (bolumLower.Contains("hukuk"))
                return 1.10m;

            // Fen-Edebiyat (1.05)
            if (bolumLower.Contains("matematik") && !bolumLower.Contains("öğretmen") ||
                bolumLower.Contains("fizik") && !bolumLower.Contains("öğretmen") ||
                bolumLower.Contains("kimya") && !bolumLower.Contains("öğretmen") ||
                bolumLower.Contains("biyoloji") && !bolumLower.Contains("öğretmen") ||
                bolumLower.Contains("genetik") || bolumLower.Contains("istatistik") ||
                bolumLower.Contains("astronomi") || bolumLower.Contains("psikoloji") ||
                bolumLower.Contains("sosyoloji") || bolumLower.Contains("felsefe") ||
                bolumLower.Contains("tarih") && !bolumLower.Contains("öğretmen") ||
                bolumLower.Contains("edebiyat") || bolumLower.Contains("dilbilim") ||
                bolumLower.Contains("arkeoloji") || bolumLower.Contains("antropoloji") ||
                bolumLower.Contains("coğrafya") && !bolumLower.Contains("öğretmen"))
                return 1.05m;

            // Ziraat ve Orman (1.05)
            if (bolumLower.Contains("ziraat") || bolumLower.Contains("tarım") ||
                bolumLower.Contains("orman") || bolumLower.Contains("su ürün"))
                return 1.05m;

            // 3. Kısmi eşleşme kontrolü (map'ten)
            foreach (var kvp in _bolumKatsayiMap)
            {
                var keyLower = kvp.Key.ToLowerInvariant();
                if (bolumLower.Contains(keyLower) || keyLower.Contains(bolumLower))
                {
                    return kvp.Value;
                }
            }

            // Eşleşme bulunamadı, varsayılan değer
            return 1.00m;
        }

        /// <summary>
        /// Tüm bölüm katsayılarını döndürür (admin paneli için).
        /// </summary>
        public Dictionary<string, decimal> GetAllKatsayilar()
        {
            return new Dictionary<string, decimal>(_bolumKatsayiMap);
        }
    }
}

