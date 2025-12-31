using System;

namespace BursOtomasyon.Desktop.Models
{
    public class Bagisci
    {
        public int BagisciID { get; set; }
        public string Ad { get; set; } = string.Empty;
        public string Soyad { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Telefon { get; set; }
        public string? TCKimlikNo { get; set; }
        public string? Adres { get; set; }
        public DateTime KayitTarihi { get; set; }
        public bool Aktif { get; set; }
    }
}

