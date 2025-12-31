using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using System.ComponentModel;
using BursOtomasyon.Desktop.Models;
using BursOtomasyon.Desktop.Services;

namespace BursOtomasyon.Desktop.Forms
{
    public partial class BasvuruListeForm : XtraForm
    {
        private BasvuruService _basvuruService;
        private OgrenciService _ogrenciService;
        private int _currentAdminId = 1; // Varsayılan admin ID

        public BasvuruListeForm()
        {
            InitializeComponent();

            // Tasarım modunda veritabanına gitme
            if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                return;

            _basvuruService = new BasvuruService();
            _ogrenciService = new OgrenciService();
            LoadBasvurular();
            ConfigureGrid();
            AddCheckboxColumn();
        }

        private void ConfigureGrid()
        {
            gridView1.OptionsView.ShowGroupPanel = false;
            gridView1.OptionsView.ShowIndicator = true;
            gridView1.OptionsSelection.MultiSelect = false;
            gridView1.OptionsBehavior.Editable = true; // Checkbox'lar için editable olmalı
            gridView1.OptionsView.ShowAutoFilterRow = true;
            
            // Sadece öğrenci bilgilerini göster, AI analiz raporunu gizle
            gridView1.Columns["AIYorum"].Visible = false;
            gridView1.Columns["OnaylayanAdminID"].Visible = false;
            gridView1.Columns["OnayTarihi"].Visible = false;
            
            // Kolon başlıklarını düzenle
            if (gridView1.Columns["OgrenciAdSoyad"] != null)
                gridView1.Columns["OgrenciAdSoyad"].Caption = "Öğrenci Ad Soyad";
            if (gridView1.Columns["OgrenciTC"] != null)
                gridView1.Columns["OgrenciTC"].Caption = "TC Kimlik No";
            if (gridView1.Columns["OgrenciEmail"] != null)
                gridView1.Columns["OgrenciEmail"].Caption = "E-Posta";
            if (gridView1.Columns["OgrenciTelefon"] != null)
                gridView1.Columns["OgrenciTelefon"].Caption = "Telefon";
            if (gridView1.Columns["OgrenciUniversite"] != null)
                gridView1.Columns["OgrenciUniversite"].Caption = "Üniversite";
            if (gridView1.Columns["OgrenciBolum"] != null)
                gridView1.Columns["OgrenciBolum"].Caption = "Bölüm";
            if (gridView1.Columns["OgrenciSinif"] != null)
                gridView1.Columns["OgrenciSinif"].Caption = "Sınıf";
            if (gridView1.Columns["OgrenciNotOrtalamasi"] != null)
            {
                gridView1.Columns["OgrenciNotOrtalamasi"].Caption = "Not Ortalaması";
                gridView1.Columns["OgrenciNotOrtalamasi"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView1.Columns["OgrenciNotOrtalamasi"].DisplayFormat.FormatString = "F2";
            }
            if (gridView1.Columns["OgrenciBursPuani"] != null)
            {
                gridView1.Columns["OgrenciBursPuani"].Caption = "Burs Puanı";
                gridView1.Columns["OgrenciBursPuani"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView1.Columns["OgrenciBursPuani"].DisplayFormat.FormatString = "F2";
            }
            if (gridView1.Columns["BasvuruDurumu"] != null)
                gridView1.Columns["BasvuruDurumu"].Caption = "Durum";
            if (gridView1.Columns["BasvuruTarihi"] != null)
                gridView1.Columns["BasvuruTarihi"].Caption = "Başvuru Tarihi";
            
            gridView1.BestFitColumns();
        }

        private void AddCheckboxColumn()
        {
            // Eğer kolon zaten varsa, ekleme
            if (gridView1.Columns["Secildi"] != null)
                return;
            
            // Checkbox kolonu ekle
            var checkColumn = new GridColumn();
            checkColumn.FieldName = "Secildi";
            checkColumn.Caption = "Seç";
            checkColumn.VisibleIndex = 0;
            checkColumn.Width = 50;
            checkColumn.OptionsColumn.AllowEdit = true;
            checkColumn.OptionsColumn.FixedWidth = true;
            
            // Checkbox repository item oluştur
            var checkEdit = new RepositoryItemCheckEdit();
            checkColumn.ColumnEdit = checkEdit;
            
            gridView1.Columns.Add(checkColumn);
        }

        private void LoadBasvurular()
        {
            var basvurular = _basvuruService.GetAll();
            // Her başvurunun Secildi property'sini false yap
            foreach (var basvuru in basvurular)
            {
                basvuru.Secildi = false;
            }
            gridControl1.DataSource = basvurular;
            
            // Checkbox kolonunu tekrar ekle (eğer yoksa)
            if (gridView1.Columns["Secildi"] == null)
            {
                AddCheckboxColumn();
            }
        }

        private void btnDetay_Click(object sender, EventArgs e)
        {
            var selectedRow = gridView1.GetFocusedRow();
            if (selectedRow != null)
            {
                var basvuru = gridView1.GetRow(gridView1.FocusedRowHandle) as Basvuru;
                if (basvuru != null)
                {
                    var form = new BasvuruDetayForm(basvuru);
                    form.ShowDialog();
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen detayını görmek için bir başvuru seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnOnayla_Click(object sender, EventArgs e)
        {
            var selectedRow = gridView1.GetFocusedRow();
            if (selectedRow != null)
            {
                var basvuru = gridView1.GetRow(gridView1.FocusedRowHandle) as Basvuru;
                if (basvuru != null)
                {
                    if (_basvuruService.UpdateDurum(basvuru.BasvuruID, "Onaylandı", _currentAdminId))
                    {
                        XtraMessageBox.Show("Başvuru onaylandı.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBasvurular();
                    }
                    else
                    {
                        XtraMessageBox.Show("Başvuru onaylanamadı!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen onaylamak için bir başvuru seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnReddet_Click(object sender, EventArgs e)
        {
            var selectedRow = gridView1.GetFocusedRow();
            if (selectedRow != null)
            {
                var basvuru = gridView1.GetRow(gridView1.FocusedRowHandle) as Basvuru;
                if (basvuru != null)
                {
                    if (_basvuruService.UpdateDurum(basvuru.BasvuruID, "Reddedildi", _currentAdminId))
                    {
                        XtraMessageBox.Show("Başvuru reddedildi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadBasvurular();
                    }
                    else
                    {
                        XtraMessageBox.Show("Başvuru reddedilemedi!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("Lütfen reddetmek için bir başvuru seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnSecilenleriSil_Click(object sender, EventArgs e)
        {
            var secilenBasvurular = new List<Basvuru>();
            
            // Seçili başvuruları topla
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                var basvuru = gridView1.GetRow(i) as Basvuru;
                if (basvuru != null && basvuru.Secildi)
                {
                    secilenBasvurular.Add(basvuru);
                }
            }
            
            if (secilenBasvurular.Count == 0)
            {
                XtraMessageBox.Show("Lütfen silmek için en az bir başvuru seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            var result = XtraMessageBox.Show(
                $"{secilenBasvurular.Count} başvurunun öğrencilerini silmek istediğinizden emin misiniz?\n\nBu işlem geri alınamaz!",
                "Onay",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);
            
            if (result == DialogResult.Yes)
            {
                int silinenSayisi = 0;
                int hataSayisi = 0;
                
                foreach (var basvuru in secilenBasvurular)
                {
                    try
                    {
                        // Önce başvuruları sil (foreign key constraint için)
                        _basvuruService.DeleteByOgrenciId(basvuru.OgrenciID);
                        
                        // Sonra öğrenciyi sil
                        if (_ogrenciService.Delete(basvuru.OgrenciID))
                        {
                            silinenSayisi++;
                        }
                        else
                        {
                            hataSayisi++;
                        }
                    }
                    catch
                    {
                        hataSayisi++;
                    }
                }
                
                string mesaj = $"{silinenSayisi} öğrenci başarıyla silindi.";
                if (hataSayisi > 0)
                {
                    mesaj += $"\n{hataSayisi} öğrenci silinirken hata oluştu.";
                }
                
                XtraMessageBox.Show(mesaj, "Sonuç", MessageBoxButtons.OK, 
                    hataSayisi > 0 ? MessageBoxIcon.Warning : MessageBoxIcon.Information);
                
                LoadBasvurular();
            }
        }
    }
}

