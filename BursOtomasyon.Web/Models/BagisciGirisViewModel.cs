using System.ComponentModel.DataAnnotations;

namespace BursOtomasyon.Web.Models
{
    public class BagisciGirisViewModel
    {
        [Required(ErrorMessage = "E-posta gereklidir")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        [Display(Name = "E-posta")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre gereklidir")]
        [Display(Name = "Şifre")]
        public string Sifre { get; set; } = string.Empty;
    }
}

