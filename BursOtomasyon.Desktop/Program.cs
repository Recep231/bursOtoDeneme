using System;
using System.Windows.Forms;
using DevExpress.LookAndFeel;
using DevExpress.Skins;
using BursOtomasyon.Desktop.Forms;

namespace BursOtomasyon.Desktop
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // Windows Forms ayarları
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                
                // DevExpress başlatma - Demo ile aynı skin ayarı
                SkinManager.EnableFormSkins();
                // WXI skin kullan (Demo ile aynı modern görünüm)
                UserLookAndFeel.Default.SetSkinStyle(SkinSvgPalette.WXI.Default);
                
                Application.Run(new MainForm());
            }
            catch (Exception error)
            {
                string errorDetails = error.Message;
                if (error.InnerException != null)
                {
                    errorDetails += $"\n\nİç Hata: {error.InnerException.Message}";
                    if (error.InnerException.InnerException != null)
                    {
                        errorDetails += $"\n\nDetay: {error.InnerException.InnerException.Message}";
                    }
                }
                
                MessageBox.Show($"Uygulama başlatılırken hata oluştu:\n\n{errorDetails}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

