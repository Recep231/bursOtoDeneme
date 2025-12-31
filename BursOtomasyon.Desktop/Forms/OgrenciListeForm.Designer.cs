using DevExpress.XtraEditors;
using System.Windows.Forms;

namespace BursOtomasyon.Desktop.Forms
{
    partial class OgrenciListeForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel panelKartlar = null!;
        private SimpleButton btnDetay = null!;
        private SimpleButton btnGuncelle = null!;
        private SimpleButton btnSil = null!;
        private SimpleButton btnKapat = null!;
        private ScrollableControl scrollableControl1 = null!;

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
            this.panelKartlar = new Panel();
            this.scrollableControl1 = new ScrollableControl();
            this.btnDetay = new SimpleButton();
            this.btnGuncelle = new SimpleButton();
            this.btnSil = new SimpleButton();
            this.btnKapat = new SimpleButton();
            this.scrollableControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // scrollableControl1
            // 
            this.scrollableControl1.Controls.Add(this.panelKartlar);
            this.scrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollableControl1.Location = new System.Drawing.Point(0, 0);
            this.scrollableControl1.Name = "scrollableControl1";
            this.scrollableControl1.Size = new System.Drawing.Size(1200, 600);
            this.scrollableControl1.TabIndex = 0;
            this.scrollableControl1.AutoScroll = true;
            // 
            // panelKartlar
            // 
            this.panelKartlar.Location = new System.Drawing.Point(0, 0);
            this.panelKartlar.Name = "panelKartlar";
            this.panelKartlar.Size = new System.Drawing.Size(1183, 583);
            this.panelKartlar.TabIndex = 0;
            // 
            // btnDetay
            // 
            this.btnDetay.Location = new System.Drawing.Point(15, 615);
            this.btnDetay.Name = "btnDetay";
            this.btnDetay.Size = new System.Drawing.Size(110, 35);
            this.btnDetay.TabIndex = 1;
            this.btnDetay.Text = "Detay";
            this.btnDetay.Click += new System.EventHandler(this.btnDetay_Click);
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.Location = new System.Drawing.Point(135, 615);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(110, 35);
            this.btnGuncelle.TabIndex = 2;
            this.btnGuncelle.Text = "Güncelle";
            this.btnGuncelle.Visible = false;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            // 
            // btnSil
            // 
            this.btnSil.Location = new System.Drawing.Point(255, 615);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(110, 35);
            this.btnSil.TabIndex = 3;
            this.btnSil.Text = "Sil";
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            // 
            // btnKapat
            // 
            this.btnKapat.Location = new System.Drawing.Point(1075, 615);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(110, 35);
            this.btnKapat.TabIndex = 4;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);
            // 
            // OgrenciListeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 650);
            this.Controls.Add(this.btnKapat);
            this.Controls.Add(this.btnSil);
            this.Controls.Add(this.btnGuncelle);
            this.Controls.Add(this.btnDetay);
            this.Controls.Add(this.scrollableControl1);
            this.Name = "OgrenciListeForm";
            this.Text = "Öğrenci Listesi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.scrollableControl1.ResumeLayout(false);
            this.scrollableControl1.PerformLayout();
            this.ResumeLayout(false);
        }

        private void btnKapat_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

