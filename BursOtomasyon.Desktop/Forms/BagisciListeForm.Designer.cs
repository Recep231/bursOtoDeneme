using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;

namespace BursOtomasyon.Desktop.Forms
{
    partial class BagisciListeForm
    {
        private System.ComponentModel.IContainer components = null;
        private GridControl gridControl1 = null!;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1 = null!;
        private GridControl gridControlBagisAkisi = null!;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBagisAkisi = null!;
        private SimpleButton btnYenile = null!;
        private SimpleButton btnKapat = null!;
        private LabelControl lblBagiscilar = null!;
        private LabelControl lblBagisAkisi = null!;
        private PanelControl panelKasa = null!;
        private LabelControl lblKasaBaslik = null!;
        private LabelControl lblToplamGelenBaslik = null!;
        private LabelControl lblToplamGelen = null!;
        private LabelControl lblBeklemedeBaslik = null!;
        private LabelControl lblBeklemede = null!;
        private LabelControl lblOnaylananBaslik = null!;
        private LabelControl lblOnaylanan = null!;
        private LabelControl lblSonGuncelleme = null!;
        private PictureEdit pictureEditKasa = null!;
        private PictureEdit pictureEditBagiscilar = null!;

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
            this.gridControl1 = new GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControlBagisAkisi = new GridControl();
            this.gridViewBagisAkisi = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnYenile = new SimpleButton();
            this.btnKapat = new SimpleButton();
            this.lblBagiscilar = new LabelControl();
            this.lblBagisAkisi = new LabelControl();
            this.panelKasa = new PanelControl();
            this.lblKasaBaslik = new LabelControl();
            this.lblToplamGelenBaslik = new LabelControl();
            this.lblToplamGelen = new LabelControl();
            this.lblBeklemedeBaslik = new LabelControl();
            this.lblBeklemede = new LabelControl();
            this.lblOnaylananBaslik = new LabelControl();
            this.lblOnaylanan = new LabelControl();
            this.lblSonGuncelleme = new LabelControl();
            this.pictureEditKasa = new PictureEdit();
            this.pictureEditBagiscilar = new PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBagisAkisi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBagisAkisi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelKasa)).BeginInit();
            this.panelKasa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditKasa.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditBagiscilar.Properties)).BeginInit();
            this.SuspendLayout();
            
            // 
            // pictureEditBagiscilar
            // 
            this.pictureEditBagiscilar.Location = new System.Drawing.Point(12, 8);
            this.pictureEditBagiscilar.Name = "pictureEditBagiscilar";
            this.pictureEditBagiscilar.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEditBagiscilar.Properties.ShowMenu = false;
            this.pictureEditBagiscilar.Properties.ReadOnly = true;
            this.pictureEditBagiscilar.Size = new System.Drawing.Size(32, 32);
            this.pictureEditBagiscilar.TabIndex = 5;
            
            // 
            // lblBagiscilar
            // 
            this.lblBagiscilar.Location = new System.Drawing.Point(50, 12);
            this.lblBagiscilar.Name = "lblBagiscilar";
            this.lblBagiscilar.Size = new System.Drawing.Size(200, 20);
            this.lblBagiscilar.Text = "Bağışçılar";
            this.lblBagiscilar.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblBagiscilar.Appearance.ForeColor = System.Drawing.Color.FromArgb(33, 150, 243);
            
            // 
            // gridControl1
            // 
            this.gridControl1.Location = new System.Drawing.Point(12, 35);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(580, 250);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            
            // 
            // panelKasa
            // 
            this.panelKasa.Controls.Add(this.pictureEditKasa);
            this.panelKasa.Controls.Add(this.lblSonGuncelleme);
            this.panelKasa.Controls.Add(this.lblOnaylanan);
            this.panelKasa.Controls.Add(this.lblOnaylananBaslik);
            this.panelKasa.Controls.Add(this.lblBeklemede);
            this.panelKasa.Controls.Add(this.lblBeklemedeBaslik);
            this.panelKasa.Controls.Add(this.lblToplamGelen);
            this.panelKasa.Controls.Add(this.lblToplamGelenBaslik);
            this.panelKasa.Controls.Add(this.lblKasaBaslik);
            this.panelKasa.Location = new System.Drawing.Point(608, 35);
            this.panelKasa.Name = "panelKasa";
            this.panelKasa.Size = new System.Drawing.Size(580, 250);
            this.panelKasa.TabIndex = 1;
            this.panelKasa.Appearance.BackColor = System.Drawing.Color.FromArgb(245, 245, 250);
            this.panelKasa.Appearance.BorderColor = System.Drawing.Color.FromArgb(200, 200, 200);
            this.panelKasa.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            
            // 
            // lblKasaBaslik
            // 
            this.lblKasaBaslik.Location = new System.Drawing.Point(15, 15);
            this.lblKasaBaslik.Name = "lblKasaBaslik";
            this.lblKasaBaslik.Size = new System.Drawing.Size(200, 20);
            this.lblKasaBaslik.Text = "KASA";
            this.lblKasaBaslik.Appearance.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblKasaBaslik.Appearance.ForeColor = System.Drawing.Color.FromArgb(64, 64, 64);
            
            // 
            // pictureEditKasa
            // 
            this.pictureEditKasa.Location = new System.Drawing.Point(450, 15);
            this.pictureEditKasa.Name = "pictureEditKasa";
            this.pictureEditKasa.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            this.pictureEditKasa.Properties.ShowMenu = false;
            this.pictureEditKasa.Size = new System.Drawing.Size(100, 100);
            this.pictureEditKasa.TabIndex = 8;
            // Icon kod tarafında yüklenecek
            
            // 
            // lblToplamGelenBaslik
            // 
            this.lblToplamGelenBaslik.Location = new System.Drawing.Point(15, 50);
            this.lblToplamGelenBaslik.Name = "lblToplamGelenBaslik";
            this.lblToplamGelenBaslik.Size = new System.Drawing.Size(150, 16);
            this.lblToplamGelenBaslik.Text = "Toplam Gelen Bağış:";
            this.lblToplamGelenBaslik.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            
            // 
            // lblToplamGelen
            // 
            this.lblToplamGelen.Location = new System.Drawing.Point(180, 50);
            this.lblToplamGelen.Name = "lblToplamGelen";
            this.lblToplamGelen.Size = new System.Drawing.Size(200, 20);
            this.lblToplamGelen.Text = "0,00 ₺";
            this.lblToplamGelen.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblToplamGelen.Appearance.ForeColor = System.Drawing.Color.FromArgb(33, 150, 243);
            
            // 
            // lblBeklemedeBaslik
            // 
            this.lblBeklemedeBaslik.Location = new System.Drawing.Point(15, 85);
            this.lblBeklemedeBaslik.Name = "lblBeklemedeBaslik";
            this.lblBeklemedeBaslik.Size = new System.Drawing.Size(150, 16);
            this.lblBeklemedeBaslik.Text = "Beklemede:";
            this.lblBeklemedeBaslik.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            
            // 
            // lblBeklemede
            // 
            this.lblBeklemede.Location = new System.Drawing.Point(180, 85);
            this.lblBeklemede.Name = "lblBeklemede";
            this.lblBeklemede.Size = new System.Drawing.Size(200, 20);
            this.lblBeklemede.Text = "0,00 ₺";
            this.lblBeklemede.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblBeklemede.Appearance.ForeColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.lblBeklemede.Appearance.BackColor = System.Drawing.Color.FromArgb(255, 235, 59);
            this.lblBeklemede.Appearance.BorderColor = System.Drawing.Color.FromArgb(255, 193, 7);
            this.lblBeklemede.Appearance.Options.UseBackColor = true;
            this.lblBeklemede.Appearance.Options.UseBorderColor = true;
            this.lblBeklemede.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblBeklemede.AutoSizeMode = LabelAutoSizeMode.None;
            this.lblBeklemede.Size = new System.Drawing.Size(200, 25);
            
            // 
            // lblOnaylananBaslik
            // 
            this.lblOnaylananBaslik.Location = new System.Drawing.Point(15, 120);
            this.lblOnaylananBaslik.Name = "lblOnaylananBaslik";
            this.lblOnaylananBaslik.Size = new System.Drawing.Size(150, 16);
            this.lblOnaylananBaslik.Text = "Onaylanan:";
            this.lblOnaylananBaslik.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular);
            
            // 
            // lblOnaylanan
            // 
            this.lblOnaylanan.Location = new System.Drawing.Point(180, 120);
            this.lblOnaylanan.Name = "lblOnaylanan";
            this.lblOnaylanan.Size = new System.Drawing.Size(200, 20);
            this.lblOnaylanan.Text = "0,00 ₺";
            this.lblOnaylanan.Appearance.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold);
            this.lblOnaylanan.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblOnaylanan.Appearance.BackColor = System.Drawing.Color.FromArgb(76, 175, 80);
            this.lblOnaylanan.Appearance.BorderColor = System.Drawing.Color.FromArgb(56, 142, 60);
            this.lblOnaylanan.Appearance.Options.UseBackColor = true;
            this.lblOnaylanan.Appearance.Options.UseBorderColor = true;
            this.lblOnaylanan.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lblOnaylanan.AutoSizeMode = LabelAutoSizeMode.None;
            this.lblOnaylanan.Size = new System.Drawing.Size(200, 25);
            
            // 
            // lblSonGuncelleme
            // 
            this.lblSonGuncelleme.Location = new System.Drawing.Point(15, 220);
            this.lblSonGuncelleme.Name = "lblSonGuncelleme";
            this.lblSonGuncelleme.Size = new System.Drawing.Size(500, 16);
            this.lblSonGuncelleme.Text = "Son Güncelleme: -";
            this.lblSonGuncelleme.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Italic);
            this.lblSonGuncelleme.Appearance.ForeColor = System.Drawing.Color.FromArgb(128, 128, 128);
            
            // 
            // lblBagisAkisi
            // 
            this.lblBagisAkisi.Location = new System.Drawing.Point(12, 300);
            this.lblBagisAkisi.Name = "lblBagisAkisi";
            this.lblBagisAkisi.Size = new System.Drawing.Size(200, 16);
            this.lblBagisAkisi.Text = "Bağış Akışı";
            this.lblBagisAkisi.Appearance.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            
            // 
            // gridControlBagisAkisi
            // 
            this.gridControlBagisAkisi.Location = new System.Drawing.Point(12, 323);
            this.gridControlBagisAkisi.MainView = this.gridViewBagisAkisi;
            this.gridControlBagisAkisi.Name = "gridControlBagisAkisi";
            this.gridControlBagisAkisi.Size = new System.Drawing.Size(1176, 400);
            this.gridControlBagisAkisi.TabIndex = 2;
            this.gridControlBagisAkisi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBagisAkisi});
            
            // 
            // gridViewBagisAkisi
            // 
            this.gridViewBagisAkisi.GridControl = this.gridControlBagisAkisi;
            this.gridViewBagisAkisi.Name = "gridViewBagisAkisi";
            
            // 
            // btnYenile
            // 
            this.btnYenile.Location = new System.Drawing.Point(12, 740);
            this.btnYenile.Name = "btnYenile";
            this.btnYenile.Size = new System.Drawing.Size(100, 30);
            this.btnYenile.TabIndex = 3;
            this.btnYenile.Text = "Yenile";
            this.btnYenile.Click += new System.EventHandler(this.btnYenile_Click);
            
            // 
            // btnKapat
            // 
            this.btnKapat.Location = new System.Drawing.Point(1088, 740);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(100, 30);
            this.btnKapat.TabIndex = 4;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);
            
            // 
            // BagisciListeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 780);
            this.Controls.Add(this.pictureEditBagiscilar);
            this.Controls.Add(this.lblBagiscilar);
            this.Controls.Add(this.lblBagisAkisi);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.btnYenile);
            this.Controls.Add(this.gridControlBagisAkisi);
            this.Controls.Add(this.panelKasa);
            this.Controls.Add(this.gridControl1);
            this.Name = "BagisciListeForm";
            this.Text = "Bağışçılar ve Bağışlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlBagisAkisi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBagisAkisi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelKasa)).EndInit();
            this.panelKasa.ResumeLayout(false);
            this.panelKasa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditKasa.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEditBagiscilar.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
