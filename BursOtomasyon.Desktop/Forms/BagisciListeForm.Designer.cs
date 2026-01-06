using DevExpress.XtraEditors;

namespace BursOtomasyon.Desktop.Forms
{
    partial class BagisciListeForm
    {
        private System.ComponentModel.IContainer components = null;

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
            this.SuspendLayout();
            // 
            // BagisciListeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 700);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.MaximizeBox = true;
            this.MinimizeBox = true;
            this.MinimumSize = new System.Drawing.Size(550, 600);
            this.Name = "BagisciListeForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Bağışçı Yönetimi";
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(245, 247, 250);
            this.Appearance.Options.UseBackColor = true;
            this.ResumeLayout(false);
        }
    }
}
