using Microsoft.Data.SqlClient;

namespace BursOtomasyon.Web.Services
{
    public class BasvuruService
    {
        private readonly string _connectionString = "Server=localhost\\SQLEXPRESS;Database=BursOtomasyonDB;Integrated Security=True;TrustServerCertificate=True;";

        public bool Add(int ogrenciId)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Open();
                    
                    // Önce öğrencinin var olup olmadığını kontrol et
                    var checkCmd = new SqlCommand("SELECT COUNT(*) FROM Ogrenciler WHERE OgrenciID = @OgrenciID", conn);
                    checkCmd.Parameters.AddWithValue("@OgrenciID", ogrenciId);
                    var ogrenciVarMi = (int)checkCmd.ExecuteScalar() > 0;
                    
                    if (!ogrenciVarMi)
                    {
                        throw new Exception($"Öğrenci ID {ogrenciId} bulunamadı!");
                    }
                    
                    var cmd = new SqlCommand(@"
                        INSERT INTO Basvurular (OgrenciID, BasvuruDurumu, BasvuruTarihi)
                        VALUES (@OgrenciID, 'Beklemede', GETDATE())", conn);
                    cmd.Parameters.AddWithValue("@OgrenciID", ogrenciId);
                    var result = cmd.ExecuteNonQuery() > 0;
                    
                    return result;
                }
            }
            catch (Exception ex)
            {
                // Hata loglama (production'da logger kullanılmalı)
                System.Diagnostics.Debug.WriteLine($"BasvuruService.Add hatası: {ex.Message}");
                throw; // Hatayı yukarı fırlat ki controller yakalayabilsin
            }
        }
    }
}

