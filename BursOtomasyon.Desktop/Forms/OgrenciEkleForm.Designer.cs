using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System.Drawing;
using System.Windows.Forms;

namespace BursOtomasyon.Desktop.Forms
{
    partial class OgrenciEkleForm
    {
        private System.ComponentModel.IContainer components = null;
        protected TextEdit txtAd = null!;
        protected TextEdit txtSoyad = null!;
        protected TextEdit txtTC = null!;
        protected DateEdit dtpDogumTarihi = null!;
        protected TextEdit txtTelefon = null!;
        protected TextEdit txtEmail = null!;
        protected ComboBoxEdit cmbUniversite = null!;
        protected TextEdit txtFakulte = null!;
        protected TextEdit txtBolum = null!;
        protected ComboBoxEdit cmbSinif = null!;
        protected TextEdit txtNotOrtalamasi = null!;
        protected ComboBoxEdit cmbKardesSayisi = null!;
        protected TextEdit txtAileGeliri = null!;
        protected TextEdit txtAnneMeslek = null!;
        protected TextEdit txtBabaMeslek = null!;
        protected SimpleButton btnKaydet = null!;
        protected SimpleButton btnIptal = null!;
        protected SimpleButton btnFotoSec = null!;
        private LayoutControl layoutControl1 = null!;
        private LayoutControlGroup layoutControlGroup1 = null!;
        private LayoutControlGroup groupSolTaraf = null!;
        private LayoutControlGroup groupSagTaraf = null!;
        private LayoutControlGroup groupKisiselBilgiler = null!;
        private LayoutControlGroup groupIletisim = null!;
        private LayoutControlGroup groupAkademik = null!;
        private LayoutControlGroup groupAile = null!;
        private LayoutControlGroup groupProfilFoto = null!;
        private LayoutControlItem itemProfilFoto = null!;
        private LayoutControlItem itemFotoSec = null!;
        private PanelControl panelHeader = null!;
        private LayoutControlItem itemAd = null!;
        private LayoutControlItem itemSoyad = null!;
        private LayoutControlItem itemTC = null!;
        private LayoutControlItem itemDogumTarihi = null!;
        private LayoutControlItem itemTelefon = null!;
        private LayoutControlItem itemEmail = null!;
        private LayoutControlItem itemUniversite = null!;
        private LayoutControlItem itemFakulte = null!;
        private LayoutControlItem itemBolum = null!;
        private LayoutControlItem itemSinif = null!;
        private LayoutControlItem itemNotOrtalamasi = null!;
        private LayoutControlItem itemKardesSayisi = null!;
        private LayoutControlItem itemAileGeliri = null!;
        private LayoutControlItem itemAnneMeslek = null!;
        private LayoutControlItem itemBabaMeslek = null!;
        private LayoutControlItem itemBtnKaydet = null!;
        private LayoutControlItem itemBtnIptal = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            layoutControl1 = new LayoutControl();
            txtAd = new TextEdit();
            txtSoyad = new TextEdit();
            txtTC = new TextEdit();
            dtpDogumTarihi = new DateEdit();
            txtTelefon = new TextEdit();
            txtEmail = new TextEdit();
            cmbUniversite = new ComboBoxEdit();
            txtFakulte = new TextEdit();
            txtBolum = new TextEdit();
            cmbSinif = new ComboBoxEdit();
            txtNotOrtalamasi = new TextEdit();
            cmbKardesSayisi = new ComboBoxEdit();
            txtAileGeliri = new TextEdit();
            txtAnneMeslek = new TextEdit();
            txtBabaMeslek = new TextEdit();
            btnKaydet = new SimpleButton();
            btnIptal = new SimpleButton();
            btnFotoSec = new SimpleButton();
            layoutControlGroup1 = new LayoutControlGroup();
            groupSolTaraf = new LayoutControlGroup();
            groupKisiselBilgiler = new LayoutControlGroup();
            itemAd = new LayoutControlItem();
            itemSoyad = new LayoutControlItem();
            itemTC = new LayoutControlItem();
            itemDogumTarihi = new LayoutControlItem();
            groupIletisim = new LayoutControlGroup();
            itemTelefon = new LayoutControlItem();
            itemEmail = new LayoutControlItem();
            groupAkademik = new LayoutControlGroup();
            itemUniversite = new LayoutControlItem();
            itemFakulte = new LayoutControlItem();
            itemBolum = new LayoutControlItem();
            itemSinif = new LayoutControlItem();
            itemNotOrtalamasi = new LayoutControlItem();
            groupAile = new LayoutControlGroup();
            itemKardesSayisi = new LayoutControlItem();
            itemAileGeliri = new LayoutControlItem();
            itemAnneMeslek = new LayoutControlItem();
            itemBabaMeslek = new LayoutControlItem();
            groupSagTaraf = new LayoutControlGroup();
            groupProfilFoto = new LayoutControlGroup();
            itemProfilFoto = new LayoutControlItem();
            itemFotoSec = new LayoutControlItem();
            itemBtnKaydet = new LayoutControlItem();
            itemBtnIptal = new LayoutControlItem();
            panelHeader = new PanelControl();
            labelControl1 = new LabelControl();
            pictureEdit1 = new PictureEdit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupSolTaraf).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupKisiselBilgiler).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemAd).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemSoyad).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemTC).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemDogumTarihi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupIletisim).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemTelefon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemEmail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupAkademik).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemUniversite).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemFakulte).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemBolum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemSinif).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemNotOrtalamasi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupAile).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemKardesSayisi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemAileGeliri).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemAnneMeslek).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemBabaMeslek).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupSagTaraf).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupProfilFoto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemProfilFoto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemFotoSec).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemBtnKaydet).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemBtnIptal).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelHeader).BeginInit();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).BeginInit();
            SuspendLayout();
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(txtAd);
            layoutControl1.Controls.Add(txtSoyad);
            layoutControl1.Controls.Add(txtTC);
            layoutControl1.Controls.Add(dtpDogumTarihi);
            layoutControl1.Controls.Add(txtTelefon);
            layoutControl1.Controls.Add(txtEmail);
            layoutControl1.Controls.Add(cmbUniversite);
            layoutControl1.Controls.Add(txtFakulte);
            layoutControl1.Controls.Add(txtBolum);
            layoutControl1.Controls.Add(cmbSinif);
            layoutControl1.Controls.Add(txtNotOrtalamasi);
            layoutControl1.Controls.Add(cmbKardesSayisi);
            layoutControl1.Controls.Add(txtAileGeliri);
            layoutControl1.Controls.Add(txtAnneMeslek);
            layoutControl1.Controls.Add(txtBabaMeslek);
            layoutControl1.Controls.Add(btnKaydet);
            layoutControl1.Controls.Add(btnIptal);
            layoutControl1.Controls.Add(btnFotoSec);
            layoutControl1.Controls.Add(pictureEdit1);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new Point(0, 50);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(1070, 200, 650, 400);
            layoutControl1.Root = layoutControlGroup1;
            layoutControl1.Size = new Size(700, 750);
            layoutControl1.TabIndex = 1;
            layoutControl1.Text = "layoutControl1";
            // 
            // txtAd
            // 
            txtAd.Location = new Point(149, 55);
            txtAd.Name = "txtAd";
            txtAd.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtAd.Properties.Appearance.Options.UseFont = true;
            txtAd.Properties.NullValuePrompt = "Öğrencinin adını giriniz";
            txtAd.Size = new Size(242, 28);
            txtAd.StyleController = layoutControl1;
            txtAd.TabIndex = 0;
            // 
            // txtSoyad
            // 
            txtSoyad.Location = new Point(149, 87);
            txtSoyad.Name = "txtSoyad";
            txtSoyad.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtSoyad.Properties.Appearance.Options.UseFont = true;
            txtSoyad.Properties.NullValuePrompt = "Öğrencinin soyadını giriniz";
            txtSoyad.Size = new Size(242, 28);
            txtSoyad.StyleController = layoutControl1;
            txtSoyad.TabIndex = 2;
            // 
            // txtTC
            // 
            txtTC.Location = new Point(149, 119);
            txtTC.Name = "txtTC";
            txtTC.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtTC.Properties.Appearance.Options.UseFont = true;
            txtTC.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            txtTC.Properties.MaskSettings.Set("mask", "00000000000");
            txtTC.Properties.NullValuePrompt = "11 haneli TC Kimlik No";
            txtTC.Size = new Size(242, 28);
            txtTC.StyleController = layoutControl1;
            txtTC.TabIndex = 3;
            // 
            // dtpDogumTarihi
            // 
            dtpDogumTarihi.EditValue = null;
            dtpDogumTarihi.Location = new Point(149, 151);
            dtpDogumTarihi.Name = "dtpDogumTarihi";
            dtpDogumTarihi.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            dtpDogumTarihi.Properties.Appearance.Options.UseFont = true;
            dtpDogumTarihi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dtpDogumTarihi.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dtpDogumTarihi.Properties.NullValuePrompt = "Doğum tarihini seçiniz";
            dtpDogumTarihi.Size = new Size(242, 28);
            dtpDogumTarihi.StyleController = layoutControl1;
            dtpDogumTarihi.TabIndex = 4;
            // 
            // txtTelefon
            // 
            txtTelefon.Location = new Point(149, 238);
            txtTelefon.Name = "txtTelefon";
            txtTelefon.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtTelefon.Properties.Appearance.Options.UseFont = true;
            txtTelefon.Properties.NullValuePrompt = "05XX XXX XX XX";
            txtTelefon.Size = new Size(242, 28);
            txtTelefon.StyleController = layoutControl1;
            txtTelefon.TabIndex = 5;
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(149, 270);
            txtEmail.Name = "txtEmail";
            txtEmail.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtEmail.Properties.Appearance.Options.UseFont = true;
            txtEmail.Properties.NullValuePrompt = "ornek@email.com";
            txtEmail.Size = new Size(242, 28);
            txtEmail.StyleController = layoutControl1;
            txtEmail.TabIndex = 6;
            // 
            // cmbUniversite
            // 
            cmbUniversite.Location = new Point(149, 357);
            cmbUniversite.Name = "cmbUniversite";
            cmbUniversite.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            cmbUniversite.Properties.Appearance.Options.UseFont = true;
            cmbUniversite.Properties.NullValuePrompt = "Üniversite seçiniz";
            cmbUniversite.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbUniversite.Size = new Size(242, 28);
            cmbUniversite.StyleController = layoutControl1;
            cmbUniversite.TabIndex = 7;
            // 
            // txtFakulte
            // 
            txtFakulte.Location = new Point(149, 389);
            txtFakulte.Name = "txtFakulte";
            txtFakulte.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtFakulte.Properties.Appearance.Options.UseFont = true;
            txtFakulte.Properties.NullValuePrompt = "Fakülte adını giriniz";
            txtFakulte.Size = new Size(242, 28);
            txtFakulte.StyleController = layoutControl1;
            txtFakulte.TabIndex = 8;
            // 
            // txtBolum
            // 
            txtBolum.Location = new Point(149, 421);
            txtBolum.Name = "txtBolum";
            txtBolum.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtBolum.Properties.Appearance.Options.UseFont = true;
            txtBolum.Properties.NullValuePrompt = "Bölüm adını giriniz";
            txtBolum.Size = new Size(242, 28);
            txtBolum.StyleController = layoutControl1;
            txtBolum.TabIndex = 9;
            // 
            // cmbSinif
            // 
            cmbSinif.Location = new Point(149, 453);
            cmbSinif.Name = "cmbSinif";
            cmbSinif.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            cmbSinif.Properties.Appearance.Options.UseFont = true;
            cmbSinif.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbSinif.Properties.NullValuePrompt = "Sınıf seçiniz";
            cmbSinif.Size = new Size(242, 28);
            cmbSinif.StyleController = layoutControl1;
            cmbSinif.TabIndex = 10;
            // 
            // txtNotOrtalamasi
            // 
            txtNotOrtalamasi.Location = new Point(149, 485);
            txtNotOrtalamasi.Name = "txtNotOrtalamasi";
            txtNotOrtalamasi.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtNotOrtalamasi.Properties.Appearance.Options.UseFont = true;
            txtNotOrtalamasi.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            txtNotOrtalamasi.Properties.MaskSettings.Set("mask", "0.00");
            txtNotOrtalamasi.Properties.NullValuePrompt = "0.00 - 4.00 arası";
            txtNotOrtalamasi.Size = new Size(242, 28);
            txtNotOrtalamasi.StyleController = layoutControl1;
            txtNotOrtalamasi.TabIndex = 11;
            // 
            // cmbKardesSayisi
            // 
            cmbKardesSayisi.Location = new Point(149, 572);
            cmbKardesSayisi.Name = "cmbKardesSayisi";
            cmbKardesSayisi.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            cmbKardesSayisi.Properties.Appearance.Options.UseFont = true;
            cmbKardesSayisi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbKardesSayisi.Properties.NullValuePrompt = "Kardeş sayısını seçiniz";
            cmbKardesSayisi.Size = new Size(242, 28);
            cmbKardesSayisi.StyleController = layoutControl1;
            cmbKardesSayisi.TabIndex = 12;
            // 
            // txtAileGeliri
            // 
            txtAileGeliri.Location = new Point(149, 604);
            txtAileGeliri.Name = "txtAileGeliri";
            txtAileGeliri.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtAileGeliri.Properties.Appearance.Options.UseFont = true;
            txtAileGeliri.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            txtAileGeliri.Properties.MaskSettings.Set("mask", "n");
            txtAileGeliri.Properties.NullValuePrompt = "Aylık gelir (TL)";
            txtAileGeliri.Size = new Size(242, 28);
            txtAileGeliri.StyleController = layoutControl1;
            txtAileGeliri.TabIndex = 13;
            // 
            // txtAnneMeslek
            // 
            txtAnneMeslek.Location = new Point(149, 636);
            txtAnneMeslek.Name = "txtAnneMeslek";
            txtAnneMeslek.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtAnneMeslek.Properties.Appearance.Options.UseFont = true;
            txtAnneMeslek.Properties.NullValuePrompt = "Anne mesleği";
            txtAnneMeslek.Size = new Size(242, 28);
            txtAnneMeslek.StyleController = layoutControl1;
            txtAnneMeslek.TabIndex = 14;
            // 
            // txtBabaMeslek
            // 
            txtBabaMeslek.Location = new Point(149, 668);
            txtBabaMeslek.Name = "txtBabaMeslek";
            txtBabaMeslek.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtBabaMeslek.Properties.Appearance.Options.UseFont = true;
            txtBabaMeslek.Properties.NullValuePrompt = "Baba mesleği";
            txtBabaMeslek.Size = new Size(242, 28);
            txtBabaMeslek.StyleController = layoutControl1;
            txtBabaMeslek.TabIndex = 15;
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
            btnKaydet.Location = new Point(27, 727);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(305, 28);
            btnKaydet.StyleController = layoutControl1;
            btnKaydet.TabIndex = 17;
            btnKaydet.Text = "Kaydet";
            btnKaydet.Click += btnKaydet_Click;
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
            btnIptal.Location = new Point(346, 727);
            btnIptal.Name = "btnIptal";
            btnIptal.Size = new Size(306, 28);
            btnIptal.StyleController = layoutControl1;
            btnIptal.TabIndex = 18;
            btnIptal.Text = "İptal";
            btnIptal.Click += btnIptal_Click;
            // 
            // btnFotoSec
            // 
            btnFotoSec.Appearance.BackColor = Color.FromArgb(33, 150, 243);
            btnFotoSec.Appearance.Font = new Font("Segoe UI", 10F);
            btnFotoSec.Appearance.ForeColor = Color.White;
            btnFotoSec.Appearance.Options.UseBackColor = true;
            btnFotoSec.Appearance.Options.UseFont = true;
            btnFotoSec.Appearance.Options.UseForeColor = true;
            btnFotoSec.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            btnFotoSec.ImageOptions.ImageUri.Uri = "Open;Size16x16";
            btnFotoSec.ImageOptions.SvgImageSize = new Size(16, 16);
            btnFotoSec.Location = new Point(420, 278);
            btnFotoSec.Name = "btnFotoSec";
            btnFotoSec.Size = new Size(200, 36);
            btnFotoSec.StyleController = layoutControl1;
            btnFotoSec.TabIndex = 16;
            btnFotoSec.Text = "Fotoğraf Seç";
            btnFotoSec.Click += btnFotoSec_Click;
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Items.AddRange(new BaseLayoutItem[] { groupSolTaraf, groupSagTaraf, itemBtnKaydet, itemBtnIptal });
            layoutControlGroup1.Name = "Root";
            layoutControlGroup1.Size = new Size(679, 782);
            layoutControlGroup1.TextVisible = false;
            // 
            // groupSolTaraf
            // 
            groupSolTaraf.GroupBordersVisible = false;
            groupSolTaraf.Items.AddRange(new BaseLayoutItem[] { groupKisiselBilgiler, groupIletisim, groupAkademik, groupAile });
            groupSolTaraf.Location = new Point(0, 0);
            groupSolTaraf.Name = "groupSolTaraf";
            groupSolTaraf.Size = new Size(395, 700);
            groupSolTaraf.TextVisible = false;
            // 
            // groupKisiselBilgiler
            // 
            groupKisiselBilgiler.AppearanceGroup.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupKisiselBilgiler.AppearanceGroup.ForeColor = Color.FromArgb(42, 42, 42);
            groupKisiselBilgiler.AppearanceGroup.Options.UseFont = true;
            groupKisiselBilgiler.AppearanceGroup.Options.UseForeColor = true;
            groupKisiselBilgiler.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            groupKisiselBilgiler.Items.AddRange(new BaseLayoutItem[] { itemAd, itemSoyad, itemTC, itemDogumTarihi });
            groupKisiselBilgiler.Location = new Point(0, 0);
            groupKisiselBilgiler.Name = "groupKisiselBilgiler";
            groupKisiselBilgiler.Size = new Size(395, 183);
            groupKisiselBilgiler.Text = "Kişisel Bilgiler";
            // 
            // itemAd
            // 
            itemAd.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemAd.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemAd.AppearanceItemCaption.Options.UseFont = true;
            itemAd.AppearanceItemCaption.Options.UseForeColor = true;
            itemAd.Control = txtAd;
            itemAd.Location = new Point(0, 0);
            itemAd.Name = "itemAd";
            itemAd.Size = new Size(367, 32);
            itemAd.Text = "Ad *";
            itemAd.TextSize = new Size(106, 21);
            // 
            // itemSoyad
            // 
            itemSoyad.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemSoyad.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemSoyad.AppearanceItemCaption.Options.UseFont = true;
            itemSoyad.AppearanceItemCaption.Options.UseForeColor = true;
            itemSoyad.Control = txtSoyad;
            itemSoyad.Location = new Point(0, 32);
            itemSoyad.Name = "itemSoyad";
            itemSoyad.Size = new Size(367, 32);
            itemSoyad.Text = "Soyad *";
            itemSoyad.TextSize = new Size(106, 21);
            // 
            // itemTC
            // 
            itemTC.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemTC.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemTC.AppearanceItemCaption.Options.UseFont = true;
            itemTC.AppearanceItemCaption.Options.UseForeColor = true;
            itemTC.Control = txtTC;
            itemTC.Location = new Point(0, 64);
            itemTC.Name = "itemTC";
            itemTC.Size = new Size(367, 32);
            itemTC.Text = "TC Kimlik No *";
            itemTC.TextSize = new Size(106, 21);
            // 
            // itemDogumTarihi
            // 
            itemDogumTarihi.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemDogumTarihi.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemDogumTarihi.AppearanceItemCaption.Options.UseFont = true;
            itemDogumTarihi.AppearanceItemCaption.Options.UseForeColor = true;
            itemDogumTarihi.Control = dtpDogumTarihi;
            itemDogumTarihi.Location = new Point(0, 96);
            itemDogumTarihi.Name = "itemDogumTarihi";
            itemDogumTarihi.Size = new Size(367, 32);
            itemDogumTarihi.Text = "Doğum Tarihi *";
            itemDogumTarihi.TextSize = new Size(106, 21);
            // 
            // groupIletisim
            // 
            groupIletisim.AppearanceGroup.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupIletisim.AppearanceGroup.ForeColor = Color.FromArgb(42, 42, 42);
            groupIletisim.AppearanceGroup.Options.UseFont = true;
            groupIletisim.AppearanceGroup.Options.UseForeColor = true;
            groupIletisim.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            groupIletisim.Items.AddRange(new BaseLayoutItem[] { itemTelefon, itemEmail });
            groupIletisim.Location = new Point(0, 183);
            groupIletisim.Name = "groupIletisim";
            groupIletisim.Size = new Size(395, 119);
            groupIletisim.Text = "İletişim Bilgileri";
            // 
            // itemTelefon
            // 
            itemTelefon.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemTelefon.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemTelefon.AppearanceItemCaption.Options.UseFont = true;
            itemTelefon.AppearanceItemCaption.Options.UseForeColor = true;
            itemTelefon.Control = txtTelefon;
            itemTelefon.Location = new Point(0, 0);
            itemTelefon.Name = "itemTelefon";
            itemTelefon.Size = new Size(367, 32);
            itemTelefon.Text = "Telefon";
            itemTelefon.TextSize = new Size(106, 21);
            // 
            // itemEmail
            // 
            itemEmail.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemEmail.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemEmail.AppearanceItemCaption.Options.UseFont = true;
            itemEmail.AppearanceItemCaption.Options.UseForeColor = true;
            itemEmail.Control = txtEmail;
            itemEmail.Location = new Point(0, 32);
            itemEmail.Name = "itemEmail";
            itemEmail.Size = new Size(367, 32);
            itemEmail.Text = "E-posta";
            itemEmail.TextSize = new Size(106, 21);
            // 
            // groupAkademik
            // 
            groupAkademik.AppearanceGroup.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupAkademik.AppearanceGroup.ForeColor = Color.FromArgb(42, 42, 42);
            groupAkademik.AppearanceGroup.Options.UseFont = true;
            groupAkademik.AppearanceGroup.Options.UseForeColor = true;
            groupAkademik.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            groupAkademik.Items.AddRange(new BaseLayoutItem[] { itemUniversite, itemFakulte, itemBolum, itemSinif, itemNotOrtalamasi });
            groupAkademik.Location = new Point(0, 302);
            groupAkademik.Name = "groupAkademik";
            groupAkademik.Size = new Size(395, 215);
            groupAkademik.Text = "Akademik Bilgiler";
            // 
            // itemUniversite
            // 
            itemUniversite.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemUniversite.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemUniversite.AppearanceItemCaption.Options.UseFont = true;
            itemUniversite.AppearanceItemCaption.Options.UseForeColor = true;
            itemUniversite.Control = cmbUniversite;
            itemUniversite.Location = new Point(0, 0);
            itemUniversite.Name = "itemUniversite";
            itemUniversite.Size = new Size(367, 32);
            itemUniversite.Text = "Üniversite";
            itemUniversite.TextSize = new Size(106, 21);
            // 
            // itemFakulte
            // 
            itemFakulte.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemFakulte.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemFakulte.AppearanceItemCaption.Options.UseFont = true;
            itemFakulte.AppearanceItemCaption.Options.UseForeColor = true;
            itemFakulte.Control = txtFakulte;
            itemFakulte.Location = new Point(0, 32);
            itemFakulte.Name = "itemFakulte";
            itemFakulte.Size = new Size(367, 32);
            itemFakulte.Text = "Fakülte";
            itemFakulte.TextSize = new Size(106, 21);
            // 
            // itemBolum
            // 
            itemBolum.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemBolum.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemBolum.AppearanceItemCaption.Options.UseFont = true;
            itemBolum.AppearanceItemCaption.Options.UseForeColor = true;
            itemBolum.Control = txtBolum;
            itemBolum.Location = new Point(0, 64);
            itemBolum.Name = "itemBolum";
            itemBolum.Size = new Size(367, 32);
            itemBolum.Text = "Bölüm";
            itemBolum.TextSize = new Size(106, 21);
            // 
            // itemSinif
            // 
            itemSinif.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemSinif.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemSinif.AppearanceItemCaption.Options.UseFont = true;
            itemSinif.AppearanceItemCaption.Options.UseForeColor = true;
            itemSinif.Control = cmbSinif;
            itemSinif.Location = new Point(0, 96);
            itemSinif.Name = "itemSinif";
            itemSinif.Size = new Size(367, 32);
            itemSinif.Text = "Sınıf";
            itemSinif.TextSize = new Size(106, 21);
            // 
            // itemNotOrtalamasi
            // 
            itemNotOrtalamasi.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemNotOrtalamasi.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemNotOrtalamasi.AppearanceItemCaption.Options.UseFont = true;
            itemNotOrtalamasi.AppearanceItemCaption.Options.UseForeColor = true;
            itemNotOrtalamasi.Control = txtNotOrtalamasi;
            itemNotOrtalamasi.Location = new Point(0, 128);
            itemNotOrtalamasi.Name = "itemNotOrtalamasi";
            itemNotOrtalamasi.Size = new Size(367, 32);
            itemNotOrtalamasi.Text = "GNO";
            itemNotOrtalamasi.TextSize = new Size(106, 21);
            // 
            // groupAile
            // 
            groupAile.AppearanceGroup.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupAile.AppearanceGroup.ForeColor = Color.FromArgb(42, 42, 42);
            groupAile.AppearanceGroup.Options.UseFont = true;
            groupAile.AppearanceGroup.Options.UseForeColor = true;
            groupAile.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            groupAile.Items.AddRange(new BaseLayoutItem[] { itemKardesSayisi, itemAileGeliri, itemAnneMeslek, itemBabaMeslek });
            groupAile.Location = new Point(0, 517);
            groupAile.Name = "groupAile";
            groupAile.Size = new Size(395, 183);
            groupAile.Text = "Aile Bilgileri";
            // 
            // itemKardesSayisi
            // 
            itemKardesSayisi.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemKardesSayisi.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemKardesSayisi.AppearanceItemCaption.Options.UseFont = true;
            itemKardesSayisi.AppearanceItemCaption.Options.UseForeColor = true;
            itemKardesSayisi.Control = cmbKardesSayisi;
            itemKardesSayisi.Location = new Point(0, 0);
            itemKardesSayisi.Name = "itemKardesSayisi";
            itemKardesSayisi.Size = new Size(367, 32);
            itemKardesSayisi.Text = "Kardeş Sayısı";
            itemKardesSayisi.TextSize = new Size(106, 21);
            // 
            // itemAileGeliri
            // 
            itemAileGeliri.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemAileGeliri.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemAileGeliri.AppearanceItemCaption.Options.UseFont = true;
            itemAileGeliri.AppearanceItemCaption.Options.UseForeColor = true;
            itemAileGeliri.Control = txtAileGeliri;
            itemAileGeliri.Location = new Point(0, 32);
            itemAileGeliri.Name = "itemAileGeliri";
            itemAileGeliri.Size = new Size(367, 32);
            itemAileGeliri.Text = "Aile Geliri (TL)";
            itemAileGeliri.TextSize = new Size(106, 21);
            // 
            // itemAnneMeslek
            // 
            itemAnneMeslek.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemAnneMeslek.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemAnneMeslek.AppearanceItemCaption.Options.UseFont = true;
            itemAnneMeslek.AppearanceItemCaption.Options.UseForeColor = true;
            itemAnneMeslek.Control = txtAnneMeslek;
            itemAnneMeslek.Location = new Point(0, 64);
            itemAnneMeslek.Name = "itemAnneMeslek";
            itemAnneMeslek.Size = new Size(367, 32);
            itemAnneMeslek.Text = "Anne Mesleği";
            itemAnneMeslek.TextSize = new Size(106, 21);
            // 
            // itemBabaMeslek
            // 
            itemBabaMeslek.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemBabaMeslek.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemBabaMeslek.AppearanceItemCaption.Options.UseFont = true;
            itemBabaMeslek.AppearanceItemCaption.Options.UseForeColor = true;
            itemBabaMeslek.Control = txtBabaMeslek;
            itemBabaMeslek.Location = new Point(0, 96);
            itemBabaMeslek.Name = "itemBabaMeslek";
            itemBabaMeslek.Size = new Size(367, 32);
            itemBabaMeslek.Text = "Baba Mesleği";
            itemBabaMeslek.TextSize = new Size(106, 21);
            // 
            // groupSagTaraf
            // 
            groupSagTaraf.GroupBordersVisible = false;
            groupSagTaraf.Items.AddRange(new BaseLayoutItem[] { groupProfilFoto });
            groupSagTaraf.Location = new Point(395, 0);
            groupSagTaraf.Name = "groupSagTaraf";
            groupSagTaraf.Size = new Size(260, 700);
            groupSagTaraf.TextVisible = false;
            // 
            // groupProfilFoto
            // 
            groupProfilFoto.AppearanceGroup.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupProfilFoto.AppearanceGroup.ForeColor = Color.FromArgb(42, 42, 42);
            groupProfilFoto.AppearanceGroup.Options.UseFont = true;
            groupProfilFoto.AppearanceGroup.Options.UseForeColor = true;
            groupProfilFoto.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            groupProfilFoto.Items.AddRange(new BaseLayoutItem[] { itemProfilFoto, itemFotoSec });
            groupProfilFoto.Location = new Point(0, 0);
            groupProfilFoto.Name = "groupProfilFoto";
            groupProfilFoto.Size = new Size(260, 700);
            groupProfilFoto.Text = "Profil Fotoğrafı";
            // 
            // itemProfilFoto
            // 
            itemProfilFoto.Control = pictureEdit1;
            itemProfilFoto.Location = new Point(0, 0);
            itemProfilFoto.Name = "itemProfilFoto";
            itemProfilFoto.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 5);
            itemProfilFoto.Size = new Size(232, 220);
            itemProfilFoto.TextVisible = false;
            itemProfilFoto.MinSize = new Size(200, 200);
            itemProfilFoto.MaxSize = new Size(250, 250);
            itemProfilFoto.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            // 
            // itemFotoSec
            // 
            itemFotoSec.Control = btnFotoSec;
            itemFotoSec.Location = new Point(0, 220);
            itemFotoSec.Name = "itemFotoSec";
            itemFotoSec.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 5, 10);
            itemFotoSec.Size = new Size(232, 50);
            itemFotoSec.TextVisible = false;
            // 
            // itemBtnKaydet
            // 
            itemBtnKaydet.Control = btnKaydet;
            itemBtnKaydet.Location = new Point(0, 700);
            itemBtnKaydet.Name = "itemBtnKaydet";
            itemBtnKaydet.Padding = new DevExpress.XtraLayout.Utils.Padding(15, 7, 15, 15);
            itemBtnKaydet.Size = new Size(327, 58);
            itemBtnKaydet.TextVisible = false;
            // 
            // itemBtnIptal
            // 
            itemBtnIptal.Control = btnIptal;
            itemBtnIptal.Location = new Point(327, 700);
            itemBtnIptal.Name = "itemBtnIptal";
            itemBtnIptal.Padding = new DevExpress.XtraLayout.Utils.Padding(7, 15, 15, 15);
            itemBtnIptal.Size = new Size(328, 58);
            itemBtnIptal.TextVisible = false;
            // 
            // panelHeader
            // 
            panelHeader.Appearance.BackColor = Color.FromArgb(42, 42, 42);
            panelHeader.Appearance.Options.UseBackColor = true;
            panelHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelHeader.Controls.Add(labelControl1);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(700, 50);
            panelHeader.TabIndex = 0;
            // 
            // labelControl1
            // 
            labelControl1.Appearance.BackColor = Color.Transparent;
            labelControl1.Appearance.Font = new Font("Segoe UI Semibold", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 162);
            labelControl1.Appearance.ForeColor = Color.White;
            labelControl1.Appearance.Options.UseBackColor = true;
            labelControl1.Appearance.Options.UseFont = true;
            labelControl1.Appearance.Options.UseForeColor = true;
            labelControl1.Location = new Point(195, 10);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(196, 31);
            labelControl1.TabIndex = 0;
            labelControl1.Text = "Yeni Öğrenci Kaydı";
            // 
            // pictureEdit1
            // 
            pictureEdit1.Location = new Point(420, 68);
            pictureEdit1.Name = "pictureEdit1";
            pictureEdit1.Properties.Appearance.BackColor = Color.FromArgb(240, 242, 245);
            pictureEdit1.Properties.Appearance.BorderColor = Color.FromArgb(180, 180, 180);
            pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            pictureEdit1.Properties.Appearance.Options.UseBorderColor = true;
            pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            pictureEdit1.Properties.ShowMenu = false;
            pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            pictureEdit1.Properties.NullText = "📷 Fotoğraf Yüklemek İçin\n\"Fotoğraf Seç\" Butonuna Tıklayın";
            pictureEdit1.Size = new Size(200, 200);
            pictureEdit1.StyleController = layoutControl1;
            pictureEdit1.TabIndex = 1;
            pictureEdit1.TabStop = false;
            // 
            // OgrenciEkleForm
            // 
            Appearance.BackColor = Color.White;
            Appearance.Options.UseBackColor = true;
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(700, 800);
            Controls.Add(layoutControl1);
            Controls.Add(panelHeader);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;
            MinimumSize = new Size(650, 700);
            Name = "OgrenciEkleForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Öğrenci Ekle";
            WindowState = FormWindowState.Normal;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupSolTaraf).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupKisiselBilgiler).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemAd).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemSoyad).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemTC).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemDogumTarihi).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupIletisim).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemTelefon).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemEmail).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupAkademik).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemUniversite).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemFakulte).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemBolum).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemSinif).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemNotOrtalamasi).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupAile).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemKardesSayisi).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemAileGeliri).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemAnneMeslek).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemBabaMeslek).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupSagTaraf).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupProfilFoto).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemProfilFoto).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemFotoSec).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemBtnKaydet).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemBtnIptal).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelHeader).EndInit();
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureEdit1.Properties).EndInit();
            ResumeLayout(false);
        }
        private LabelControl labelControl1;
        protected PictureEdit pictureEdit1;
    }
}
