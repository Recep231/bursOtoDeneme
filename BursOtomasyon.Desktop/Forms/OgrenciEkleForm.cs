using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BursOtomasyon.Desktop.Models;
using BursOtomasyon.Desktop.Services;

namespace BursOtomasyon.Desktop.Forms
{
    public partial class OgrenciEkleForm : XtraForm
    {
        private OgrenciService _ogrenciService;
        private BursPuanlamaService _puanlamaService;

        public OgrenciEkleForm()
        {
            InitializeComponent();
            _ogrenciService = new OgrenciService();
            _puanlamaService = new BursPuanlamaService();
            LoadComboBoxes();
        }

        private void LoadComboBoxes()
        {
            cmbSinif.Properties.Items.AddRange(new[] { "Hazırlık", "1", "2", "3", "4" });
            cmbKardesSayisi.Properties.Items.AddRange(new[] { "0", "1", "2", "3", "4", "4+" });
            
            // Üniversite listesini yükle (sadece üniversite adları, şehir adlarını filtrele)
            var sehirService = new SehirKatsayiService();
            var universiteList = sehirService.GetAllUniversiteSehirMap().Keys
                .Where(k => k.Contains("Üniversitesi") || k.Contains("Enstitüsü"))
                .Distinct()
                .OrderBy(k => k)
                .ToList();
            
            cmbUniversite.Properties.Items.AddRange(universiteList);
        }

        protected virtual void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Profil fotoğrafını kaydet (varsa)
                string? profilFotoYolu = null;
                if (pictureEdit1.Image != null && !string.IsNullOrEmpty(_selectedImagePath))
                {
                    profilFotoYolu = SaveProfilFoto(_selectedImagePath);
                }

                var ogrenci = new Ogrenci
                {
                    Ad = txtAd.Text ?? string.Empty,
                    Soyad = txtSoyad.Text ?? string.Empty,
                    TC = txtTC.Text ?? string.Empty,
                    DogumTarihi = dtpDogumTarihi.DateTime,
                    Telefon = txtTelefon.Text,
                    Email = txtEmail.Text,
                    Universite = cmbUniversite.Text,
                    Fakulte = txtFakulte.Text,
                    Bolum = txtBolum.Text,
                    Sinif = cmbSinif.Text ?? "1",
                    NotOrtalamasi = decimal.Parse(txtNotOrtalamasi.Text ?? "0"),
                    KardesSayisi = cmbKardesSayisi.Text ?? "0",
                    AileGeliri = decimal.Parse(txtAileGeliri.Text ?? "0"),
                    AnneMeslek = txtAnneMeslek.Text,
                    BabaMeslek = txtBabaMeslek.Text,
                    ProfilFotoYolu = profilFotoYolu
                };

                // Burs puanını hesapla
                ogrenci.BursPuani = _puanlamaService.HesaplaBursPuani(ogrenci);

                if (_ogrenciService.Add(ogrenci))
                {
                    XtraMessageBox.Show($"Öğrenci başarıyla kaydedildi!\nHesaplanan Burs Puanı: {ogrenci.BursPuani:F2}", 
                        "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    XtraMessageBox.Show("Öğrenci kaydedilemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (InvalidOperationException ex)
            {
                // TC duplicate hatası gibi özel hatalar
                XtraMessageBox.Show(ex.Message, "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string? _selectedImagePath;

        private void btnFotoSec_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Resim Dosyaları|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Tüm Dosyalar|*.*";
                dialog.Title = "Profil Fotoğrafı Seç";
                dialog.Multiselect = false;
                
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _selectedImagePath = dialog.FileName;
                        var image = System.Drawing.Image.FromFile(_selectedImagePath);
                        pictureEdit1.Image = image;
                    }
                    catch (Exception ex)
                    {
                        XtraMessageBox.Show($"Fotoğraf yüklenirken hata oluştu:\n{ex.Message}", 
                            "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private string SaveProfilFoto(string sourcePath)
        {
            try
            {
                // Web projesinin wwwroot klasörünü bul
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

        private void btnIptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}

