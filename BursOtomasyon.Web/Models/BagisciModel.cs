using System.ComponentModel.DataAnnotations;

namespace BursOtomasyon.Web.Models
{
    public class BagisciModel
    {
        public BagisciModel()
        {
            Ad = string.Empty;
            Soyad = string.Empty;
            Email = string.Empty;
            Telefon = string.Empty;
            Sifre = string.Empty;
        }

        public int BagisciID { get; set; }

        [Required(ErrorMessage = "Ad gereklidir")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad gereklidir")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "E-posta gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; }

        public string Telefon { get; set; }

        public string? TCKimlikNo { get; set; }

        public string? Adres { get; set; }

        [Required(ErrorMessage = "Şifre gereklidir")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır")]
        public string Sifre { get; set; }

        public DateTime KayitTarihi { get; set; }
        public bool Aktif { get; set; }
    }
}

