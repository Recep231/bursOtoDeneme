using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace BursOtomasyon.Web.Models
{
    public class BasvuruViewModel
    {
        public BasvuruViewModel()
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

        [Required(ErrorMessage = "Ad gereklidir")]
        public string Ad { get; set; }

        [Required(ErrorMessage = "Soyad gereklidir")]
        public string Soyad { get; set; }

        [Required(ErrorMessage = "TC Kimlik No gereklidir")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "TC Kimlik No 11 haneli olmalıdır")]
        public string TC { get; set; }

        [Required(ErrorMessage = "Doğum tarihi gereklidir")]
        [DataType(DataType.Date)]
        public DateTime DogumTarihi { get; set; }

        public string Telefon { get; set; }

        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Üniversite gereklidir")]
        public string Universite { get; set; }

        public string Fakulte { get; set; }

        [Required(ErrorMessage = "Bölüm gereklidir")]
        public string Bolum { get; set; }

        [Required(ErrorMessage = "Sınıf gereklidir")]
        public string Sinif { get; set; }

        [Required(ErrorMessage = "Not ortalaması gereklidir")]
        [Range(0.00, 4.00, ErrorMessage = "Not ortalaması 0.00 - 4.00 arasında olmalıdır")]
        public decimal NotOrtalamasi { get; set; }

        [Required(ErrorMessage = "Kardeş sayısı gereklidir")]
        public string KardesSayisi { get; set; }

        [Required(ErrorMessage = "Aile aylık geliri gereklidir")]
        [Range(0, double.MaxValue, ErrorMessage = "Aile geliri 0'dan büyük olmalıdır")]
        public decimal AileGeliri { get; set; }

        public string AnneMeslek { get; set; }

        public string BabaMeslek { get; set; }

        // Profil fotoğrafı (isteğe bağlı)
        public IFormFile? ProfilFoto { get; set; }

        // 3 klasik soru cevapları
        [Required(ErrorMessage = "Lütfen bu soruya cevap veriniz")]
        [Display(Name = "Bu bursu neden hak ettiğini düşünüyorsun?")]
        public string KlasikSoru1Cevap { get; set; }

        [Required(ErrorMessage = "Lütfen bu soruya cevap veriniz")]
        [Display(Name = "Gelecek hedeflerin nelerdir?")]
        public string KlasikSoru2Cevap { get; set; }

        [Required(ErrorMessage = "Lütfen bu soruya cevap veriniz")]
        [Display(Name = "Şu anki maddi/ailesel durumunu kısaca açıklar mısın?")]
        public string KlasikSoru3Cevap { get; set; }
    }
}

