using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using BursOtomasyon.Desktop.Models;
using BursOtomasyon.Desktop.Services;

namespace BursOtomasyon.Desktop.Forms
{
    public partial class AIAnalizForm : XtraForm
    {
        private OgrenciService _ogrenciService;
        private BasvuruService _basvuruService;
        private AIAnalizService _aiService;
        private BursPuanlamaService _puanlamaService;

        public AIAnalizForm()
        {
            InitializeComponent();

            // Designer'da veritabanı / AI servisine gitme
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
            {
                return;
            }

            _ogrenciService = new OgrenciService();
            _basvuruService = new BasvuruService();
            _aiService = new AIAnalizService();
            _puanlamaService = new BursPuanlamaService();
            LoadOgrenciler();
            ConfigureGrid();
            SetupGridEvents();
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

        private void SetupGridEvents()
        {
            // Grid event'leri burada ayarlanabilir
        }

        private void LoadOgrenciler()
        {
            var ogrenciler = _ogrenciService.GetAll();
            gridControl1.DataSource = ogrenciler;
        }

        private async void btnAnalizYap_Click(object sender, EventArgs e)
        {
            var selectedRow = gridView1.GetFocusedRow();
            if (selectedRow == null)
            {
                XtraMessageBox.Show("Lütfen bir öğrenci seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var ogrenci = gridView1.GetRow(gridView1.FocusedRowHandle) as Ogrenci;
            if (ogrenci == null)
            {
                XtraMessageBox.Show("Öğrenci bulunamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                btnAnalizYap.Enabled = false;
                btnAnalizYap.Text = "Analiz Yapılıyor...";
                rtbAnaliz.Text = "AI analizi yapılıyor, lütfen bekleyin...\n\nBu işlem birkaç saniye sürebilir.";
                rtbAnaliz.Refresh();
                Application.DoEvents();

                // Burs puanını hesapla (yoksa veya 0 ise)
                decimal bursPuani = ogrenci.BursPuani;
                if (bursPuani <= 0)
                {
                    rtbAnaliz.Text = "Burs puanı hesaplanıyor...\n\n" + rtbAnaliz.Text;
                    rtbAnaliz.Refresh();
                    Application.DoEvents();
                    
                    bursPuani = _puanlamaService.HesaplaBursPuani(ogrenci);
                    
                    // Hesaplanan puanı göster
                    rtbAnaliz.Text = $"Burs Puanı: {bursPuani:F2}/100\n\nAI analizi yapılıyor...";
                    rtbAnaliz.Refresh();
                    Application.DoEvents();
                }

                // AI analizi yap
                rtbAnaliz.Text = $"Burs Puanı: {bursPuani:F2}/100\n\nAI analizi yapılıyor, lütfen bekleyin...";
                rtbAnaliz.Refresh();
                Application.DoEvents();
                
                string aiYorum;
                try
                {
                    aiYorum = await _aiService.AnalizYapAsync(ogrenci, bursPuani);
                    
                    if (string.IsNullOrWhiteSpace(aiYorum) || aiYorum.Contains("API anahtarı bulunamadı"))
                    {
                        // Fallback: Detaylı analiz dene
                        aiYorum = await _aiService.DetayliAnalizYapAsync(ogrenci, bursPuani);
                    }
                }
                catch (Exception aiEx)
                {
                    // AI hatası durumunda fallback analiz
                    aiYorum = $"AI analizi sırasında bir hata oluştu: {aiEx.Message}\n\n" +
                              _aiService.SimuleAnaliz(ogrenci, bursPuani);
                }
                
                // Sonucu göster
                rtbAnaliz.Text = $"═══════════════════════════════════════════════════════\n" +
                                $"BURS PUANI: {bursPuani:F2}/100\n" +
                                $"═══════════════════════════════════════════════════════\n\n" +
                                aiYorum;

                // Başvuru varsa AI yorumunu kaydet
                try
                {
                    var basvurular = _basvuruService.GetAll();
                    var basvuru = basvurular.FirstOrDefault(b => b.OgrenciID == ogrenci.OgrenciID && b.BasvuruDurumu == "Beklemede");
                    if (basvuru != null)
                    {
                        _basvuruService.UpdateAIYorum(basvuru.BasvuruID, aiYorum);
                    }
                }
                catch
                {
                    // Başvuru kaydetme hatası kritik değil
                }

                XtraMessageBox.Show("AI analizi tamamlandı!", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"Hata: {ex.Message}\n\nDetay: {ex.StackTrace}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rtbAnaliz.Text = $"═══════════════════════════════════════════════════════\n" +
                                $"HATA OLUŞTU\n" +
                                $"═══════════════════════════════════════════════════════\n\n" +
                                $"Hata Mesajı: {ex.Message}\n\n" +
                                $"Lütfen sistem yöneticisine başvurun.";
            }
            finally
            {
                btnAnalizYap.Enabled = true;
                btnAnalizYap.Text = "AI Analiz Yap";
            }
        }
    }
}

