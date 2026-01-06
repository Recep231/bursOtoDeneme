using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using System.Drawing;
using System.Windows.Forms;

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
        private PanelControl panelButtons = null!;

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
            panelButtons = new PanelControl();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelButtons).BeginInit();
            SuspendLayout();
            // 
            // panelButtons
            // 
            panelButtons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelButtons.Location = new System.Drawing.Point(0, 505);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new System.Drawing.Size(580, 50);
            panelButtons.TabIndex = 10;
            // 
            // gridControl1
            // 
            gridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            gridControl1.Location = new System.Drawing.Point(0, 0);
            gridControl1.MainView = gridView1;
            gridControl1.Name = "gridControl1";
            gridControl1.Size = new System.Drawing.Size(580, 505);
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
            btnSecilenOgrenciPuanHesapla.Location = new System.Drawing.Point(10, 10);
            btnSecilenOgrenciPuanHesapla.Name = "btnSecilenOgrenciPuanHesapla";
            btnSecilenOgrenciPuanHesapla.Size = new System.Drawing.Size(180, 32);
            btnSecilenOgrenciPuanHesapla.TabIndex = 1;
            btnSecilenOgrenciPuanHesapla.Text = "📊 Seçilen Puanı Hesapla";
            btnSecilenOgrenciPuanHesapla.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnSecilenOgrenciPuanHesapla.Appearance.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnSecilenOgrenciPuanHesapla.Appearance.ForeColor = System.Drawing.Color.White;
            btnSecilenOgrenciPuanHesapla.Appearance.Options.UseFont = true;
            btnSecilenOgrenciPuanHesapla.Appearance.Options.UseBackColor = true;
            btnSecilenOgrenciPuanHesapla.Appearance.Options.UseForeColor = true;
            btnSecilenOgrenciPuanHesapla.Click += new System.EventHandler(btnSecilenOgrenciPuanHesapla_Click);
            // 
            // btnTumPuanlariHesapla
            // 
            btnTumPuanlariHesapla.Location = new System.Drawing.Point(200, 10);
            btnTumPuanlariHesapla.Name = "btnTumPuanlariHesapla";
            btnTumPuanlariHesapla.Size = new System.Drawing.Size(170, 32);
            btnTumPuanlariHesapla.TabIndex = 2;
            btnTumPuanlariHesapla.Text = "📋 Tüm Puanları Hesapla";
            btnTumPuanlariHesapla.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            btnTumPuanlariHesapla.Appearance.BackColor = System.Drawing.Color.FromArgb(40, 167, 69);
            btnTumPuanlariHesapla.Appearance.ForeColor = System.Drawing.Color.White;
            btnTumPuanlariHesapla.Appearance.Options.UseFont = true;
            btnTumPuanlariHesapla.Appearance.Options.UseBackColor = true;
            btnTumPuanlariHesapla.Appearance.Options.UseForeColor = true;
            btnTumPuanlariHesapla.Click += new System.EventHandler(btnTumPuanlariHesapla_Click);
            // 
            // btnKapat
            // 
            btnKapat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnKapat.Location = new System.Drawing.Point(480, 10);
            btnKapat.Name = "btnKapat";
            btnKapat.Size = new System.Drawing.Size(90, 32);
            btnKapat.TabIndex = 3;
            btnKapat.Text = "Kapat";
            btnKapat.Click += new System.EventHandler(btnKapat_Click);
            // 
            // BursPuaniHesaplaForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(580, 555);
            panelButtons.Controls.Add(btnSecilenOgrenciPuanHesapla);
            panelButtons.Controls.Add(btnTumPuanlariHesapla);
            panelButtons.Controls.Add(btnKapat);
            Controls.Add(gridControl1);
            Controls.Add(panelButtons);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;
            MinimumSize = new System.Drawing.Size(550, 500);
            Name = "BursPuaniHesaplaForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "Burs Puanı Hesapla";
            ((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelButtons).EndInit();
            ResumeLayout(false);
        }

        private void btnKapat_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

