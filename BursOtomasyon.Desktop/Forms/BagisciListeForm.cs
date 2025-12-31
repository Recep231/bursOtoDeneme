using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Repository;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using BursOtomasyon.Desktop.Services;
using BursOtomasyon.Desktop.Models;

namespace BursOtomasyon.Desktop.Forms
{
    public partial class BagisciListeForm : XtraForm
    {
        private BagisciService _bagisciService;

        public BagisciListeForm()
        {
            InitializeComponent();

            // Tasarım modunda veritabanına gitme
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _bagisciService = new BagisciService();
            LoadBagiscilar();
            LoadBagisAkisi();
            LoadKasaOzeti();
            ConfigureGrids();
            LoadKasaIcon();
            LoadBagiscilarIcon();
        }

        private void LoadKasaIcon()
        {
            try
            {
                // Basit bir para görseli oluştur
                var iconImage = CreateMoneyIcon();
                if (iconImage != null)
                {
                    pictureEditKasa.Image = iconImage;
                }
            }
            catch
            {
                // Icon yüklenemezse görsel olmadan devam et
            }
        }

        private void LoadBagiscilarIcon()
        {
            try
            {
                // Basit bir para görseli oluştur
                var iconImage = CreateMoneyIcon();
                if (iconImage != null)
                {
                    pictureEditBagiscilar.Image = iconImage;
                    pictureEditBagiscilar.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
                    pictureEditBagiscilar.Properties.ShowMenu = false;
                    pictureEditBagiscilar.Properties.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda log tutulabilir
                System.Diagnostics.Debug.WriteLine($"Icon yükleme hatası: {ex.Message}");
            }
        }

        private Image CreateMoneyIcon()
        {
            // Basit bir para simgesi oluştur
            var bitmap = new Bitmap(32, 32);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                
                // Para simgesi için basit bir daire ve ₺ işareti
                var brush = new SolidBrush(Color.FromArgb(76, 175, 80)); // Yeşil
                var pen = new Pen(Color.FromArgb(56, 142, 60), 2);
                
                // Daire çiz
                g.FillEllipse(brush, 2, 2, 28, 28);
                g.DrawEllipse(pen, 2, 2, 28, 28);
                
                // ₺ işareti çiz
                var font = new Font("Arial", 16, FontStyle.Bold);
                var textBrush = new SolidBrush(Color.White);
                var sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                g.DrawString("₺", font, textBrush, new RectangleF(0, 0, 32, 32), sf);
                
                font.Dispose();
                textBrush.Dispose();
                brush.Dispose();
                pen.Dispose();
            }
            return bitmap;
        }

