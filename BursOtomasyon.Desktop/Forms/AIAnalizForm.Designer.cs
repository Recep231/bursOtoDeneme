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
        private PanelControl panelButtons = null!;
        private PanelControl panelRapor = null!;
        private LabelControl lblRaporBaslik = null!;

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
            panelButtons = new PanelControl();
            panelRapor = new PanelControl();
            lblRaporBaslik = new LabelControl();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelButtons).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelRapor).BeginInit();
            SuspendLayout();
            // 
            // panelButtons
            // 
            panelButtons.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            panelButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            panelButtons.Location = new System.Drawing.Point(0, 510);
            panelButtons.Name = "panelButtons";
            panelButtons.Size = new System.Drawing.Size(580, 45);
            panelButtons.TabIndex = 10;
            // 
            // gridControl1
            // 
            gridControl1.Dock = System.Windows.Forms.DockStyle.Top;
            gridControl1.Location = new System.Drawing.Point(0, 0);
            gridControl1.MainView = gridView1;
            gridControl1.Name = "gridControl1";
            gridControl1.Size = new System.Drawing.Size(580, 250);
            gridControl1.TabIndex = 0;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            // 
            // panelRapor
            // 
            panelRapor.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            panelRapor.Dock = System.Windows.Forms.DockStyle.Fill;
            panelRapor.Location = new System.Drawing.Point(0, 250);
            panelRapor.Name = "panelRapor";
            panelRapor.Size = new System.Drawing.Size(580, 260);
            panelRapor.TabIndex = 11;
            panelRapor.Appearance.BackColor = System.Drawing.Color.White;
            panelRapor.Appearance.Options.UseBackColor = true;
            // 
            // lblRaporBaslik
            // 
            lblRaporBaslik.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            lblRaporBaslik.Appearance.ForeColor = System.Drawing.Color.FromArgb(33, 37, 41);
            lblRaporBaslik.Location = new System.Drawing.Point(10, 8);
            lblRaporBaslik.Name = "lblRaporBaslik";
            lblRaporBaslik.Size = new System.Drawing.Size(200, 20);
            lblRaporBaslik.Text = "📋 AI Analiz Raporu";
            // 
            // rtbAnaliz
            // 
            rtbAnaliz.BorderStyle = System.Windows.Forms.BorderStyle.None;
            rtbAnaliz.Dock = System.Windows.Forms.DockStyle.Fill;
            rtbAnaliz.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            rtbAnaliz.Location = new System.Drawing.Point(0, 30);
            rtbAnaliz.Name = "rtbAnaliz";
            rtbAnaliz.ReadOnly = true;
            rtbAnaliz.BackColor = System.Drawing.Color.White;
            rtbAnaliz.Size = new System.Drawing.Size(576, 226);
            rtbAnaliz.TabIndex = 1;
            rtbAnaliz.Text = "Lütfen yukarıdaki listeden bir öğrenci seçin ve 'AI Analiz Yap' butonuna tıklayın...";
            // 
            // btnAnalizYap
            // 
            btnAnalizYap.Location = new System.Drawing.Point(10, 8);
            btnAnalizYap.Name = "btnAnalizYap";
            btnAnalizYap.Size = new System.Drawing.Size(180, 32);
            btnAnalizYap.TabIndex = 2;
            btnAnalizYap.Text = "🤖 AI Analiz Yap";
            btnAnalizYap.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            btnAnalizYap.Appearance.BackColor = System.Drawing.Color.FromArgb(0, 123, 255);
            btnAnalizYap.Appearance.ForeColor = System.Drawing.Color.White;
            btnAnalizYap.Appearance.Options.UseFont = true;
            btnAnalizYap.Appearance.Options.UseBackColor = true;
            btnAnalizYap.Appearance.Options.UseForeColor = true;
            btnAnalizYap.Click += new System.EventHandler(btnAnalizYap_Click);
            // 
            // btnKapat
            // 
            btnKapat.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            btnKapat.Location = new System.Drawing.Point(480, 8);
            btnKapat.Name = "btnKapat";
            btnKapat.Size = new System.Drawing.Size(90, 32);
            btnKapat.TabIndex = 3;
            btnKapat.Text = "Kapat";
            btnKapat.Click += new System.EventHandler(btnKapat_Click);
            // 
            // AIAnalizForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(580, 555);
            panelRapor.Controls.Add(rtbAnaliz);
            panelRapor.Controls.Add(lblRaporBaslik);
            panelButtons.Controls.Add(btnAnalizYap);
            panelButtons.Controls.Add(btnKapat);
            Controls.Add(panelRapor);
            Controls.Add(panelButtons);
            Controls.Add(gridControl1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;
            MinimumSize = new System.Drawing.Size(550, 500);
            Name = "AIAnalizForm";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "AI Analiz Raporu";
            ((System.ComponentModel.ISupportInitialize)gridControl1).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelButtons).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelRapor).EndInit();
            ResumeLayout(false);
        }

        private void btnKapat_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}

