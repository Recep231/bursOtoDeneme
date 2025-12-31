using System;
using System.IO;
using System.Linq;
using System.Drawing;

namespace BursOtomasyon.Desktop.Helpers
{
    public static class ImageHelper
    {
        public static Image? LoadProfilFoto(string? profilFotoYolu)
        {
            if (string.IsNullOrEmpty(profilFotoYolu))
            {
                return null;
            }

            try
            {
                // Web'den gelen yol /profile-photos/... şeklinde olabilir
                var currentDir = Directory.GetCurrentDirectory();
                var solutionRoot = currentDir;
                
                // Solution root'u bul (BursOtomasyoon klasörü)
                while (!string.IsNullOrEmpty(solutionRoot) && !Directory.GetFiles(solutionRoot, "*.sln").Any())
                {
                    var parent = Directory.GetParent(solutionRoot);
                    if (parent == null) break;
                    solutionRoot = parent.FullName;
                }
                
                // Web projesinin wwwroot klasörünü bul
                var webRootPath = Path.Combine(solutionRoot, "BursOtomasyon.Web", "wwwroot");
                var relativePath = profilFotoYolu.TrimStart('/');
                var fullPath = Path.Combine(webRootPath, relativePath);

                if (File.Exists(fullPath))
                {
                    return Image.FromFile(fullPath);
                }
                
                // Alternatif: Mevcut dizinden dene
                var altPath = Path.Combine(currentDir, "wwwroot", relativePath);
                if (File.Exists(altPath))
                {
                    return Image.FromFile(altPath);
                }
                
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}

