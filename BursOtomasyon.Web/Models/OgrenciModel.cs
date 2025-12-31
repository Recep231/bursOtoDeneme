namespace BursOtomasyon.Web.Models
{
    public class OgrenciModel
    {
        public OgrenciModel()
        {
            Ad = string.Empty;
            Soyad = string.Empty;
            TC = string.Empty;
            Telefon = string.Empty;
            Email = string.Empty;
            Universite = string.Empty;
            Fakulte = string.Empty;
            Bolum = string.Empty;
            Sinif = "1";
            KardesSayisi = "0";
            AnneMeslek = string.Empty;
            BabaMeslek = string.Empty;
            KlasikSoru1Cevap = string.Empty;
            KlasikSoru2Cevap = string.Empty;
            KlasikSoru3Cevap = string.Empty;
        }

        public int OgrenciID { get; set; }
        public string Ad { get; set; }
        public string Soyad { get; set; }
        public string TC { get; set; }
        public DateTime DogumTarihi { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Universite { get; set; }
        public string Fakulte { get; set; }
        public string Bolum { get; set; }
        public string Sinif { get; set; }
        public decimal NotOrtalamasi { get; set; }
        public string KardesSayisi { get; set; }
        public decimal AileGeliri { get; set; }
        public string AnneMeslek { get; set; }
        public string BabaMeslek { get; set; }
        public decimal BursPuani { get; set; }

        // Web başvurusundan gelen ekstra bilgiler
        public string? ProfilFotoYolu { get; set; }
        public string KlasikSoru1Cevap { get; set; }
        public string KlasikSoru2Cevap { get; set; }
        public string KlasikSoru3Cevap { get; set; }
    }
}

