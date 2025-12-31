using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.Ribbon.ViewInfo;
using BursOtomasyon.Desktop.Forms;

namespace BursOtomasyon.Desktop.Forms
{
    public partial class MainForm : RibbonForm
    {
        public MainForm()
        {
            InitializeComponent();
            InitializeRibbon();
            InitializeStatusBar();
            
            // Form başlığı
            this.Text = "Öğrenci Burs Yönetim Sistemi";
            this.WindowState = FormWindowState.Maximized;
        }

        private void InitializeRibbon()
        {
            // RibbonControl ayarları
            ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.True;

            // İŞLEMLER SAYFASI (tek sekme, grup yok)
            RibbonPage pageIslemler = new RibbonPage("İşlemler");
            RibbonPageGroup groupIslemler = new RibbonPageGroup();

            // Öğrenci İşlemleri Butonları
            BarButtonItem btnOgrenciEkle = new BarButtonItem(ribbonControl1.Manager, "Öğrenci Ekle");
            btnOgrenciEkle.ImageOptions.ImageUri.Uri = "Add;Size32x32";
            btnOgrenciEkle.ItemClick += (s, e) => { new OgrenciEkleForm().ShowDialog(); RefreshData(); };
            
            BarButtonItem btnOgrenciListele = new BarButtonItem(ribbonControl1.Manager, "Öğrenci Listele");
            btnOgrenciListele.ImageOptions.ImageUri.Uri = "ListBullets;Size32x32";
            btnOgrenciListele.ItemClick += (s, e) => { new OgrenciListeForm().ShowDialog(); RefreshData(); };
            
            BarButtonItem btnOgrenciGuncelle = new BarButtonItem(ribbonControl1.Manager, "Öğrenci Güncelle");
            btnOgrenciGuncelle.ImageOptions.ImageUri.Uri = "Edit;Size32x32";
            btnOgrenciGuncelle.ItemClick += (s, e) => { new OgrenciListeForm(true).ShowDialog(); RefreshData(); };

            // Başvuru İşlemleri Butonları
            BarButtonItem btnBasvuruListele = new BarButtonItem(ribbonControl1.Manager, "Başvuru Listele");
            btnBasvuruListele.ImageOptions.ImageUri.Uri = "TaskList;Size32x32";
            btnBasvuruListele.ItemClick += (s, e) => { new BasvuruListeForm().ShowDialog(); RefreshData(); };

            // Analiz ve Rapor Butonları
            BarButtonItem btnAIAnaliz = new BarButtonItem(ribbonControl1.Manager, "AI Analiz Yap");
            btnAIAnaliz.ImageOptions.ImageUri.Uri = "Paste;Size32x32";
            btnAIAnaliz.ItemClick += (s, e) => { new AIAnalizForm().ShowDialog(); };
            
            BarButtonItem btnBursPuaniHesapla = new BarButtonItem(ribbonControl1.Manager, "Burs Puanı Hesapla");
            btnBursPuaniHesapla.ImageOptions.ImageUri.Uri = "CustomizeGrid;Size32x32";
            btnBursPuaniHesapla.ItemClick += (s, e) => { new BursPuaniHesaplaForm().ShowDialog(); RefreshData(); };

            // Bağışçılar Butonu
            BarButtonItem btnBagisciListe = new BarButtonItem(ribbonControl1.Manager, "Bağışçılar");
            // İkon için programatik görsel oluştur ve hem Image hem de LargeImage olarak ayarla
            var bagisciIcon = CreateBagisciIcon();
            if (bagisciIcon != null)
            {
                btnBagisciListe.ImageOptions.Image = bagisciIcon;
                btnBagisciListe.ImageOptions.LargeImage = bagisciIcon;
            }
            btnBagisciListe.ItemClick += (s, e) => { new BagisciListeForm().ShowDialog(); RefreshData(); };
            
            // Tüm butonları gruba ekle
            groupIslemler.ItemLinks.Add(btnOgrenciEkle);
            groupIslemler.ItemLinks.Add(btnOgrenciListele);
            groupIslemler.ItemLinks.Add(btnOgrenciGuncelle);
            groupIslemler.ItemLinks.Add(btnBasvuruListele);
            groupIslemler.ItemLinks.Add(btnAIAnaliz);
            groupIslemler.ItemLinks.Add(btnBursPuaniHesapla);
            groupIslemler.ItemLinks.Add(btnBagisciListe);

            pageIslemler.Groups.Add(groupIslemler);
            
            // Ribbon sayfalarını ekle (tek sekme)
            ribbonControl1.Pages.Add(pageIslemler);
        }

        private void InitializeStatusBar()
        {
            // Status bar bilgileri
            if (ribbonControl1.StatusBar != null)
            {
                BarStaticItem statusItem = new BarStaticItem();
                statusItem.Manager = ribbonControl1.Manager;
                statusItem.Caption = "Hazır";
                ribbonControl1.StatusBar.ItemLinks.Add(statusItem);
            }
        }

        private void RefreshData()
        {
            // Veri yenileme işlemleri burada yapılabilir
        }

        private Image CreateBagisciIcon()
        {
            // Bağışçılar için para simgesi oluştur (32x32 boyutunda)
            var bitmap = new Bitmap(32, 32);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                
                // Para simgesi için basit bir daire ve ₺ işareti
                var brush = new SolidBrush(Color.FromArgb(76, 175, 80)); // Yeşil
                var pen = new Pen(Color.FromArgb(56, 142, 60), 2);
                
                // Daire çiz
                g.FillEllipse(brush, 1, 1, 30, 30);
                g.DrawEllipse(pen, 1, 1, 30, 30);
                
                // ₺ işareti çiz
                var font = new Font("Arial", 18, FontStyle.Bold);
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
    }
}

