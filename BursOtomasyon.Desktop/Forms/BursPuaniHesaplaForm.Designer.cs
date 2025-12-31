using DevExpress.XtraEditors;
using DevExpress.XtraGrid;

namespace BursOtomasyon.Desktop.Forms
{
    partial class BursPuaniHesaplaForm
    {
        private System.ComponentModel.IContainer components = null;
        private GridControl gridControl1 = null!;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1 = null!;
        private SimpleButton btnSecilenOgrenciPuanHesapla = null!;
        private SimpleButton btnTumPuanlariHesapla = null!;
        private SimpleButton btnKapat = null!;

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
            gridControl1 = new GridControl();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            btnSecilenOgrenciPuanHesapla = new SimpleButton();
            btnTumPuanlariHesapla = new SimpleButton();
            btnKapat = new SimpleButton();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            SuspendLayout();
            // 
            // gridControl1
            // 
            gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControl1.Location = new System.Drawing.Point(0, 0);
            gridControl1.MainView = gridView1;
            gridControl1.Name = "gridControl1";
            gridControl1.Size = new System.Drawing.Size(1000, 500);
            gridControl1.TabIndex = 0;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            // 
            // btnSecilenOgrenciPuanHesapla
            // 
            btnSecilenOgrenciPuanHesapla.Location = new System.Drawing.Point(12, 510);
            btnSecilenOgrenciPuanHesapla.Name = "btnSecilenOgrenciPuanHesapla";
            btnSecilenOgrenciPuanHesapla.Size = new System.Drawing.Size(150, 32);
            btnSecilenOgrenciPuanHesapla.TabIndex = 1;
            btnSecilenOgrenciPuanHesapla.Text = "Seçilen Öğrenci Puanı Hesapla";
            btnSecilenOgrenciPuanHesapla.Click += new System.EventHandler(btnSecilenOgrenciPuanHesapla_Click);
            // 
            // btnTumPuanlariHesapla
            // 
            btnTumPuanlariHesapla.Location = new System.Drawing.Point(168, 510);
            btnTumPuanlariHesapla.Name = "btnTumPuanlariHesapla";
            btnTumPuanlariHesapla.Size = new System.Drawing.Size(150, 32);
            btnTumPuanlariHesapla.TabIndex = 2;
            btnTumPuanlariHesapla.Text = "Tüm Puanları Hesapla";
            btnTumPuanlariHesapla.Click += new System.EventHandler(btnTumPuanlariHesapla_Click);
            // 
            // btnKapat
            // 
            btnKapat.Location = new System.Drawing.Point(888, 510);
            btnKapat.Name = "btnKapat";
            btnKapat.Size = new System.Drawing.Size(100, 32);
            btnKapat.TabIndex = 3;
            btnKapat.Text = "Kapat";
            btnKapat.Click += new System.EventHandler(btnKapat_Click);
            // 
            // BursPuaniHesaplaForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1000, 550);
            Controls.Add(btnKapat);
            Controls.Add(btnTumPuanlariHesapla);
            Controls.Add(btnSecilenOgrenciPuanHesapla);
            Controls.Add(gridControl1);
            Name = "BursPuaniHesaplaForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Burs Puanı Hesapla";
            ((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ResumeLayout(false);
        }

        private void btnKapat_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

