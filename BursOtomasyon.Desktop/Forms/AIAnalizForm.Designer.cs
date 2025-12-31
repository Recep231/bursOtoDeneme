using DevExpress.XtraEditors;
using DevExpress.XtraGrid;

namespace BursOtomasyon.Desktop.Forms
{
    partial class AIAnalizForm
    {
        private System.ComponentModel.IContainer components = null;
        private GridControl gridControl1 = null!;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1 = null!;
        private RichTextBox rtbAnaliz = null!;
        private SimpleButton btnAnalizYap = null!;
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
            rtbAnaliz = new RichTextBox();
            btnAnalizYap = new SimpleButton();
            btnKapat = new SimpleButton();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            SuspendLayout();
            // 
            // gridControl1
            // 
            gridControl1.Dock = System.Windows.Forms.DockStyle.Left;
            gridControl1.Location = new System.Drawing.Point(0, 0);
            gridControl1.MainView = gridView1;
            gridControl1.Name = "gridControl1";
            gridControl1.Size = new System.Drawing.Size(600, 550);
            gridControl1.TabIndex = 0;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            // 
            // rtbAnaliz
            // 
            rtbAnaliz.Dock = System.Windows.Forms.DockStyle.Fill;
            rtbAnaliz.Font = new System.Drawing.Font("Segoe UI", 10F);
            rtbAnaliz.Location = new System.Drawing.Point(600, 0);
            rtbAnaliz.Name = "rtbAnaliz";
            rtbAnaliz.ReadOnly = true;
            rtbAnaliz.Size = new System.Drawing.Size(400, 470);
            rtbAnaliz.TabIndex = 1;
            rtbAnaliz.Text = "Lütfen bir öğrenci seçin ve 'AI Analiz Yap' butonuna tıklayın...";
            // 
            // btnAnalizYap
            // 
            btnAnalizYap.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnAnalizYap.Location = new System.Drawing.Point(600, 470);
            btnAnalizYap.Name = "btnAnalizYap";
            btnAnalizYap.Size = new System.Drawing.Size(400, 40);
            btnAnalizYap.TabIndex = 2;
            btnAnalizYap.Text = "AI Analiz Yap";
            btnAnalizYap.Click += new System.EventHandler(btnAnalizYap_Click);
            // 
            // btnKapat
            // 
            btnKapat.Dock = System.Windows.Forms.DockStyle.Bottom;
            btnKapat.Location = new System.Drawing.Point(600, 510);
            btnKapat.Name = "btnKapat";
            btnKapat.Size = new System.Drawing.Size(400, 40);
            btnKapat.TabIndex = 3;
            btnKapat.Text = "Kapat";
            btnKapat.Click += new System.EventHandler(btnKapat_Click);
            // 
            // AIAnalizForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1000, 550);
            Controls.Add(rtbAnaliz);
            Controls.Add(btnAnalizYap);
            Controls.Add(btnKapat);
            Controls.Add(gridControl1);
            Name = "AIAnalizForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "AI Analiz Raporu";
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

