using System.ComponentModel.DataAnnotations;

namespace BursOtomasyon.Web.Models
{
    public class BagisViewModel
    {
        public BagisViewModel()
        {
            Ad = string.Empty;
            Soyad = string.Empty;
            Email = string.Empty;
            Telefon = string.Empty;
            Aciklama = string.Empty;
        }

        [Required(ErrorMessage = "Ad gereklidir")]
        [Display(Name = "Ad")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad gereklidir")]
        [Display(Name = "Soyad")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "E-posta gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }

        [Display(Name = "Telefon")]
        public string Telefon { get; set; }


        [Required(ErrorMessage = "Tutar gereklidir")]
        [Range(1, double.MaxValue, ErrorMessage = "Tutar 1 TL'den büyük olmalıdır")]
        [Display(Name = "Bağış Tutarı (TL)")]
        public decimal Tutar { get; set; }

        [Display(Name = "Açıklama (İsteğe bağlı)")]
        public string Aciklama { get; set; }
    }
}

