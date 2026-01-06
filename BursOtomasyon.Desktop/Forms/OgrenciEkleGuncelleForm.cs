using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BursOtomasyon.Desktop.Models;
using BursOtomasyon.Desktop.Services;
using BursOtomasyon.Desktop.Helpers;

namespace BursOtomasyon.Desktop.Forms
{
    public partial class OgrenciEkleGuncelleForm : OgrenciEkleForm
    {
        private Ogrenci _ogrenci;
        private OgrenciService _ogrenciService;
        private BursPuanlamaService _puanlamaService;

        public OgrenciEkleGuncelleForm(Ogrenci ogrenci) : base()
        {
            _ogrenci = ogrenci;
            _ogrenciService = new OgrenciService();
            _puanlamaService = new BursPuanlamaService();
            LoadOgrenciData();
        }

        private void LoadOgrenciData()
        {
            if (_ogrenci != null)
            {
                txtAd.Text = _ogrenci.Ad;
                txtSoyad.Text = _ogrenci.Soyad;
                txtTC.Text = _ogrenci.TC;
                dtpDogumTarihi.EditValue = _ogrenci.DogumTarihi;
                txtTelefon.Text = _ogrenci.Telefon ?? string.Empty;
                txtEmail.Text = _ogrenci.Email ?? string.Empty;
                cmbUniversite.Text = _ogrenci.Universite ?? string.Empty;
                txtFakulte.Text = _ogrenci.Fakulte ?? string.Empty;
                txtBolum.Text = _ogrenci.Bolum ?? string.Empty;
                cmbSinif.Text = _ogrenci.Sinif;
                txtNotOrtalamasi.Text = _ogrenci.NotOrtalamasi.ToString();
                cmbKardesSayisi.Text = _ogrenci.KardesSayisi;
                txtAileGeliri.Text = _ogrenci.AileGeliri.ToString();
                txtAnneMeslek.Text = _ogrenci.AnneMeslek ?? string.Empty;
                txtBabaMeslek.Text = _ogrenci.BabaMeslek ?? string.Empty;

                // Profil fotoğrafını yükle
                if (!string.IsNullOrEmpty(_ogrenci.ProfilFotoYolu))
                {
                    pictureEdit1.Image = ImageHelper.LoadProfilFoto(_ogrenci.ProfilFotoYolu);
                }

                this.Text = "Öğrenci Güncelle";
                // Buton metnini "Güncelle" olarak değiştir
                btnKaydet.Text = "Güncelle";
            }
        }

