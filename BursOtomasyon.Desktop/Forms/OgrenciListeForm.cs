using System;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;
using DevExpress.XtraEditors;
using BursOtomasyon.Desktop.Models;
using BursOtomasyon.Desktop.Services;
using BursOtomasyon.Desktop.Helpers;

namespace BursOtomasyon.Desktop.Forms
{
    public partial class OgrenciListeForm : XtraForm
    {
        private OgrenciService _ogrenciService;
        private bool _isUpdateMode;
        private bool _isDeleteMode;

        public OgrenciListeForm(bool isUpdateMode = false, bool isDeleteMode = false)
        {
            _isUpdateMode = isUpdateMode;
            _isDeleteMode = isDeleteMode;
            InitializeComponent();
            _ogrenciService = new OgrenciService();
            LoadOgrenciKartlari();
            
            // Güncelleme modunda "Güncelle" butonunu göster
            if (_isUpdateMode)
            {
                btnGuncelle.Visible = true;
                this.Text = "Öğrenci Seç - Güncelle";
            }
            else
            {
                btnGuncelle.Visible = false;
            }
            
            // Butonları modernize et
            ModernizeButtons();
            UpdateButtonStates();
            
            // Form ve ScrollableControl resize olduğunda kartları yeniden düzenle
            this.Resize += (s, e) => {
                if (this.WindowState != FormWindowState.Minimized)
                {
                    LoadOgrenciKartlari();
                }
            };
            
            scrollableControl1.Resize += (s, e) => {
                LoadOgrenciKartlari();
            };
        }
        
        private void ModernizeButtons()
        {
            // Detay butonu - Modern SVG ikon
            btnDetay.ImageOptions.ImageUri.Uri = "Info;Size16x16";
            btnDetay.ImageOptions.SvgImageSize = new Size(16, 16);
            btnDetay.Appearance.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btnDetay.Appearance.BackColor = Color.FromArgb(76, 175, 80);
            btnDetay.Appearance.ForeColor = Color.White;
            btnDetay.Appearance.Options.UseBackColor = true;
            btnDetay.Appearance.Options.UseForeColor = true;
            btnDetay.Enabled = false;
            
            // Güncelle butonu - Modern SVG ikon
            btnGuncelle.ImageOptions.ImageUri.Uri = "Edit;Size16x16";
            btnGuncelle.ImageOptions.SvgImageSize = new Size(16, 16);
            btnGuncelle.Appearance.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btnGuncelle.Appearance.BackColor = Color.FromArgb(33, 150, 243);
            btnGuncelle.Appearance.ForeColor = Color.White;
            btnGuncelle.Appearance.Options.UseBackColor = true;
            btnGuncelle.Appearance.Options.UseForeColor = true;
            btnGuncelle.Enabled = false;
            
            // Sil butonu - Modern SVG ikon
            btnSil.ImageOptions.ImageUri.Uri = "Delete;Size16x16";
            btnSil.ImageOptions.SvgImageSize = new Size(16, 16);
            btnSil.Appearance.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btnSil.Appearance.BackColor = Color.FromArgb(244, 67, 54);
            btnSil.Appearance.ForeColor = Color.White;
            btnSil.Appearance.Options.UseBackColor = true;
            btnSil.Appearance.Options.UseForeColor = true;
            btnSil.Enabled = false;
            
            // Kapat butonu
            btnKapat.ImageOptions.ImageUri.Uri = "Close;Size16x16";
            btnKapat.ImageOptions.SvgImageSize = new Size(16, 16);
            btnKapat.Appearance.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btnKapat.Appearance.BackColor = Color.FromArgb(158, 158, 158);
            btnKapat.Appearance.ForeColor = Color.White;
            btnKapat.Appearance.Options.UseBackColor = true;
            btnKapat.Appearance.Options.UseForeColor = true;
        }
        
        private Ogrenci? _selectedOgrenci = null;
        
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            if (!_isUpdateMode)
                return;
            
