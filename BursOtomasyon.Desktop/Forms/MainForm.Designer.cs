using DevExpress.XtraBars.Ribbon;

namespace BursOtomasyon.Desktop.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1 = null!;

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
            ribbonControl1 = new RibbonControl();
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).BeginInit();
            SuspendLayout();
            // 
            // ribbonControl1
            // 
            ribbonControl1.EmptyAreaImageOptions.ImagePadding = new Padding(30, 32, 30, 32);
            ribbonControl1.ExpandCollapseItem.Id = 0;
            ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbonControl1.ExpandCollapseItem });
            ribbonControl1.Location = new Point(0, 0);
            ribbonControl1.MaxItemId = 1;
            ribbonControl1.Name = "ribbonControl1";
            ribbonControl1.Size = new Size(1200, 71);
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1200, 853);
            Controls.Add(ribbonControl1);
            Name = "MainForm";
            Ribbon = ribbonControl1;
            Text = "Öğrenci Burs Yönetim Sistemi";
            ((System.ComponentModel.ISupportInitialize)ribbonControl1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}

