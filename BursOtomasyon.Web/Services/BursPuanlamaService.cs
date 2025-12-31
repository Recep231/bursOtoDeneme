using BursOtomasyon.Web.Models;

namespace BursOtomasyon.Web.Services
{
    public class BursPuanlamaService
    {
        public decimal HesaplaBursPuani(OgrenciModel ogrenci)
        {
            decimal puan = 0;

            // Not Ortalaması Puanı (0-40 puan)
            puan += (ogrenci.NotOrtalamasi / 4.00m) * 40m;

            // Aile Geliri Puanı (0-30 puan)
            if (ogrenci.AileGeliri <= 5000)
                puan += 30;
            else if (ogrenci.AileGeliri <= 10000)
                puan += 25;
            else if (ogrenci.AileGeliri <= 15000)
                puan += 20;
            else if (ogrenci.AileGeliri <= 20000)
                puan += 15;
            else if (ogrenci.AileGeliri <= 25000)
                puan += 10;
            else
                puan += 5;

            // Kardeş Sayısı Puanı (0-20 puan)
            switch (ogrenci.KardesSayisi)
            {
                case "0":
                    puan += 0;
                    break;
                case "1":
                    puan += 5;
                    break;
                case "2":
                    puan += 10;
                    break;
                case "3":
                    puan += 15;
                    break;
                case "4":
                case "4+":
                    puan += 20;
                    break;
            }

            // Sınıf Durumu Puanı (0-10 puan)
            switch (ogrenci.Sinif)
            {
                case "Hazırlık":
                    puan += 2;
                    break;
                case "1":
                    puan += 4;
                    break;
                case "2":
                    puan += 6;
                    break;
                case "3":
                    puan += 8;
                    break;
                case "4":
                    puan += 10;
                    break;
            }

            // Klasik sorulara verilen cevaplara göre ek/düzenleyici puan (toplam +-20 puan aralığında)
            int klasikToplamUzunluk =
                (ogrenci.KlasikSoru1Cevap ?? string.Empty).Length +
                (ogrenci.KlasikSoru2Cevap ?? string.Empty).Length +
                (ogrenci.KlasikSoru3Cevap ?? string.Empty).Length;

            // Çok kısa / umursamaz cevaplar için ceza
            if (klasikToplamUzunluk < 150)
            {
                puan -= 15; // Umursamaz / çok kısa ise ciddi düşür
            }
            else if (klasikToplamUzunluk < 350)
            {
                // Nötr aralık, değişiklik yok
            }
            else
            {
                // Detaylı ve özenli cevaplar için küçük bir bonus
                puan += 5;
            }

            // Puanı 0-100 aralığına sınırla
            if (puan > 100) puan = 100;
            if (puan < 0) puan = 0;

            return Math.Round(puan, 2);
        }
    }
}