        private void ConfigureGrids()
        {
            // Bağışçılar Grid
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = true;
            gridView1.OptionsSelection.MultiSelect = false;
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.BestFitColumns();

            // Bağış Akışı Grid
            gridViewBagisAkisi.OptionsView.ShowGroupPanel = false;
            gridViewBagisAkisi.OptionsView.ShowIndicator = true;
            gridViewBagisAkisi.OptionsSelection.MultiSelect = false;
            gridViewBagisAkisi.OptionsBehavior.Editable = false;
            gridViewBagisAkisi.OptionsView.ShowAutoFilterRow = true;
            
            // Durum sütununu özelleştir
            if (gridViewBagisAkisi.Columns["Durum"] != null)
            {
                var durumColumn = gridViewBagisAkisi.Columns["Durum"];
                durumColumn.Caption = "Durum";
                durumColumn.Width = 120;
                
                // Durum görünümünü özelleştir (CustomDrawCell ile renklendirme yapılacak)
                gridViewBagisAkisi.CustomDrawCell += GridViewBagisAkisi_CustomDrawCell;
            }
            
            // Bağış Tutarı formatı
            if (gridViewBagisAkisi.Columns["Tutar"] != null)
            {
                gridViewBagisAkisi.Columns["Tutar"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewBagisAkisi.Columns["Tutar"].DisplayFormat.FormatString = "C2";
                gridViewBagisAkisi.Columns["Tutar"].Caption = "Tutar (TL)";
            }
            
            // Bağış Tarihi formatı
            if (gridViewBagisAkisi.Columns["BagisTarihi"] != null)
            {
                gridViewBagisAkisi.Columns["BagisTarihi"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridViewBagisAkisi.Columns["BagisTarihi"].DisplayFormat.FormatString = "dd.MM.yyyy HH:mm";
                gridViewBagisAkisi.Columns["BagisTarihi"].Caption = "Bağış Tarihi";
            }
            
            // Ödeme Yöntemi formatı
            if (gridViewBagisAkisi.Columns["OdemeYontemi"] != null)
            {
                gridViewBagisAkisi.Columns["OdemeYontemi"].Caption = "Ödeme Yöntemi";
            }
            
            // Bağışçı Ad Soyad formatı
            if (gridViewBagisAkisi.Columns["BagisciAdSoyad"] != null)
            {
                gridViewBagisAkisi.Columns["BagisciAdSoyad"].Caption = "Bağışçı";
            }
            
            // Ödeme Yöntemi değerlerini Türkçeleştir
            gridViewBagisAkisi.CustomColumnDisplayText += GridViewBagisAkisi_CustomColumnDisplayText;
            
            gridViewBagisAkisi.BestFitColumns();
        }

        private void GridViewBagisAkisi_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column?.FieldName == "OdemeYontemi")
            {
                switch (e.Value?.ToString())
                {
                    case "KrediKarti":
                        e.DisplayText = "Kredi Kartı";
                        break;
                    case "BankaHavalesi":
                        e.DisplayText = "Banka Havalesi";
                        break;
                    case "Nakit":
                        e.DisplayText = "Nakit";
                        break;
                }
            }
            else if (e.Column?.FieldName == "Durum")
            {
                switch (e.Value?.ToString())
                {
                    case "Beklemede":
                        e.DisplayText = "Beklemede";
                        break;
                    case "Onaylandi":
                        e.DisplayText = "Onaylandı";
                        break;
                    case "Iptal":
                        e.DisplayText = "İptal";
                        break;
                }
            }
        }

        private void GridViewBagisAkisi_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column?.FieldName == "Durum")
            {
                var durum = e.CellValue?.ToString() ?? "";
                var backColor = Color.White;
                var foreColor = Color.Black;
                string durumText = durum;
                
                switch (durum)
                {
                    case "Beklemede":
                        backColor = Color.FromArgb(255, 235, 59); // Sarı
                        foreColor = Color.Black;
                        durumText = "Beklemede";
                        break;
                    case "Onaylandi":
                        backColor = Color.FromArgb(76, 175, 80); // Yeşil
                        foreColor = Color.White;
                        durumText = "Onaylandı";
                        break;
                    case "Iptal":
                        backColor = Color.FromArgb(244, 67, 54); // Kırmızı
                        foreColor = Color.White;
                        durumText = "İptal";
                        break;
                }
                
                e.Appearance.BackColor = backColor;
                e.Appearance.ForeColor = foreColor;
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.DisplayText = durumText;
            }
        }

        private void LoadBagiscilar()
        {
            var bagiscilar = _bagisciService.GetAll();
            gridControl1.DataSource = bagiscilar;
        }

        private void LoadBagisAkisi()
        {
            var bagislar = _bagisciService.GetBagislar();
            gridControlBagisAkisi.DataSource = bagislar;
        }

        private void LoadKasaOzeti()
        {
            var kasaOzeti = _bagisciService.GetKasaOzeti();
            
            lblToplamGelen.Text = kasaOzeti.ToplamGelenBagis.ToString("C2");
            lblBeklemede.Text = kasaOzeti.BeklemedeBagis.ToString("C2");
            lblOnaylanan.Text = kasaOzeti.OnaylananBagis.ToString("C2");
            lblSonGuncelleme.Text = $"Son Güncelleme: {kasaOzeti.SonGuncellemeTarihi:dd.MM.yyyy HH:mm}";
        }

        private void btnYenile_Click(object sender, EventArgs e)
        {
            LoadBagiscilar();
            LoadBagisAkisi();
            LoadKasaOzeti();
        }

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

