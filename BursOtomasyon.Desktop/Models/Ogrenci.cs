using System;

namespace BursOtomasyon.Desktop.Models
{
    public class Ogrenci
    {
        public Ogrenci()
        {
            Ad = string.Empty;
            Soyad = string.Empty;
            TC = string.Empty;
            Sinif = "1";
            KardesSayisi = "0";
            KayitTarihi = DateTime.Now;
        }

        public int OgrenciID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TC { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string? Telefon { get; set; }
        public string? Email { get; set; }
        public string? Universite { get; set; }
        public string? Fakulte { get; set; }
        public string? Bolum { get; set; }
        public string Sinif { get; set; }
        public decimal NotOrtalamasi { get; set; }
        public string KardesSayisi { get; set; }
        public decimal AileGeliri { get; set; }
        public string? AnneMeslek { get; set; }
        public string? BabaMeslek { get; set; }
        public decimal BursPuani { get; set; }
        public DateTime KayitTarihi { get; set; }
        public string? ProfilFotoYolu { get; set; }
        public string? KlasikSoru1Cevap { get; set; }
        public string? KlasikSoru2Cevap { get; set; }
        public string? KlasikSoru3Cevap { get; set; }
        
        // Checkbox için seçim durumu
        public bool Secildi { get; set; } = false;
        
        // Şehir bilgisi (otomatik hesaplanır, kullanıcıdan alınmaz)
        public string? Sehir { get; set; }
        
        // Şehir yaşam maliyeti katsayısı (otomatik hesaplanır, kullanıcıdan alınmaz)
        public decimal SehirKatsayi { get; set; } = 1.00m;
        
        // Bölüm zorluk katsayısı (otomatik hesaplanır, kullanıcıdan alınmaz)
        public decimal BolumKatsayi { get; set; } = 1.00m;
    }
}