        private void InitializeComponent()
        {
            ((System.ComponentModel.ISupportInitialize)txtAd.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtSoyad.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtTC.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtpDogumTarihi.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dtpDogumTarihi.Properties.CalendarTimeProperties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtTelefon.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtEmail.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbUniversite.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtFakulte.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtBolum.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbSinif.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtNotOrtalamasi.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbKardesSayisi.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtAileGeliri.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtAnneMeslek.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtBabaMeslek.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).BeginInit();
            SuspendLayout();
            // 
            // txtAd
            // 
            txtAd.Location = new Point(149, 55);
            txtAd.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtAd.Properties.Appearance.Options.UseFont = true;
            txtAd.Size = new Size(174, 28);
            // 
            // txtSoyad
            // 
            txtSoyad.Location = new Point(149, 87);
            txtSoyad.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtSoyad.Properties.Appearance.Options.UseFont = true;
            txtSoyad.Size = new Size(174, 28);
            // 
            // txtTC
            // 
            txtTC.Location = new Point(149, 119);
            txtTC.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtTC.Properties.Appearance.Options.UseFont = true;
            txtTC.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            txtTC.Properties.MaskSettings.Set("mask", "00000000000");
            txtTC.Size = new Size(174, 28);
            // 
            // dtpDogumTarihi
            // 
            dtpDogumTarihi.EditValue = null;
            dtpDogumTarihi.Location = new Point(149, 151);
            dtpDogumTarihi.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            dtpDogumTarihi.Properties.Appearance.Options.UseFont = true;
            dtpDogumTarihi.Size = new Size(174, 28);
            // 
            // txtTelefon
            // 
            txtTelefon.Location = new Point(149, 238);
            txtTelefon.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtTelefon.Properties.Appearance.Options.UseFont = true;
            txtTelefon.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.RegularMaskManager));
            txtTelefon.Properties.MaskSettings.Set("mask", "0 (\\d\\d\\d) \\d\\d\\d \\d\\d \\d\\d");
            txtTelefon.Properties.MaskSettings.Set("SaveLiteral", false);
            txtTelefon.Properties.MaskSettings.Set("AutoComplete", DevExpress.Utils.DefaultBoolean.False);
            txtTelefon.Properties.MaskSettings.Set("PlaceHolder", ' ');
            txtTelefon.Size = new Size(174, 28);
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(149, 270);
            txtEmail.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtEmail.Properties.Appearance.Options.UseFont = true;
            txtEmail.Size = new Size(174, 28);
            // 
            // cmbUniversite
            // 
            cmbUniversite.Location = new Point(149, 357);
            cmbUniversite.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            cmbUniversite.Properties.Appearance.Options.UseFont = true;
            cmbUniversite.Size = new Size(174, 28);
            // 
            // txtFakulte
            // 
            txtFakulte.Location = new Point(149, 389);
            txtFakulte.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtFakulte.Properties.Appearance.Options.UseFont = true;
            txtFakulte.Size = new Size(174, 28);
            // 
            // txtBolum
            // 
            txtBolum.Location = new Point(149, 421);
            txtBolum.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtBolum.Properties.Appearance.Options.UseFont = true;
            txtBolum.Size = new Size(174, 28);
            // 
            // cmbSinif
            // 
            cmbSinif.Location = new Point(149, 453);
            cmbSinif.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            cmbSinif.Properties.Appearance.Options.UseFont = true;
            cmbSinif.Size = new Size(174, 28);
            // 
            // txtNotOrtalamasi
            // 
            txtNotOrtalamasi.Location = new Point(149, 485);
            txtNotOrtalamasi.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtNotOrtalamasi.Properties.Appearance.Options.UseFont = true;
            txtNotOrtalamasi.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            txtNotOrtalamasi.Properties.MaskSettings.Set("mask", "0.00");
            txtNotOrtalamasi.Size = new Size(174, 28);
            // 
            // cmbKardesSayisi
            // 
            cmbKardesSayisi.Location = new Point(149, 572);
            cmbKardesSayisi.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            cmbKardesSayisi.Properties.Appearance.Options.UseFont = true;
            cmbKardesSayisi.Size = new Size(174, 28);
            // 
            // txtAileGeliri
            // 
            txtAileGeliri.Location = new Point(149, 604);
            txtAileGeliri.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtAileGeliri.Properties.Appearance.Options.UseFont = true;
            txtAileGeliri.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            txtAileGeliri.Properties.MaskSettings.Set("mask", "n");
            txtAileGeliri.Size = new Size(174, 28);
            // 
            // txtAnneMeslek
            // 
            txtAnneMeslek.Location = new Point(149, 636);
            txtAnneMeslek.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtAnneMeslek.Properties.Appearance.Options.UseFont = true;
            txtAnneMeslek.Size = new Size(174, 28);
            // 
            // txtBabaMeslek
            // 
            txtBabaMeslek.Location = new Point(149, 668);
            txtBabaMeslek.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtBabaMeslek.Properties.Appearance.Options.UseFont = true;
            txtBabaMeslek.Size = new Size(174, 28);
            // 
            // btnKaydet
            // 
            btnKaydet.Appearance.BackColor = Color.FromArgb(76, 175, 80);
            btnKaydet.Appearance.Font = new Font("Segoe UI", 10F);
            btnKaydet.Appearance.ForeColor = Color.White;
            btnKaydet.Appearance.Options.UseBackColor = true;
            btnKaydet.Appearance.Options.UseFont = true;
            btnKaydet.Appearance.Options.UseForeColor = true;
            btnKaydet.ImageOptions.ImageUri.Uri = "Save;Size16x16";
            btnKaydet.ImageOptions.SvgImageSize = new Size(16, 16);
            btnKaydet.Location = new Point(24, 724);
            btnKaydet.Size = new Size(309, 28);
            // 
            // btnIptal
            // 
            btnIptal.Appearance.BackColor = Color.FromArgb(158, 158, 158);
            btnIptal.Appearance.Font = new Font("Segoe UI", 10F);
            btnIptal.Appearance.ForeColor = Color.White;
            btnIptal.Appearance.Options.UseBackColor = true;
            btnIptal.Appearance.Options.UseFont = true;
            btnIptal.Appearance.Options.UseForeColor = true;
            btnIptal.ImageOptions.ImageUri.Uri = "Cancel;Size16x16";
            btnIptal.ImageOptions.SvgImageSize = new Size(16, 16);
            btnIptal.Location = new Point(345, 724);
            btnIptal.Size = new Size(310, 28);
            // 
            // pictureEdit1
            // 
            pictureEdit1.Location = new Point(365, 65);
            pictureEdit1.Properties.Appearance.BackColor = Color.FromArgb(248, 249, 250);
            pictureEdit1.Properties.Appearance.BorderColor = Color.FromArgb(200, 200, 200);
            pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            pictureEdit1.Properties.Appearance.Options.UseBorderColor = true;
            pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Stretch;
            pictureEdit1.Size = new Size(276, 569);
            // 
            // btnFotoSec
            // 
            btnFotoSec.Appearance.BackColor = Color.FromArgb(33, 150, 243);
            btnFotoSec.Appearance.Font = new Font("Segoe UI", 10F);
            btnFotoSec.Appearance.ForeColor = Color.White;
            btnFotoSec.Appearance.Options.UseBackColor = true;
            btnFotoSec.Appearance.Options.UseFont = true;
            btnFotoSec.Appearance.Options.UseForeColor = true;
            btnFotoSec.ImageOptions.ImageUri.Uri = "Open;Size16x16";
            btnFotoSec.ImageOptions.SvgImageSize = new Size(16, 16);
            btnFotoSec.Location = new Point(365, 658);
            btnFotoSec.Size = new Size(276, 28);
            // 
            // OgrenciEkleGuncelleForm
            // 
            Appearance.BackColor = Color.White;
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(7F, 16F);
            ClientSize = new Size(700, 800);
            Name = "OgrenciEkleGuncelleForm";
            ((System.ComponentModel.ISupportInitialize)txtAd.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtSoyad.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtTC.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtpDogumTarihi.Properties.CalendarTimeProperties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dtpDogumTarihi.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtTelefon.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtEmail.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbUniversite.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtFakulte.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtBolum.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbSinif.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtNotOrtalamasi.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbKardesSayisi.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtAileGeliri.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtAnneMeslek.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtBabaMeslek.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).EndInit();
            ResumeLayout(false);

        }

        protected override void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Burs puanını etkileyen alanların eski değerlerini sakla
                decimal eskiNotOrtalamasi = _ogrenci.NotOrtalamasi;
                decimal eskiAileGeliri = _ogrenci.AileGeliri;
                string eskiKardesSayisi = _ogrenci.KardesSayisi;
                string eskiSinif = _ogrenci.Sinif;

                _ogrenci.Ad = txtAd.Text ?? string.Empty;
                _ogrenci.Soyad = txtSoyad.Text ?? string.Empty;
                _ogrenci.TC = txtTC.Text ?? string.Empty;
                _ogrenci.DogumTarihi = dtpDogumTarihi.EditValue != null ? (DateTime)dtpDogumTarihi.EditValue : DateTime.Now;
                _ogrenci.Telefon = txtTelefon.Text;
                _ogrenci.Email = txtEmail.Text;
                _ogrenci.Universite = cmbUniversite.Text;
                _ogrenci.Fakulte = txtFakulte.Text;
                _ogrenci.Bolum = txtBolum.Text;
                _ogrenci.Sinif = cmbSinif.Text ?? "1";
                _ogrenci.NotOrtalamasi = decimal.Parse(txtNotOrtalamasi.Text ?? "0");
                _ogrenci.KardesSayisi = cmbKardesSayisi.Text ?? "0";
                _ogrenci.AileGeliri = decimal.Parse(txtAileGeliri.Text ?? "0");
                _ogrenci.AnneMeslek = txtAnneMeslek.Text;
                _ogrenci.BabaMeslek = txtBabaMeslek.Text;

                // Profil fotoğrafını güncelle (varsa)
                if (pictureEdit1.Image != null && !string.IsNullOrEmpty(_selectedImagePath))
                {
                    _ogrenci.ProfilFotoYolu = SaveProfilFoto(_selectedImagePath);
                }

                // Burs puanını etkileyen alanlar değişti mi kontrol et
                bool puanEtkilenenAlanDegisti =
                    eskiNotOrtalamasi != _ogrenci.NotOrtalamasi ||
                    eskiAileGeliri != _ogrenci.AileGeliri ||
                    eskiKardesSayisi != _ogrenci.KardesSayisi ||
                    eskiSinif != _ogrenci.Sinif;

                // Sadece puanı etkileyen alanlar değiştiyse yeniden hesapla
                if (puanEtkilenenAlanDegisti)
                {
                    _ogrenci.BursPuani = _puanlamaService.HesaplaBursPuani(_ogrenci);
                }

                if (_ogrenciService.Update(_ogrenci))
                {
                    MessageBox.Show($"Öğrenci başarıyla güncellendi!\nYeni Burs Puanı: {_ogrenci.BursPuani:F2}",
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Öğrenci güncellenemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (InvalidOperationException ex)
            {
                // TC duplicate hatası gibi özel hatalar
                MessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string? _selectedImagePath;

        private void btnFotoSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Tüm Dosyalar|*.*";
                dialog.Title = "Profil Fotoğrafı Seç";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    _selectedImagePath = dialog.FileName;
                    pictureEdit1.Image = System.Drawing.Image.FromFile(_selectedImagePath);
                }
            }
        }

        private string SaveProfilFoto(string sourcePath)
        {
            try
            {
                var currentDir = Directory.GetCurrentDirectory();
                var solutionRoot = currentDir;

                while (!string.IsNullOrEmpty(solutionRoot) && !Directory.GetFiles(solutionRoot, "*.sln").Any())
                {
                    var parent = Directory.GetParent(solutionRoot);
                    if (parent == null) break;
                    solutionRoot = parent.FullName;
                }

                var webRootPath = Path.Combine(solutionRoot, "BursOtomasyon.Web", "wwwroot", "profile-photos");
                if (!Directory.Exists(webRootPath))
                {
                    Directory.CreateDirectory(webRootPath);
                }

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(sourcePath)}";
                var destPath = Path.Combine(webRootPath, fileName);

                File.Copy(sourcePath, destPath, true);

                return "/profile-photos/" + fileName;
            }
            catch
            {
                return null;
            }
        }
    }
}

