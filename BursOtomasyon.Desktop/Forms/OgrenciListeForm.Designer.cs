using DevExpress.XtraEditors;
using System.Windows.Forms;
using System.Drawing;

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
            this.panelKartlar = new Panel();
            this.scrollableControl1 = new ScrollableControl();
            this.panelButtons = new PanelControl();
            this.btnDetay = new SimpleButton();
            this.btnGuncelle = new SimpleButton();
            this.btnSil = new SimpleButton();
            this.btnKapat = new SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).BeginInit();
            this.panelButtons.SuspendLayout();
            this.scrollableControl1.SuspendLayout();
            this.SuspendLayout();
            
            // 
            // panelButtons - Alt kısımda butonlar için panel
            // 
            this.panelButtons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelButtons.Controls.Add(this.btnDetay);
            this.panelButtons.Controls.Add(this.btnGuncelle);
            this.panelButtons.Controls.Add(this.btnSil);
            this.panelButtons.Controls.Add(this.btnKapat);
            this.panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelButtons.Location = new System.Drawing.Point(0, 470);
            this.panelButtons.Name = "panelButtons";
            this.panelButtons.Size = new System.Drawing.Size(590, 50);
            this.panelButtons.TabIndex = 5;
            this.panelButtons.Appearance.BackColor = System.Drawing.Color.FromArgb(248, 249, 250);
            this.panelButtons.Appearance.Options.UseBackColor = true;
            
            // 
            // scrollableControl1
            // 
            this.scrollableControl1.Controls.Add(this.panelKartlar);
            this.scrollableControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollableControl1.Location = new System.Drawing.Point(0, 0);
            this.scrollableControl1.Name = "scrollableControl1";
            this.scrollableControl1.Size = new System.Drawing.Size(590, 470);
            this.scrollableControl1.TabIndex = 0;
            this.scrollableControl1.AutoScroll = true;
            
            // 
            // panelKartlar
            // 
            this.panelKartlar.Location = new System.Drawing.Point(0, 0);
            this.panelKartlar.Name = "panelKartlar";
            this.panelKartlar.Size = new System.Drawing.Size(570, 450);
            this.panelKartlar.TabIndex = 0;
            this.panelKartlar.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            
            // 
            // btnDetay
            // 
            this.btnDetay.Location = new System.Drawing.Point(10, 8);
            this.btnDetay.Name = "btnDetay";
            this.btnDetay.Size = new System.Drawing.Size(90, 32);
            this.btnDetay.TabIndex = 1;
            this.btnDetay.Text = "Detay";
            this.btnDetay.Click += new System.EventHandler(this.btnDetay_Click);
            
            // 
            // btnGuncelle
            // 
            this.btnGuncelle.Location = new System.Drawing.Point(110, 8);
            this.btnGuncelle.Name = "btnGuncelle";
            this.btnGuncelle.Size = new System.Drawing.Size(90, 32);
            this.btnGuncelle.TabIndex = 2;
            this.btnGuncelle.Text = "Güncelle";
            this.btnGuncelle.Visible = false;
            this.btnGuncelle.Click += new System.EventHandler(this.btnGuncelle_Click);
            
            // 
            // btnSil
            // 
            this.btnSil.Location = new System.Drawing.Point(210, 8);
            this.btnSil.Name = "btnSil";
            this.btnSil.Size = new System.Drawing.Size(90, 32);
            this.btnSil.TabIndex = 3;
            this.btnSil.Text = "Sil";
            this.btnSil.Click += new System.EventHandler(this.btnSil_Click);
            
            // 
            // btnKapat
            // 
            this.btnKapat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKapat.Location = new System.Drawing.Point(490, 8);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(90, 32);
            this.btnKapat.TabIndex = 4;
            this.btnKapat.Text = "Kapat";
            this.btnKapat.Click += new System.EventHandler(this.btnKapat_Click);
            
            // 
            // OgrenciListeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 520);
            this.Controls.Add(this.scrollableControl1);
            this.Controls.Add(this.panelButtons);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new System.Drawing.Size(550, 500);
            this.Name = "OgrenciListeForm";
            this.Text = "Öğrenci Listesi";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.Appearance.Options.UseBackColor = true;
            ((System.ComponentModel.ISupportInitialize)(this.panelButtons)).EndInit();
            this.panelButtons.ResumeLayout(false);
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
