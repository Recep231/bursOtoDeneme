using System;

namespace BursOtomasyon.Desktop.Models
{
    public class Basvuru
    {
        public int BasvuruID { get; set; }
        public int OgrenciID { get; set; }
        public string BasvuruDurumu { get; set; } = "Beklemede";
        public DateTime BasvuruTarihi { get; set; }
        public string? AIYorum { get; set; }
        public DateTime? OnayTarihi { get; set; }
        public int? OnaylayanAdminID { get; set; }
        
        // İlişkili öğrenci bilgileri
        public string OgrenciAdSoyad { get; set; } = string.Empty;
        public string OgrenciTC { get; set; } = string.Empty;
        public string? OgrenciEmail { get; set; }
        public string? OgrenciTelefon { get; set; }
        public string? OgrenciUniversite { get; set; }
        public string? OgrenciBolum { get; set; }
        public string? OgrenciSinif { get; set; }
        public decimal OgrenciNotOrtalamasi { get; set; }
        public decimal OgrenciBursPuani { get; set; }
        
        // Checkbox için seçim durumu
        public bool Secildi { get; set; } = false;
    }
}