            // Seçili öğrenci ID'sini kontrol et
            if (_selectedOgrenci == null || _selectedOgrenci.OgrenciID <= 0)
            {
                XtraMessageBox.Show("Lütfen güncellemek için bir öğrenci seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                var form = new OgrenciEkleGuncelleForm(_selectedOgrenci);
                if (form.ShowDialog() == DialogResult.OK)
                {
                    _selectedOgrenci = null;
                    LoadOgrenciKartlari();
                    UpdateButtonStates();
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Güncelleme sırasında hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void LoadOgrenciKartlari()
        {
            // Mevcut kartları temizle
            panelKartlar.Controls.Clear();
            
            var ogrenciler = _ogrenciService.GetAll();
            var sehirKatsayiService = new SehirKatsayiService();
            
            // Kompakt kart boyutları
            int cardWidth = 260;
            int cardHeight = 95;
            int spacing = 10;
            int padding = 10;
            
            // Form genişliğine göre satır başına kart sayısını dinamik hesapla
            int availableWidth = scrollableControl1.Width - (padding * 2);
            int cardsPerRow = Math.Max(1, (availableWidth + spacing) / (cardWidth + spacing));
            
            int x = padding;
            int y = padding;
            
            foreach (var ogrenci in ogrenciler)
            {
                // Şehir katsayısını hesapla
                string? sehir = sehirKatsayiService.GetSehirByUniversite(ogrenci.Universite);
                ogrenci.Sehir = sehir;
                ogrenci.SehirKatsayi = sehirKatsayiService.GetKatsayiBySehir(sehir);
                
                // Her öğrenci için bir kart oluştur
                var card = CreateOgrenciKarti(ogrenci);
                card.Location = new Point(x, y);
                
                panelKartlar.Controls.Add(card);
                
                // Bir sonraki kartın pozisyonunu hesapla (FlowLayoutPanel mantığı)
                x += cardWidth + spacing;
                
                // Eğer sağ tarafa taşacaksa alt satıra geç
                if (x + cardWidth > availableWidth + padding)
                {
                    x = padding;
                    y += cardHeight + spacing;
                }
            }
            
            // Panel yüksekliğini ayarla
            if (panelKartlar.Controls.Count > 0)
            {
                panelKartlar.Height = y + cardHeight + padding;
            }
            
            // Panel genişliğini form genişliğine ayarla
            panelKartlar.Width = availableWidth + (padding * 2);
        }
        
        private PanelControl CreateOgrenciKarti(Ogrenci ogrenci)
        {
            // Ana kart paneli - Kompakt tasarım
            var cardPanel = new PanelControl();
            cardPanel.Size = new Size(260, 95);
            cardPanel.Margin = new Padding(0);
            cardPanel.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
            cardPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            cardPanel.Appearance.BackColor = Color.White;
            cardPanel.Tag = ogrenci;
            cardPanel.Cursor = Cursors.Hand;
            
            // Hover efekti - Arka plan rengi değişir (her zaman aktif)
            cardPanel.MouseEnter += (s, e) => {
                // Seçili değilse hover efekti göster
                if (_selectedOgrenci?.OgrenciID != ogrenci.OgrenciID)
                {
                    cardPanel.Appearance.BackColor = Color.FromArgb(240, 248, 255);
                    cardPanel.Appearance.BorderColor = Color.FromArgb(100, 181, 246);
                }
                // Seçili olsa bile hafif bir hover efekti göster
                else
                {
                    cardPanel.Appearance.BackColor = Color.FromArgb(225, 240, 255);
                }
            };
            
            cardPanel.MouseLeave += (s, e) => {
                // Seçili değilse normal duruma dön
                if (_selectedOgrenci?.OgrenciID != ogrenci.OgrenciID)
                {
                    cardPanel.Appearance.BackColor = Color.White;
                    cardPanel.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
                }
                // Seçili ise seçili rengine dön
                else
                {
                    cardPanel.Appearance.BackColor = Color.FromArgb(235, 245, 255);
                    cardPanel.Appearance.BorderColor = Color.FromArgb(33, 150, 243);
                }
            };
            
            cardPanel.Click += (s, e) => {
                SelectCard(cardPanel, ogrenci);
            };
            
            // Fotoğraf container (kompakt)
            var photoContainer = new PanelControl();
            photoContainer.Location = new Point(7, 7);
            photoContainer.Size = new Size(75, 75);
            photoContainer.Appearance.BackColor = Color.FromArgb(245, 245, 250);
            photoContainer.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            
            // Fotoğraf (yuvarlak, küçük)
            var pictureEdit = new PictureEdit();
            pictureEdit.Location = new Point(2, 2);
            pictureEdit.Size = new Size(71, 71);
            pictureEdit.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            pictureEdit.Properties.ShowMenu = false;
            pictureEdit.Properties.ReadOnly = true;
            pictureEdit.Properties.NullText = "";
            
            // Fotoğraf yükle - ImageHelper kullan
            var profileImage = ImageHelper.LoadProfilFoto(ogrenci.ProfilFotoYolu);
            if (profileImage != null)
            {
                pictureEdit.Image = CreateCircularImage(profileImage, 71);
            }
            else
            {
                // Varsayılan avatar oluştur
                pictureEdit.Image = CreateDefaultAvatar(ogrenci.Ad, ogrenci.Soyad, 71);
            }
            
            photoContainer.Controls.Add(pictureEdit);
            
            // Bilgi paneli (sağ taraf, kompakt)
            var infoPanel = new PanelControl();
            infoPanel.Location = new Point(88, 7);
            infoPanel.Size = new Size(165, 81);
            infoPanel.Appearance.BackColor = Color.Transparent;
            infoPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            
            // İsim - Kompakt
            var lblAdSoyad = new LabelControl();
            lblAdSoyad.Location = new Point(0, 0);
            lblAdSoyad.Size = new Size(165, 20);
            lblAdSoyad.Text = $"{ogrenci.Ad} {ogrenci.Soyad}";
            lblAdSoyad.Appearance.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            lblAdSoyad.Appearance.ForeColor = Color.FromArgb(25, 25, 25);
            
            // Telefon - Kompakt
            var lblTelefon = new LabelControl();
            lblTelefon.Location = new Point(0, 22);
            lblTelefon.Size = new Size(165, 17);
            lblTelefon.Text = $"☎ {ogrenci.Telefon ?? "Belirtilmemiş"}";
            lblTelefon.Appearance.Font = new Font("Segoe UI", 8.5f);
            lblTelefon.Appearance.ForeColor = Color.FromArgb(100, 100, 100);
            
            // Bölüm - Kompakt
            var lblBolum = new LabelControl();
            lblBolum.Location = new Point(0, 41);
            lblBolum.Size = new Size(165, 17);
            lblBolum.Text = $"🎓 {ogrenci.Bolum ?? "Belirtilmemiş"}";
            lblBolum.Appearance.Font = new Font("Segoe UI", 8.5f);
            lblBolum.Appearance.ForeColor = Color.FromArgb(100, 100, 100);
            
            // Üniversite - Kompakt
            var lblUniversite = new LabelControl();
            lblUniversite.Location = new Point(0, 60);
            lblUniversite.Size = new Size(165, 15);
            lblUniversite.Text = $"🏛️ {ogrenci.Universite ?? "Belirtilmemiş"}";
            lblUniversite.Appearance.Font = new Font("Segoe UI", 8);
            lblUniversite.Appearance.ForeColor = Color.FromArgb(150, 150, 150);
            
            // Kontrolleri ekle
            infoPanel.Controls.Add(lblAdSoyad);
            infoPanel.Controls.Add(lblTelefon);
            infoPanel.Controls.Add(lblBolum);
            infoPanel.Controls.Add(lblUniversite);
            
            cardPanel.Controls.Add(photoContainer);
            cardPanel.Controls.Add(infoPanel);
            
            return cardPanel;
        }
        
        private void SelectCard(PanelControl cardPanel, Ogrenci ogrenci)
        {
            // Seçili öğrenci ID'sini yakala
            int selectedOgrenciID = ogrenci.OgrenciID;
            
            // Tüm kartların seçim durumunu sıfırla
            foreach (Control ctrl in panelKartlar.Controls)
            {
                if (ctrl is PanelControl panel && panel.Tag is Ogrenci)
                {
                    var panelOgrenci = panel.Tag as Ogrenci;
                    if (panelOgrenci?.OgrenciID != selectedOgrenciID)
                    {
                        panel.Appearance.BackColor = Color.White;
                        panel.Appearance.BorderColor = Color.FromArgb(230, 230, 230);
                        panel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
                    }
                }
            }
            
            // Seçili kartı vurgula - Modern mavi ton
            cardPanel.Appearance.BackColor = Color.FromArgb(235, 245, 255);
            cardPanel.Appearance.BorderColor = Color.FromArgb(33, 150, 243);
            cardPanel.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            
            // Seçili öğrenciyi güvenli şekilde kaydet
            _selectedOgrenci = ogrenci;
            
            // Butonları aktif et
            UpdateButtonStates();
        }
        
        private void UpdateButtonStates()
        {
            // Seçili öğrenci ID'sini kontrol et
            bool hasSelection = _selectedOgrenci != null && _selectedOgrenci.OgrenciID > 0;
            btnDetay.Enabled = hasSelection;
            btnGuncelle.Enabled = hasSelection && _isUpdateMode;
            btnSil.Enabled = hasSelection;
        }
        
        private void btnDetay_Click(object sender, EventArgs e)
        {
            // Seçili öğrenci ID'sini kontrol et
            if (_selectedOgrenci == null || _selectedOgrenci.OgrenciID <= 0)
            {
                XtraMessageBox.Show("Lütfen detaylarını görmek için bir öğrenci seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                // Seçili öğrenci ID'si ile detayları göster
                var detailMessage = $"Öğrenci Detayları\n\n" +
                    $"ID: {_selectedOgrenci.OgrenciID}\n" +
                    $"Ad Soyad: {_selectedOgrenci.Ad} {_selectedOgrenci.Soyad}\n" +
                    $"TC: {_selectedOgrenci.TC}\n" +
                    $"Telefon: {_selectedOgrenci.Telefon ?? "Belirtilmemiş"}\n" +
                    $"Email: {_selectedOgrenci.Email ?? "Belirtilmemiş"}\n" +
                    $"Üniversite: {_selectedOgrenci.Universite ?? "Belirtilmemiş"}\n" +
                    $"Fakülte: {_selectedOgrenci.Fakulte ?? "Belirtilmemiş"}\n" +
                    $"Bölüm: {_selectedOgrenci.Bolum ?? "Belirtilmemiş"}\n" +
                    $"Sınıf: {_selectedOgrenci.Sinif}\n" +
                    $"Not Ortalaması: {_selectedOgrenci.NotOrtalamasi:F2}\n" +
                    $"Burs Puanı: {_selectedOgrenci.BursPuani:F2}\n" +
                    $"Şehir: {_selectedOgrenci.Sehir ?? "Belirtilmemiş"}\n" +
                    $"Şehir Katsayısı: {_selectedOgrenci.SehirKatsayi:F2}";
                
                XtraMessageBox.Show(detailMessage, "Öğrenci Detayları", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Detay gösterilirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
        private Image CreateCircularImage(Image originalImage, int size)
        {
            var bitmap = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                
                // Yuvarlak mask oluştur
                using (var path = new GraphicsPath())
                {
                    path.AddEllipse(0, 0, size, size);
                    g.SetClip(path);
                }
                
                // Resmi çiz
                g.DrawImage(originalImage, 0, 0, size, size);
            }
            return bitmap;
        }
        
        private Image CreateDefaultAvatar(string ad, string soyad, int size)
        {
            var bitmap = new Bitmap(size, size);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                
                // Arka plan - Gradient
                using (var brush = new LinearGradientBrush(
                    new Rectangle(0, 0, size, size),
                    Color.FromArgb(100, 181, 246),
                    Color.FromArgb(66, 165, 245),
                    LinearGradientMode.Vertical))
                {
                    g.FillEllipse(brush, 0, 0, size, size);
                }
                
                // İlk harfler
                string initials = "";
                if (!string.IsNullOrEmpty(ad))
                    initials += ad[0].ToString().ToUpper();
                if (!string.IsNullOrEmpty(soyad))
                    initials += soyad[0].ToString().ToUpper();
                
                if (string.IsNullOrEmpty(initials))
                    initials = "?";
                
                // Harfleri çiz
                using (var font = new Font("Segoe UI", size / 2.5f, FontStyle.Bold))
                using (var brush = new SolidBrush(Color.White))
                {
                    var sf = new StringFormat
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    g.DrawString(initials, font, brush, new RectangleF(0, 0, size, size), sf);
                }
            }
            return bitmap;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            // Seçili öğrenci ID'sini kontrol et
            if (_selectedOgrenci == null || _selectedOgrenci.OgrenciID <= 0)
            {
                XtraMessageBox.Show("Lütfen silmek için bir öğrenci seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                int ogrenciID = _selectedOgrenci.OgrenciID;
                string ogrenciAdSoyad = $"{_selectedOgrenci.Ad} {_selectedOgrenci.Soyad}";
                
                var result = XtraMessageBox.Show(
                    $"'{ogrenciAdSoyad}' adlı öğrenciyi (ID: {ogrenciID}) silmek istediğinizden emin misiniz?", 
                    "Onay", 
                    MessageBoxButtons.YesNo, 
                    MessageBoxIcon.Question);
                
                if (result == DialogResult.Yes)
                {
                    if (_ogrenciService.Delete(ogrenciID))
                    {
                        XtraMessageBox.Show("Öğrenci başarıyla silindi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        _selectedOgrenci = null;
                        LoadOgrenciKartlari();
                        UpdateButtonStates();
                    }
                    else
                    {
                        XtraMessageBox.Show("Öğrenci silinemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Öğrenci silinirken hata oluştu: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

