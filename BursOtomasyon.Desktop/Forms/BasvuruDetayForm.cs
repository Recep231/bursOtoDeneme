using System;
using System.Windows.Forms;
using System.ComponentModel;
using DevExpress.XtraEditors;
using BursOtomasyon.Desktop.Models;
using BursOtomasyon.Desktop.Services;
using BursOtomasyon.Desktop.Helpers;

namespace BursOtomasyon.Desktop.Forms
{
    public partial class BasvuruDetayForm : XtraForm
    {
        private Basvuru _basvuru;
        private OgrenciService _ogrenciService;

        // Designer için parametresiz ctor
        public BasvuruDetayForm()
        {
            InitializeComponent();

            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }
        }

        public BasvuruDetayForm(Basvuru basvuru)
        {
            _basvuru = basvuru;
            InitializeComponent();
            _ogrenciService = new OgrenciService();
            LoadDetay();
        }

        private void LoadDetay()
        {
            var ogrenci = _ogrenciService.GetById(_basvuru.OgrenciID);
            
            rtbDetay.Text = $"BAŞVURU DETAYI\n\n";
            rtbDetay.Text += $"Başvuru ID: {_basvuru.BasvuruID}\n";
            rtbDetay.Text += $"Başvuru Tarihi: {_basvuru.BasvuruTarihi:dd.MM.yyyy HH:mm}\n";
            rtbDetay.Text += $"Durum: {_basvuru.BasvuruDurumu}\n\n";
            
            if (ogrenci != null)
            {
                // Profil fotoğrafını yükle
                LoadProfilFoto(ogrenci.ProfilFotoYolu);
                
                rtbDetay.Text += $"ÖĞRENCİ BİLGİLERİ\n\n";
                rtbDetay.Text += $"Ad Soyad: {ogrenci.Ad} {ogrenci.Soyad}\n";
                rtbDetay.Text += $"TC: {ogrenci.TC}\n";
                rtbDetay.Text += $"Doğum Tarihi: {ogrenci.DogumTarihi:dd.MM.yyyy}\n";
                rtbDetay.Text += $"Telefon: {ogrenci.Telefon ?? "Belirtilmemiş"}\n";
                rtbDetay.Text += $"E-Posta: {ogrenci.Email ?? "Belirtilmemiş"}\n";
                rtbDetay.Text += $"Üniversite: {ogrenci.Universite ?? "Belirtilmemiş"}\n";
                rtbDetay.Text += $"Fakülte: {ogrenci.Fakulte ?? "Belirtilmemiş"}\n";
                rtbDetay.Text += $"Bölüm: {ogrenci.Bolum ?? "Belirtilmemiş"}\n";
                rtbDetay.Text += $"Sınıf: {ogrenci.Sinif}\n";
                rtbDetay.Text += $"Not Ortalaması: {ogrenci.NotOrtalamasi:F2}/4.00\n";
                rtbDetay.Text += $"Kardeş Sayısı: {ogrenci.KardesSayisi}\n";
                rtbDetay.Text += $"Aile Aylık Geliri: {ogrenci.AileGeliri:C}\n";
                rtbDetay.Text += $"Anne Meslek: {ogrenci.AnneMeslek ?? "Belirtilmemiş"}\n";
                rtbDetay.Text += $"Baba Meslek: {ogrenci.BabaMeslek ?? "Belirtilmemiş"}\n";
                rtbDetay.Text += $"Burs Puanı: {ogrenci.BursPuani:F2}/100\n\n";
                
                // Klasik sorulara verilen cevaplar
                if (!string.IsNullOrEmpty(ogrenci.KlasikSoru1Cevap) || 
                    !string.IsNullOrEmpty(ogrenci.KlasikSoru2Cevap) || 
                    !string.IsNullOrEmpty(ogrenci.KlasikSoru3Cevap))
                {
                    rtbDetay.Text += $"KLASİK SORULARA VERİLEN CEVAPLAR\n\n";
                    if (!string.IsNullOrEmpty(ogrenci.KlasikSoru1Cevap))
                    {
                        rtbDetay.Text += $"1. Bu bursu neden hak ettiğini düşünüyorsun?\n{ogrenci.KlasikSoru1Cevap}\n\n";
                    }
                    if (!string.IsNullOrEmpty(ogrenci.KlasikSoru2Cevap))
                    {
                        rtbDetay.Text += $"2. Gelecek hedeflerin nelerdir?\n{ogrenci.KlasikSoru2Cevap}\n\n";
                    }
                    if (!string.IsNullOrEmpty(ogrenci.KlasikSoru3Cevap))
                    {
                        rtbDetay.Text += $"3. Şu anki maddi/ailesel durumunu kısaca açıklar mısın?\n{ogrenci.KlasikSoru3Cevap}\n\n";
                    }
                }
            }
            
            if (!string.IsNullOrEmpty(_basvuru.AIYorum))
            {
                rtbDetay.Text += $"AI ANALİZ RAPORU\n\n";
                rtbDetay.Text += $"{_basvuru.AIYorum}\n";
            }
        }

        private void LoadProfilFoto(string? profilFotoYolu)
        {
            pictureEdit1.Image = ImageHelper.LoadProfilFoto(profilFotoYolu);
        }
    }
}

