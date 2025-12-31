using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using BursOtomasyon.Desktop.Models;
using BursOtomasyon.Desktop.Services;

namespace BursOtomasyon.Desktop.Forms
{
    public partial class BursPuaniHesaplaForm : XtraForm
    {
        private OgrenciService _ogrenciService;
        private BursPuanlamaService _puanlamaService;

        public BursPuaniHesaplaForm()
        {
            InitializeComponent();
            _ogrenciService = new OgrenciService();
            _puanlamaService = new BursPuanlamaService();
            LoadOgrenciler();
            ConfigureGrid();
        }

        private void ConfigureGrid()
        {
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = true;
            gridView1.OptionsSelection.MultiSelect = false;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            
            // Sadece temel bilgileri göster
            if (gridView1.Columns["OgrenciID"] != null)
                gridView1.Columns["OgrenciID"].Visible = false;
            if (gridView1.Columns["DogumTarihi"] != null)
                gridView1.Columns["DogumTarihi"].Visible = false;
            if (gridView1.Columns["Fakulte"] != null)
                gridView1.Columns["Fakulte"].Visible = false;
            if (gridView1.Columns["Telefon"] != null)
                gridView1.Columns["Telefon"].Visible = false;
            if (gridView1.Columns["Email"] != null)
                gridView1.Columns["Email"].Visible = false;
            if (gridView1.Columns["KardesSayisi"] != null)
                gridView1.Columns["KardesSayisi"].Visible = false;
            if (gridView1.Columns["AileGeliri"] != null)
                gridView1.Columns["AileGeliri"].Visible = false;
            if (gridView1.Columns["AnneMeslek"] != null)
                gridView1.Columns["AnneMeslek"].Visible = false;
            if (gridView1.Columns["BabaMeslek"] != null)
                gridView1.Columns["BabaMeslek"].Visible = false;
            if (gridView1.Columns["KayitTarihi"] != null)
                gridView1.Columns["KayitTarihi"].Visible = false;
            if (gridView1.Columns["ProfilFotoYolu"] != null)
                gridView1.Columns["ProfilFotoYolu"].Visible = false;
            if (gridView1.Columns["KlasikSoru1Cevap"] != null)
                gridView1.Columns["KlasikSoru1Cevap"].Visible = false;
            if (gridView1.Columns["KlasikSoru2Cevap"] != null)
                gridView1.Columns["KlasikSoru2Cevap"].Visible = false;
            if (gridView1.Columns["KlasikSoru3Cevap"] != null)
                gridView1.Columns["KlasikSoru3Cevap"].Visible = false;
            if (gridView1.Columns["Secildi"] != null)
                gridView1.Columns["Secildi"].Visible = false;
            
            // Kolon başlıklarını düzenle - KISA BİLGİLER
            if (gridView1.Columns["Ad"] != null)
            {
                gridView1.Columns["Ad"].Caption = "Ad";
                gridView1.Columns["Ad"].Width = 80;
            }
            if (gridView1.Columns["Soyad"] != null)
            {
                gridView1.Columns["Soyad"].Caption = "Soyad";
                gridView1.Columns["Soyad"].Width = 100;
            }
            if (gridView1.Columns["TC"] != null)
            {
                gridView1.Columns["TC"].Caption = "TC";
                gridView1.Columns["TC"].Width = 100;
            }
            if (gridView1.Columns["Universite"] != null)
            {
                gridView1.Columns["Universite"].Caption = "Üniversite";
                gridView1.Columns["Universite"].Width = 150;
            }
            if (gridView1.Columns["Bolum"] != null)
            {
                gridView1.Columns["Bolum"].Caption = "Bölüm";
                gridView1.Columns["Bolum"].Width = 120;
            }
            if (gridView1.Columns["Sinif"] != null)
            {
                gridView1.Columns["Sinif"].Caption = "Sınıf";
                gridView1.Columns["Sinif"].Width = 60;
            }
            if (gridView1.Columns["NotOrtalamasi"] != null)
            {
                gridView1.Columns["NotOrtalamasi"].Caption = "Ortalama";
                gridView1.Columns["NotOrtalamasi"].Width = 80;
                gridView1.Columns["NotOrtalamasi"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView1.Columns["NotOrtalamasi"].DisplayFormat.FormatString = "F2";
            }
            if (gridView1.Columns["BursPuani"] != null)
            {
                gridView1.Columns["BursPuani"].Caption = "Burs Puanı";
                gridView1.Columns["BursPuani"].Width = 90;
                gridView1.Columns["BursPuani"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView1.Columns["BursPuani"].DisplayFormat.FormatString = "F2";
                gridView1.Columns["BursPuani"].Visible = true;
            }
            
            gridView1.BestFitColumns();
        }

        private void LoadOgrenciler()
        {
            var ogrenciler = _ogrenciService.GetAll();
            gridControl1.DataSource = ogrenciler;
        }

        private void btnSecilenOgrenciPuanHesapla_Click(object sender, EventArgs e)
        {
            var selectedRow = gridView1.GetFocusedRow();
            if (selectedRow != null)
            {
                var ogrenci = gridView1.GetRow(gridView1.FocusedRowHandle) as Ogrenci;
                if (ogrenci != null)
                {
                    try
                    {
                        // Burs puanını hesapla
                        var yeniPuan = _puanlamaService.HesaplaBursPuani(ogrenci);
                        
                        // Öğrenciyi güncelle
                        ogrenci.BursPuani = yeniPuan;
                        if (_ogrenciService.Update(ogrenci))
                        {
                            XtraMessageBox.Show(
                                $"Öğrenci: {ogrenci.Ad} {ogrenci.Soyad}\n" +
                                $"Not Ortalaması: {ogrenci.NotOrtalamasi:F2}\n" +
                                $"Hesaplanan Burs Puanı: {yeniPuan:F2}\n\n" +
                                (ogrenci.NotOrtalamasi >= 3.5m ? "✓ Yüksek not ortalaması nedeniyle artı puan eklendi!" : ""),
                                "Burs Puanı Hesaplandı", 
                                MessageBoxButtons.OK, 
                                MessageBoxIcon.Information);
                            LoadOgrenciler();
                        }
                        else
                        {
                            XtraMessageBox.Show("Puan hesaplandı ancak kaydedilemedi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen puan hesaplamak için bir öğrenci seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTumPuanlariHesapla_Click(object sender, EventArgs e)
        {
            try
            {
                var result = XtraMessageBox.Show(
                    "Tüm öğrencilerin burs puanlarını hesaplamak istediğinizden emin misiniz?",
                    "Onay",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    var ogrenciler = _ogrenciService.GetAll();
                    int guncellenenSayisi = 0;

                    foreach (var ogrenci in ogrenciler)
                    {
                        var yeniPuan = _puanlamaService.HesaplaBursPuani(ogrenci);
                        if (ogrenci.BursPuani != yeniPuan)
                        {
                            ogrenci.BursPuani = yeniPuan;
                            _ogrenciService.Update(ogrenci);
                            guncellenenSayisi++;
                        }
                    }

                    XtraMessageBox.Show($"{guncellenenSayisi} öğrencinin burs puanı güncellendi.", 
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadOgrenciler();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

