using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace BursOtomasyon.Desktop.Forms
{
    partial class BasvuruDetayForm
    {
        private System.ComponentModel.IContainer components = null;
        private RichTextBox rtbDetay = null!;
        private SimpleButton btnKapat = null!;
        private PictureEdit pictureEdit1 = null!;
        private LabelControl lblProfilFoto = null!;

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
            rtbDetay = new RichTextBox();
            btnKapat = new SimpleButton();
            pictureEdit1 = new PictureEdit();
            lblProfilFoto = new LabelControl();
            ((System.ComponentModel.ISupportInitialize)(pictureEdit1.Properties)).BeginInit();
            SuspendLayout();
            
            // 
            // lblProfilFoto
            // 
            lblProfilFoto.Location = new System.Drawing.Point(630, 12);
            lblProfilFoto.Name = "lblProfilFoto";
            lblProfilFoto.Size = new System.Drawing.Size(100, 16);
            lblProfilFoto.Text = "Profil Fotoğrafı";
            lblProfilFoto.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            
            // 
            // pictureEdit1
            // 
            pictureEdit1.Location = new System.Drawing.Point(630, 35);
            pictureEdit1.Name = "pictureEdit1";
            pictureEdit1.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Zoom;
            pictureEdit1.Properties.ShowMenu = false;
            pictureEdit1.Size = new System.Drawing.Size(200, 200);
            pictureEdit1.TabIndex = 2;
            pictureEdit1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            
            // 
            // rtbDetay
            // 
            rtbDetay.Font = new Font("Consolas", 9F);
            rtbDetay.Location = new Point(12, 12);
            rtbDetay.Name = "rtbDetay";
            rtbDetay.ReadOnly = true;
            rtbDetay.Size = new Size(600, 400);
            rtbDetay.TabIndex = 0;
            rtbDetay.Text = "";
            
            // 
            // btnKapat
            // 
            btnKapat.Location = new Point(730, 450);
            btnKapat.Name = "btnKapat";
            btnKapat.Size = new Size(100, 30);
            btnKapat.TabIndex = 1;
            btnKapat.Text = "Kapat";
            btnKapat.Click += btnKapat_Click;
            
            // 
            // BasvuruDetayForm
            // 
            ClientSize = new Size(850, 500);
            Controls.Add(lblProfilFoto);
            Controls.Add(pictureEdit1);
            Controls.Add(rtbDetay);
            Controls.Add(btnKapat);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "BasvuruDetayForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Başvuru Detayı";
            ((System.ComponentModel.ISupportInitialize)(pictureEdit1.Properties)).EndInit();
            ResumeLayout(false);
        }

        private void btnKapat_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

