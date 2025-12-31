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
                
                // DevExpress başlatma
                SkinManager.EnableFormSkins();
                UserLookAndFeel.Default.SetSkinStyle("Office 2019 Colorful");
                
                Application.Run(new MainForm());
            }
            catch (Exception ex)
            {
                string errorDetails = ex.Message;
                if (ex.InnerException != null)
                {
                    errorDetails += $"\n\nİç Hata: {ex.InnerException.Message}";
                    if (ex.InnerException.InnerException != null)
                    {
                        errorDetails += $"\n\nDetay: {ex.InnerException.InnerException.Message}";
                    }
                }
                
                MessageBox.Show($"Uygulama başlatılırken hata oluştu:\n\n{errorDetails}", 
                    "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

