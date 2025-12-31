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

