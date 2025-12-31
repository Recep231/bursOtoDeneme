using System;

namespace BursOtomasyon.Desktop.Models
{
    public class Bagis
    {
        public int BagisID { get; set; }
        public int BagisciID { get; set; }
        public string BagisciAdSoyad { get; set; } = string.Empty;
        public decimal Tutar { get; set; }
        public DateTime BagisTarihi { get; set; }
        public string? Aciklama { get; set; }
        public string? IBAN { get; set; }
        public string OdemeYontemi { get; set; } = string.Empty;
        public string Durum { get; set; } = string.Empty;
    }
}

