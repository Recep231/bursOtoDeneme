using Microsoft.Data.SqlClient;

namespace BursOtomasyon.Web.Services
{
    public class BagisService
    {
        private readonly string _connectionString = "Server=localhost\\SQLEXPRESS;Database=BursOtomasyonDB;Integrated Security=True;TrustServerCertificate=True;";

        public int Add(int bagisciId, decimal tutar, string aciklama)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    INSERT INTO Bagislar (BagisciID, Tutar, Aciklama, OdemeYontemi, Durum)
                    VALUES (@BagisciID, @Tutar, @Aciklama, @OdemeYontemi, 'Beklemede');
                    SELECT CAST(SCOPE_IDENTITY() as int);", conn);

                cmd.Parameters.AddWithValue("@BagisciID", bagisciId);
                cmd.Parameters.AddWithValue("@Tutar", tutar);
                cmd.Parameters.AddWithValue("@Aciklama", (object)aciklama ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@OdemeYontemi", "BankaHavalesi");

                return (int)cmd.ExecuteScalar();
            }
        }
    }
}

