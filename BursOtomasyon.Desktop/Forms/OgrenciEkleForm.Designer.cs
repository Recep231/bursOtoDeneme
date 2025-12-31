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
        protected PictureEdit pictureEdit1 = null!;
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
        private LabelControl lblHeader = null!;
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
            panelHeader = new PanelControl();
            lblHeader = new LabelControl();
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
            pictureEdit1 = new PictureEdit();
            btnFotoSec = new SimpleButton();
            layoutControlGroup1 = new LayoutControlGroup();
            groupSolTaraf = new LayoutControlGroup();
            groupSagTaraf = new LayoutControlGroup();
            groupKisiselBilgiler = new LayoutControlGroup();
            groupIletisim = new LayoutControlGroup();
            groupAkademik = new LayoutControlGroup();
            groupAile = new LayoutControlGroup();
            groupProfilFoto = new LayoutControlGroup();
            
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)panelHeader).BeginInit();
            panelHeader.SuspendLayout();
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
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupSolTaraf).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupSagTaraf).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupKisiselBilgiler).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupIletisim).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupAkademik).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupAile).BeginInit();
            ((System.ComponentModel.ISupportInitialize)groupProfilFoto).BeginInit();
            SuspendLayout();
            
            // 
            // panelHeader
            // 
            panelHeader.Controls.Add(lblHeader);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1000, 65);
            panelHeader.TabIndex = 0;
            panelHeader.Appearance.BackColor = Color.FromArgb(42, 42, 42);
            panelHeader.Appearance.Options.UseBackColor = true;
            panelHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            
            // 
            // lblHeader
            // 
            lblHeader.Appearance.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            lblHeader.Appearance.ForeColor = Color.White;
            lblHeader.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            lblHeader.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            lblHeader.AutoSizeMode = LabelAutoSizeMode.None;
            lblHeader.Dock = DockStyle.Fill;
            lblHeader.Location = new Point(2, 2);
            lblHeader.Name = "lblHeader";
            lblHeader.Size = new Size(996, 61);
            lblHeader.Text = "Yeni Öğrenci Kaydı";
            lblHeader.ImageOptions.ImageUri.Uri = "Add;Size32x32";
            lblHeader.ImageOptions.SvgImageSize = new Size(24, 24);
            
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
            layoutControl1.Controls.Add(pictureEdit1);
            layoutControl1.Controls.Add(btnFotoSec);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new Point(0, 65);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(1070, 200, 650, 400);
            layoutControl1.Root = layoutControlGroup1;
            layoutControl1.Size = new Size(1000, 735);
            layoutControl1.TabIndex = 1;
            layoutControl1.Text = "layoutControl1";
            
            // 
            // txtAd
            // 
            txtAd.Location = new Point(0, 0);
            txtAd.Name = "txtAd";
            txtAd.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtAd.Properties.Appearance.Options.UseFont = true;
            txtAd.Properties.NullValuePrompt = "Öğrencinin adını giriniz";
            txtAd.Properties.NullValuePromptShowForEmptyValue = true;
            txtAd.Size = new Size(0, 26);
            txtAd.StyleController = layoutControl1;
            txtAd.TabIndex = 4;
            
            // 
            // txtSoyad
            // 
            txtSoyad.Location = new Point(0, 0);
            txtSoyad.Name = "txtSoyad";
            txtSoyad.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtSoyad.Properties.Appearance.Options.UseFont = true;
            txtSoyad.Properties.NullValuePrompt = "Öğrencinin soyadını giriniz";
            txtSoyad.Properties.NullValuePromptShowForEmptyValue = true;
            txtSoyad.Size = new Size(0, 26);
            txtSoyad.StyleController = layoutControl1;
            txtSoyad.TabIndex = 5;
            
            // 
            // txtTC
            // 
            txtTC.Location = new Point(0, 0);
            txtTC.Name = "txtTC";
            txtTC.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtTC.Properties.Appearance.Options.UseFont = true;
            txtTC.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            txtTC.Properties.MaskSettings.Set("mask", "00000000000");
            txtTC.Properties.NullValuePrompt = "11 haneli TC Kimlik No";
            txtTC.Properties.NullValuePromptShowForEmptyValue = true;
            txtTC.Size = new Size(0, 26);
            txtTC.StyleController = layoutControl1;
            txtTC.TabIndex = 6;
            
            // 
            // dtpDogumTarihi
            // 
            dtpDogumTarihi.EditValue = null;
            dtpDogumTarihi.Location = new Point(0, 0);
            dtpDogumTarihi.Name = "dtpDogumTarihi";
            dtpDogumTarihi.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            dtpDogumTarihi.Properties.Appearance.Options.UseFont = true;
            dtpDogumTarihi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dtpDogumTarihi.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            dtpDogumTarihi.Properties.NullValuePrompt = "Doğum tarihini seçiniz";
            dtpDogumTarihi.Properties.NullValuePromptShowForEmptyValue = true;
            dtpDogumTarihi.Size = new Size(0, 26);
            dtpDogumTarihi.StyleController = layoutControl1;
            dtpDogumTarihi.TabIndex = 7;
            
            // 
            // txtTelefon
            // 
            txtTelefon.Location = new Point(0, 0);
            txtTelefon.Name = "txtTelefon";
            txtTelefon.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtTelefon.Properties.Appearance.Options.UseFont = true;
            txtTelefon.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.RegularMaskManager));
            txtTelefon.Properties.MaskSettings.Set("mask", "0 (\\d\\d\\d) \\d\\d\\d \\d\\d \\d\\d");
            txtTelefon.Properties.MaskSettings.Set("SaveLiteral", false);
            txtTelefon.Properties.MaskSettings.Set("AutoComplete", DevExpress.Utils.DefaultBoolean.False);
            txtTelefon.Properties.MaskSettings.Set("PlaceHolder", ' ');
            txtTelefon.Properties.MaxLength = 15;
            txtTelefon.Properties.NullValuePrompt = "0 (5XX) XXX XX XX";
            txtTelefon.Properties.NullValuePromptShowForEmptyValue = true;
            txtTelefon.Size = new Size(0, 26);
            txtTelefon.StyleController = layoutControl1;
            txtTelefon.TabIndex = 8;
            
            // 
            // txtEmail
            // 
            txtEmail.Location = new Point(0, 0);
            txtEmail.Name = "txtEmail";
            txtEmail.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtEmail.Properties.Appearance.Options.UseFont = true;
            txtEmail.Properties.NullValuePrompt = "ornek@email.com";
            txtEmail.Properties.NullValuePromptShowForEmptyValue = true;
            txtEmail.Size = new Size(0, 26);
            txtEmail.StyleController = layoutControl1;
            txtEmail.TabIndex = 9;
            
            // 
            // cmbUniversite
            // 
            cmbUniversite.Location = new Point(0, 0);
            cmbUniversite.Name = "cmbUniversite";
            cmbUniversite.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            cmbUniversite.Properties.Appearance.Options.UseFont = true;
            cmbUniversite.Properties.NullValuePrompt = "Üniversite seçiniz";
            cmbUniversite.Properties.NullValuePromptShowForEmptyValue = true;
            cmbUniversite.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            cmbUniversite.Size = new Size(0, 26);
            cmbUniversite.StyleController = layoutControl1;
            cmbUniversite.TabIndex = 10;
            
            // 
            // txtFakulte
            // 
            txtFakulte.Location = new Point(0, 0);
            txtFakulte.Name = "txtFakulte";
            txtFakulte.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtFakulte.Properties.Appearance.Options.UseFont = true;
            txtFakulte.Properties.NullValuePrompt = "Fakülte adını giriniz";
            txtFakulte.Properties.NullValuePromptShowForEmptyValue = true;
            txtFakulte.Size = new Size(0, 26);
            txtFakulte.StyleController = layoutControl1;
            txtFakulte.TabIndex = 11;
            
            // 
            // txtBolum
            // 
            txtBolum.Location = new Point(0, 0);
            txtBolum.Name = "txtBolum";
            txtBolum.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtBolum.Properties.Appearance.Options.UseFont = true;
            txtBolum.Properties.NullValuePrompt = "Bölüm adını giriniz";
            txtBolum.Properties.NullValuePromptShowForEmptyValue = true;
            txtBolum.Size = new Size(0, 26);
            txtBolum.StyleController = layoutControl1;
            txtBolum.TabIndex = 12;
            
            // 
            // cmbSinif
            // 
            cmbSinif.Location = new Point(0, 0);
            cmbSinif.Name = "cmbSinif";
            cmbSinif.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            cmbSinif.Properties.Appearance.Options.UseFont = true;
            cmbSinif.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbSinif.Properties.NullValuePrompt = "Sınıf seçiniz";
            cmbSinif.Properties.NullValuePromptShowForEmptyValue = true;
            cmbSinif.Size = new Size(0, 26);
            cmbSinif.StyleController = layoutControl1;
            cmbSinif.TabIndex = 13;
            
            // 
            // txtNotOrtalamasi
            // 
            txtNotOrtalamasi.Location = new Point(0, 0);
            txtNotOrtalamasi.Name = "txtNotOrtalamasi";
            txtNotOrtalamasi.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtNotOrtalamasi.Properties.Appearance.Options.UseFont = true;
            txtNotOrtalamasi.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            txtNotOrtalamasi.Properties.MaskSettings.Set("mask", "0.00");
            txtNotOrtalamasi.Properties.NullValuePrompt = "0.00 - 4.00 arası";
            txtNotOrtalamasi.Properties.NullValuePromptShowForEmptyValue = true;
            txtNotOrtalamasi.Size = new Size(0, 26);
            txtNotOrtalamasi.StyleController = layoutControl1;
            txtNotOrtalamasi.TabIndex = 14;
            
            // 
            // cmbKardesSayisi
            // 
            cmbKardesSayisi.Location = new Point(0, 0);
            cmbKardesSayisi.Name = "cmbKardesSayisi";
            cmbKardesSayisi.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            cmbKardesSayisi.Properties.Appearance.Options.UseFont = true;
            cmbKardesSayisi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbKardesSayisi.Properties.NullValuePrompt = "Kardeş sayısını seçiniz";
            cmbKardesSayisi.Properties.NullValuePromptShowForEmptyValue = true;
            cmbKardesSayisi.Size = new Size(0, 26);
            cmbKardesSayisi.StyleController = layoutControl1;
            cmbKardesSayisi.TabIndex = 15;
            
            // 
            // txtAileGeliri
            // 
            txtAileGeliri.Location = new Point(0, 0);
            txtAileGeliri.Name = "txtAileGeliri";
            txtAileGeliri.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtAileGeliri.Properties.Appearance.Options.UseFont = true;
            txtAileGeliri.Properties.MaskSettings.Set("MaskManagerType", typeof(DevExpress.Data.Mask.NumericMaskManager));
            txtAileGeliri.Properties.MaskSettings.Set("mask", "n");
            txtAileGeliri.Properties.NullValuePrompt = "Aylık gelir (TL)";
            txtAileGeliri.Properties.NullValuePromptShowForEmptyValue = true;
            txtAileGeliri.Size = new Size(0, 26);
            txtAileGeliri.StyleController = layoutControl1;
            txtAileGeliri.TabIndex = 16;
            
            // 
            // txtAnneMeslek
            // 
            txtAnneMeslek.Location = new Point(0, 0);
            txtAnneMeslek.Name = "txtAnneMeslek";
            txtAnneMeslek.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtAnneMeslek.Properties.Appearance.Options.UseFont = true;
            txtAnneMeslek.Properties.NullValuePrompt = "Anne mesleği";
            txtAnneMeslek.Properties.NullValuePromptShowForEmptyValue = true;
            txtAnneMeslek.Size = new Size(0, 26);
            txtAnneMeslek.StyleController = layoutControl1;
            txtAnneMeslek.TabIndex = 17;
            
            // 
            // txtBabaMeslek
            // 
            txtBabaMeslek.Location = new Point(0, 0);
            txtBabaMeslek.Name = "txtBabaMeslek";
            txtBabaMeslek.Properties.Appearance.Font = new Font("Segoe UI", 9.75F);
            txtBabaMeslek.Properties.Appearance.Options.UseFont = true;
            txtBabaMeslek.Properties.NullValuePrompt = "Baba mesleği";
            txtBabaMeslek.Properties.NullValuePromptShowForEmptyValue = true;
            txtBabaMeslek.Size = new Size(0, 26);
            txtBabaMeslek.StyleController = layoutControl1;
            txtBabaMeslek.TabIndex = 18;
            
            // 
            // pictureEdit1
            // 
            pictureEdit1.Location = new Point(0, 0);
            pictureEdit1.Name = "pictureEdit1";
            pictureEdit1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            pictureEdit1.Properties.ShowMenu = false;
            pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            pictureEdit1.Properties.Appearance.BackColor = Color.FromArgb(248, 249, 250);
            pictureEdit1.Properties.Appearance.Options.UseBackColor = true;
            pictureEdit1.Properties.Appearance.BorderColor = Color.FromArgb(200, 200, 200);
            pictureEdit1.Properties.Appearance.Options.UseBorderColor = true;
            pictureEdit1.Properties.AllowZoomOnMouseWheel = DevExpress.Utils.DefaultBoolean.True;
            pictureEdit1.Properties.ShowZoomSubMenu = DevExpress.Utils.DefaultBoolean.False;
            pictureEdit1.Properties.ShowScrollBars = false;
            pictureEdit1.Properties.NullText = "Profil Fotoğrafı";
            pictureEdit1.Size = new Size(0, 0);
            pictureEdit1.StyleController = layoutControl1;
            pictureEdit1.TabIndex = 21;
            
            // 
            // btnFotoSec
            // 
            btnFotoSec.Location = new Point(0, 0);
            btnFotoSec.Name = "btnFotoSec";
            btnFotoSec.Size = new Size(0, 0);
            btnFotoSec.StyleController = layoutControl1;
            btnFotoSec.TabIndex = 22;
            btnFotoSec.Text = "Fotoğraf Seç";
            btnFotoSec.ImageOptions.ImageUri.Uri = "Open;Size16x16";
            btnFotoSec.ImageOptions.SvgImageSize = new Size(16, 16);
            btnFotoSec.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnFotoSec.Appearance.Options.UseFont = true;
            btnFotoSec.Appearance.BackColor = Color.FromArgb(33, 150, 243);
            btnFotoSec.Appearance.ForeColor = Color.White;
            btnFotoSec.Appearance.Options.UseBackColor = true;
            btnFotoSec.Appearance.Options.UseForeColor = true;
            btnFotoSec.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            btnFotoSec.Click += btnFotoSec_Click;
            
            // 
            // btnKaydet
            // 
            btnKaydet.Location = new Point(0, 0);
            btnKaydet.Name = "btnKaydet";
            btnKaydet.Size = new Size(0, 0);
            btnKaydet.StyleController = layoutControl1;
            btnKaydet.TabIndex = 19;
            btnKaydet.Text = "Kaydet";
            btnKaydet.ImageOptions.ImageUri.Uri = "Save;Size16x16";
            btnKaydet.ImageOptions.SvgImageSize = new Size(16, 16);
            btnKaydet.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnKaydet.Appearance.Options.UseFont = true;
            btnKaydet.Appearance.BackColor = Color.FromArgb(76, 175, 80);
            btnKaydet.Appearance.ForeColor = Color.White;
            btnKaydet.Appearance.Options.UseBackColor = true;
            btnKaydet.Appearance.Options.UseForeColor = true;
            btnKaydet.Click += btnKaydet_Click;
            
            // 
            // btnIptal
            // 
            btnIptal.Location = new Point(0, 0);
            btnIptal.Name = "btnIptal";
            btnIptal.Size = new Size(0, 0);
            btnIptal.StyleController = layoutControl1;
            btnIptal.TabIndex = 20;
            btnIptal.Text = "İptal";
            btnIptal.ImageOptions.ImageUri.Uri = "Cancel;Size16x16";
            btnIptal.ImageOptions.SvgImageSize = new Size(16, 16);
            btnIptal.Appearance.Font = new Font("Segoe UI", 10F, FontStyle.Regular);
            btnIptal.Appearance.Options.UseFont = true;
            btnIptal.Appearance.BackColor = Color.FromArgb(158, 158, 158);
            btnIptal.Appearance.ForeColor = Color.White;
            btnIptal.Appearance.Options.UseBackColor = true;
            btnIptal.Appearance.Options.UseForeColor = true;
            btnIptal.Click += btnIptal_Click;
            
            // 
            // layoutControlGroup1
            // 
            layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            layoutControlGroup1.GroupBordersVisible = false;
            layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            layoutControlGroup1.TextVisible = false;
            
            // Sol ve Sağ grupları oluştur
            groupSolTaraf = new LayoutControlGroup();
            groupSolTaraf.TextVisible = false;
            groupSolTaraf.GroupBordersVisible = false;
            groupSolTaraf.Location = new Point(0, 0);
            groupSolTaraf.Name = "groupSolTaraf";
            groupSolTaraf.Size = new Size(0, 0);
            
            groupSagTaraf = new LayoutControlGroup();
            groupSagTaraf.TextVisible = false;
            groupSagTaraf.GroupBordersVisible = false;
            groupSagTaraf.Location = new Point(0, 0);
            groupSagTaraf.Name = "groupSagTaraf";
            groupSagTaraf.Size = new Size(0, 0);
            
            // LayoutControlItem'ları oluştur
            itemAd = new LayoutControlItem();
            itemAd.Control = txtAd;
            itemAd.Location = new Point(0, 0);
            itemAd.Name = "itemAd";
            itemAd.Size = new Size(540, 30);
            itemAd.Text = "Ad *";
            itemAd.TextSize = new Size(120, 16);
            itemAd.TextToControlDistance = 10;
            itemAd.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemAd.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemAd.AppearanceItemCaption.Options.UseFont = true;
            itemAd.AppearanceItemCaption.Options.UseForeColor = true;
            
            itemSoyad = new LayoutControlItem();
            itemSoyad.Control = txtSoyad;
            itemSoyad.Location = new Point(0, 30);
            itemSoyad.Name = "itemSoyad";
            itemSoyad.Size = new Size(540, 30);
            itemSoyad.Text = "Soyad *";
            itemSoyad.TextSize = new Size(120, 16);
            itemSoyad.TextToControlDistance = 10;
            itemSoyad.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemSoyad.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemSoyad.AppearanceItemCaption.Options.UseFont = true;
            itemSoyad.AppearanceItemCaption.Options.UseForeColor = true;
            
            itemTC = new LayoutControlItem();
            itemTC.Control = txtTC;
            itemTC.Location = new Point(0, 60);
            itemTC.Name = "itemTC";
            itemTC.Size = new Size(540, 30);
            itemTC.Text = "TC Kimlik No *";
            itemTC.TextSize = new Size(120, 16);
            itemTC.TextToControlDistance = 10;
            itemTC.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemTC.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemTC.AppearanceItemCaption.Options.UseFont = true;
            itemTC.AppearanceItemCaption.Options.UseForeColor = true;
            
            itemDogumTarihi = new LayoutControlItem();
            itemDogumTarihi.Control = dtpDogumTarihi;
            itemDogumTarihi.Location = new Point(0, 90);
            itemDogumTarihi.Name = "itemDogumTarihi";
            itemDogumTarihi.Size = new Size(540, 30);
            itemDogumTarihi.Text = "Doğum Tarihi *";
            itemDogumTarihi.TextSize = new Size(120, 16);
            itemDogumTarihi.TextToControlDistance = 10;
            itemDogumTarihi.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemDogumTarihi.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemDogumTarihi.AppearanceItemCaption.Options.UseFont = true;
            itemDogumTarihi.AppearanceItemCaption.Options.UseForeColor = true;
            
            itemTelefon = new LayoutControlItem();
            itemTelefon.Control = txtTelefon;
            itemTelefon.Location = new Point(0, 0);
            itemTelefon.Name = "itemTelefon";
            itemTelefon.Size = new Size(540, 30);
            itemTelefon.Text = "Telefon";
            itemTelefon.TextSize = new Size(120, 16);
            itemTelefon.TextToControlDistance = 10;
            itemTelefon.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemTelefon.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemTelefon.AppearanceItemCaption.Options.UseFont = true;
            itemTelefon.AppearanceItemCaption.Options.UseForeColor = true;
            
            itemEmail = new LayoutControlItem();
            itemEmail.Control = txtEmail;
            itemEmail.Location = new Point(0, 30);
            itemEmail.Name = "itemEmail";
            itemEmail.Size = new Size(540, 30);
            itemEmail.Text = "E-posta";
            itemEmail.TextSize = new Size(120, 16);
            itemEmail.TextToControlDistance = 10;
            itemEmail.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemEmail.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemEmail.AppearanceItemCaption.Options.UseFont = true;
            itemEmail.AppearanceItemCaption.Options.UseForeColor = true;
            
            itemUniversite = new LayoutControlItem();
            itemUniversite.Control = cmbUniversite;
            itemUniversite.Location = new Point(0, 0);
            itemUniversite.Name = "itemUniversite";
            itemUniversite.Size = new Size(540, 30);
            itemUniversite.Text = "Üniversite";
            itemUniversite.TextSize = new Size(120, 16);
            itemUniversite.TextToControlDistance = 10;
            itemUniversite.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemUniversite.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemUniversite.AppearanceItemCaption.Options.UseFont = true;
            itemUniversite.AppearanceItemCaption.Options.UseForeColor = true;
            
            itemFakulte = new LayoutControlItem();
            itemFakulte.Control = txtFakulte;
            itemFakulte.Location = new Point(0, 30);
            itemFakulte.Name = "itemFakulte";
            itemFakulte.Size = new Size(540, 30);
            itemFakulte.Text = "Fakülte";
            itemFakulte.TextSize = new Size(120, 16);
            itemFakulte.TextToControlDistance = 10;
            itemFakulte.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemFakulte.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemFakulte.AppearanceItemCaption.Options.UseFont = true;
            itemFakulte.AppearanceItemCaption.Options.UseForeColor = true;
            
            itemBolum = new LayoutControlItem();
            itemBolum.Control = txtBolum;
            itemBolum.Location = new Point(0, 60);
            itemBolum.Name = "itemBolum";
            itemBolum.Size = new Size(540, 30);
            itemBolum.Text = "Bölüm";
            itemBolum.TextSize = new Size(120, 16);
            itemBolum.TextToControlDistance = 10;
            itemBolum.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemBolum.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemBolum.AppearanceItemCaption.Options.UseFont = true;
            itemBolum.AppearanceItemCaption.Options.UseForeColor = true;
            
            itemSinif = new LayoutControlItem();
            itemSinif.Control = cmbSinif;
            itemSinif.Location = new Point(0, 90);
            itemSinif.Name = "itemSinif";
            itemSinif.Size = new Size(540, 30);
            itemSinif.Text = "Sınıf";
            itemSinif.TextSize = new Size(120, 16);
            itemSinif.TextToControlDistance = 10;
            itemSinif.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemSinif.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemSinif.AppearanceItemCaption.Options.UseFont = true;
            itemSinif.AppearanceItemCaption.Options.UseForeColor = true;
            
            itemNotOrtalamasi = new LayoutControlItem();
            itemNotOrtalamasi.Control = txtNotOrtalamasi;
            itemNotOrtalamasi.Location = new Point(0, 120);
            itemNotOrtalamasi.Name = "itemNotOrtalamasi";
            itemNotOrtalamasi.Size = new Size(540, 30);
            itemNotOrtalamasi.Text = "Not Ortalaması (GNO)";
            itemNotOrtalamasi.TextSize = new Size(120, 16);
            itemNotOrtalamasi.TextToControlDistance = 10;
            itemNotOrtalamasi.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemNotOrtalamasi.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemNotOrtalamasi.AppearanceItemCaption.Options.UseFont = true;
            itemNotOrtalamasi.AppearanceItemCaption.Options.UseForeColor = true;
            
            itemKardesSayisi = new LayoutControlItem();
            itemKardesSayisi.Control = cmbKardesSayisi;
            itemKardesSayisi.Location = new Point(0, 0);
            itemKardesSayisi.Name = "itemKardesSayisi";
            itemKardesSayisi.Size = new Size(540, 30);
            itemKardesSayisi.Text = "Kardeş Sayısı";
            itemKardesSayisi.TextSize = new Size(120, 16);
            itemKardesSayisi.TextToControlDistance = 10;
            itemKardesSayisi.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemKardesSayisi.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemKardesSayisi.AppearanceItemCaption.Options.UseFont = true;
            itemKardesSayisi.AppearanceItemCaption.Options.UseForeColor = true;
            
            itemAileGeliri = new LayoutControlItem();
            itemAileGeliri.Control = txtAileGeliri;
            itemAileGeliri.Location = new Point(0, 30);
            itemAileGeliri.Name = "itemAileGeliri";
            itemAileGeliri.Size = new Size(540, 30);
            itemAileGeliri.Text = "Aile Aylık Geliri (TL)";
            itemAileGeliri.TextSize = new Size(120, 16);
            itemAileGeliri.TextToControlDistance = 10;
            itemAileGeliri.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemAileGeliri.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemAileGeliri.AppearanceItemCaption.Options.UseFont = true;
            itemAileGeliri.AppearanceItemCaption.Options.UseForeColor = true;
            
            itemAnneMeslek = new LayoutControlItem();
            itemAnneMeslek.Control = txtAnneMeslek;
            itemAnneMeslek.Location = new Point(0, 60);
            itemAnneMeslek.Name = "itemAnneMeslek";
            itemAnneMeslek.Size = new Size(540, 30);
            itemAnneMeslek.Text = "Anne Mesleği";
            itemAnneMeslek.TextSize = new Size(120, 16);
            itemAnneMeslek.TextToControlDistance = 10;
            itemAnneMeslek.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemAnneMeslek.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemAnneMeslek.AppearanceItemCaption.Options.UseFont = true;
            itemAnneMeslek.AppearanceItemCaption.Options.UseForeColor = true;
            
            itemBabaMeslek = new LayoutControlItem();
            itemBabaMeslek.Control = txtBabaMeslek;
            itemBabaMeslek.Location = new Point(0, 90);
            itemBabaMeslek.Name = "itemBabaMeslek";
            itemBabaMeslek.Size = new Size(540, 30);
            itemBabaMeslek.Text = "Baba Mesleği";
            itemBabaMeslek.TextSize = new Size(120, 16);
            itemBabaMeslek.TextToControlDistance = 10;
            itemBabaMeslek.AppearanceItemCaption.Font = new Font("Segoe UI", 9.5F);
            itemBabaMeslek.AppearanceItemCaption.ForeColor = Color.FromArgb(60, 60, 60);
            itemBabaMeslek.AppearanceItemCaption.Options.UseFont = true;
            itemBabaMeslek.AppearanceItemCaption.Options.UseForeColor = true;
            
            itemProfilFoto = new LayoutControlItem();
            itemProfilFoto.Control = pictureEdit1;
            itemProfilFoto.Location = new Point(0, 0);
            itemProfilFoto.Name = "itemProfilFoto";
            itemProfilFoto.Size = new Size(380, 380);
            itemProfilFoto.Text = "Profil Fotoğrafı";
            itemProfilFoto.TextSize = new Size(0, 0);
            itemProfilFoto.TextVisible = false;
            itemProfilFoto.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            
            itemFotoSec = new LayoutControlItem();
            itemFotoSec.Control = btnFotoSec;
            itemFotoSec.Location = new Point(0, 380);
            itemFotoSec.Name = "itemFotoSec";
            itemFotoSec.Size = new Size(380, 45);
            itemFotoSec.TextSize = new Size(0, 0);
            itemFotoSec.TextVisible = false;
            itemFotoSec.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            
            itemBtnKaydet = new LayoutControlItem();
            itemBtnKaydet.Control = btnKaydet;
            itemBtnKaydet.Location = new Point(0, 450);
            itemBtnKaydet.Name = "itemBtnKaydet";
            itemBtnKaydet.Size = new Size(490, 65);
            itemBtnKaydet.TextSize = new Size(0, 0);
            itemBtnKaydet.TextVisible = false;
            itemBtnKaydet.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 5, 10, 10);
            
            itemBtnIptal = new LayoutControlItem();
            itemBtnIptal.Control = btnIptal;
            itemBtnIptal.Location = new Point(490, 450);
            itemBtnIptal.Name = "itemBtnIptal";
            itemBtnIptal.Size = new Size(490, 65);
            itemBtnIptal.TextSize = new Size(0, 0);
            itemBtnIptal.TextVisible = false;
            itemBtnIptal.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 10, 10, 10);
            
            // 
            // groupKisiselBilgiler
            // 
            groupKisiselBilgiler.Items.AddRange(new BaseLayoutItem[] {
                itemAd,
                itemSoyad,
                itemTC,
                itemDogumTarihi
            });
            groupKisiselBilgiler.Location = new Point(0, 0);
            groupKisiselBilgiler.Name = "groupKisiselBilgiler";
            groupKisiselBilgiler.Size = new Size(560, 120);
            groupKisiselBilgiler.Text = "Kişisel Bilgiler";
            groupKisiselBilgiler.AppearanceGroup.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupKisiselBilgiler.AppearanceGroup.ForeColor = Color.FromArgb(42, 42, 42);
            groupKisiselBilgiler.AppearanceGroup.Options.UseFont = true;
            groupKisiselBilgiler.AppearanceGroup.Options.UseForeColor = true;
            groupKisiselBilgiler.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            
            // 
            // groupIletisim
            // 
            groupIletisim.Items.AddRange(new BaseLayoutItem[] {
                itemTelefon,
                itemEmail
            });
            groupIletisim.Location = new Point(0, 120);
            groupIletisim.Name = "groupIletisim";
            groupIletisim.Size = new Size(560, 60);
            groupIletisim.Text = "İletişim Bilgileri";
            groupIletisim.AppearanceGroup.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupIletisim.AppearanceGroup.ForeColor = Color.FromArgb(42, 42, 42);
            groupIletisim.AppearanceGroup.Options.UseFont = true;
            groupIletisim.AppearanceGroup.Options.UseForeColor = true;
            groupIletisim.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            
            // 
            // groupAkademik
            // 
            groupAkademik.Items.AddRange(new BaseLayoutItem[] {
                itemUniversite,
                itemFakulte,
                itemBolum,
                itemSinif,
                itemNotOrtalamasi
            });
            groupAkademik.Location = new Point(0, 180);
            groupAkademik.Name = "groupAkademik";
            groupAkademik.Size = new Size(560, 150);
            groupAkademik.Text = "Akademik Bilgiler";
            groupAkademik.AppearanceGroup.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupAkademik.AppearanceGroup.ForeColor = Color.FromArgb(42, 42, 42);
            groupAkademik.AppearanceGroup.Options.UseFont = true;
            groupAkademik.AppearanceGroup.Options.UseForeColor = true;
            groupAkademik.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            
            // 
            // groupAile
            // 
            groupAile.Items.AddRange(new BaseLayoutItem[] {
                itemKardesSayisi,
                itemAileGeliri,
                itemAnneMeslek,
                itemBabaMeslek
            });
            groupAile.Location = new Point(0, 330);
            groupAile.Name = "groupAile";
            groupAile.Size = new Size(560, 120);
            groupAile.Text = "Aile Bilgileri";
            groupAile.AppearanceGroup.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupAile.AppearanceGroup.ForeColor = Color.FromArgb(42, 42, 42);
            groupAile.AppearanceGroup.Options.UseFont = true;
            groupAile.AppearanceGroup.Options.UseForeColor = true;
            groupAile.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            
            // Sol taraf grubunu oluştur
            groupSolTaraf.Items.AddRange(new BaseLayoutItem[] {
                groupKisiselBilgiler,
                groupIletisim,
                groupAkademik,
                groupAile
            });
            groupSolTaraf.TextVisible = false;
            groupSolTaraf.GroupBordersVisible = false;
            
            // 
            // groupProfilFoto
            // 
            groupProfilFoto.Items.AddRange(new BaseLayoutItem[] {
                itemProfilFoto,
                itemFotoSec
            });
            groupProfilFoto.Location = new Point(0, 0);
            groupProfilFoto.Name = "groupProfilFoto";
            groupProfilFoto.Size = new Size(400, 425);
            groupProfilFoto.Text = "Profil Fotoğrafı";
            groupProfilFoto.AppearanceGroup.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            groupProfilFoto.AppearanceGroup.ForeColor = Color.FromArgb(42, 42, 42);
            groupProfilFoto.AppearanceGroup.Options.UseFont = true;
            groupProfilFoto.AppearanceGroup.Options.UseForeColor = true;
            groupProfilFoto.GroupStyle = DevExpress.Utils.GroupStyle.Title;
            
            // Sağ taraf grubunu oluştur
            groupSagTaraf.Items.AddRange(new BaseLayoutItem[] {
                groupProfilFoto
            });
            groupSagTaraf.TextVisible = false;
            groupSagTaraf.GroupBordersVisible = false;
            
            // Ana layout grubunu oluştur - yatay split
            layoutControlGroup1.Items.AddRange(new BaseLayoutItem[] {
                groupSolTaraf,
                groupSagTaraf,
                itemBtnKaydet,
                itemBtnIptal
            });
            layoutControlGroup1.Name = "Root";
            layoutControlGroup1.Size = new Size(980, 715);
            layoutControlGroup1.TextVisible = false;
            
            // Sabit genişliklerle düzenle - Sol %60, Sağ %40
            groupSolTaraf.Location = new Point(0, 0);
            groupSolTaraf.Size = new Size(580, 450);
            
            groupSagTaraf.Location = new Point(580, 0);
            groupSagTaraf.Size = new Size(400, 450);
            
            // Butonları en alta ekle
            itemBtnKaydet.Location = new Point(0, 450);
            itemBtnKaydet.Size = new Size(490, 65);
            
            itemBtnIptal.Location = new Point(490, 450);
            itemBtnIptal.Size = new Size(490, 65);
            
            // 
            // OgrenciEkleForm
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 800);
            Controls.Add(layoutControl1);
            Controls.Add(panelHeader);
            Name = "OgrenciEkleForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Öğrenci Ekle";
            Appearance.BackColor = Color.White;
            Appearance.Options.UseBackColor = true;
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)panelHeader).EndInit();
            panelHeader.ResumeLayout(false);
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
            ((System.ComponentModel.ISupportInitialize)layoutControlGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupSolTaraf).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupSagTaraf).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupKisiselBilgiler).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupIletisim).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupAkademik).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupAile).EndInit();
            ((System.ComponentModel.ISupportInitialize)groupProfilFoto).EndInit();
            ResumeLayout(false);
        }
    }
}
