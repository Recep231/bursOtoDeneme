using DevExpress.XtraEditors;
using DevExpress.XtraGrid;

namespace BursOtomasyon.Desktop.Forms
{
    partial class BasvuruListeForm
    {
        private System.ComponentModel.IContainer components = null;
        private GridControl gridControl1 = null!;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1 = null!;
        private SimpleButton btnDetay = null!;
        private SimpleButton btnOnayla = null!;
        private SimpleButton btnReddet = null!;
        private SimpleButton btnSecilenleriSil = null!;
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
            btnDetay = new SimpleButton();
            btnOnayla = new SimpleButton();
            btnReddet = new SimpleButton();
            btnSecilenleriSil = new SimpleButton();
            btnKapat = new SimpleButton();
            ((System.ComponentModel.ISupportInitialize)gridControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            SuspendLayout();
            // 
            // gridControl1
            // 
            gridControl1.Dock = DockStyle.Fill;
            gridControl1.Location = new Point(0, 0);
            gridControl1.MainView = gridView1;
            gridControl1.Name = "gridControl1";
            gridControl1.Size = new Size(1181, 614);
            gridControl1.TabIndex = 0;
            gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView1 });
            // 
            // gridView1
            // 
            gridView1.DetailHeight = 373;
            gridView1.GridControl = gridControl1;
            gridView1.Name = "gridView1";
            // 
            // btnDetay
            // 
            btnDetay.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnDetay.Location = new Point(12, 544);
            btnDetay.Name = "btnDetay";
            btnDetay.Size = new Size(100, 32);
            btnDetay.TabIndex = 1;
            btnDetay.Text = "Detay";
            btnDetay.Click += btnDetay_Click;
            // 
            // btnOnayla
            // 
            btnOnayla.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnOnayla.Location = new Point(118, 544);
            btnOnayla.Name = "btnOnayla";
            btnOnayla.Size = new Size(100, 32);
            btnOnayla.TabIndex = 2;
            btnOnayla.Text = "Onayla";
            btnOnayla.Click += btnOnayla_Click;
            // 
            // btnReddet
            // 
            btnReddet.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnReddet.Location = new Point(224, 544);
            btnReddet.Name = "btnReddet";
            btnReddet.Size = new Size(100, 32);
            btnReddet.TabIndex = 3;
            btnReddet.Text = "Reddet";
            btnReddet.Click += btnReddet_Click;
            // 
            // btnSecilenleriSil
            // 
            btnSecilenleriSil.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSecilenleriSil.Location = new Point(330, 544);
            btnSecilenleriSil.Name = "btnSecilenleriSil";
            btnSecilenleriSil.Size = new Size(120, 32);
            btnSecilenleriSil.TabIndex = 4;
            btnSecilenleriSil.Text = "Seçilenleri Sil";
            btnSecilenleriSil.Click += btnSecilenleriSil_Click;
            // 
            // btnKapat
            // 
            btnKapat.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnKapat.Location = new Point(888, 544);
            btnKapat.Name = "btnKapat";
            btnKapat.Size = new Size(100, 32);
            btnKapat.TabIndex = 5;
            btnKapat.Text = "Kapat";
            btnKapat.Click += btnKapat_Click;
            // 
            // BasvuruListeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 16F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1181, 614);
            Controls.Add(btnKapat);
            Controls.Add(btnSecilenleriSil);
            Controls.Add(btnReddet);
            Controls.Add(btnOnayla);
            Controls.Add(btnDetay);
            Controls.Add(gridControl1);
            FormBorderStyle = FormBorderStyle.Sizable;
            MaximizeBox = true;
            MinimizeBox = true;
            MinimumSize = new Size(800, 500);
            Name = "BasvuruListeForm";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Başvuru Listesi";
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

