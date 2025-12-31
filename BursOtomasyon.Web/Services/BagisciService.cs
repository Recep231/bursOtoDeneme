using Microsoft.Data.SqlClient;
using BursOtomasyon.Web.Models;

namespace BursOtomasyon.Web.Services
{
    public class BagisciService
    {
        private readonly string _connectionString = "Server=localhost\\SQLEXPRESS;Database=BursOtomasyonDB;Integrated Security=True;TrustServerCertificate=True;";

        public int Add(BagisciModel bagisci)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    INSERT INTO Bagiscilar (Ad, Soyad, Email, Telefon, TCKimlikNo, Adres, Sifre)
                    VALUES (@Ad, @Soyad, @Email, @Telefon, @TCKimlikNo, @Adres, @Sifre);
                    SELECT CAST(SCOPE_IDENTITY() as int);", conn);

                cmd.Parameters.AddWithValue("@Ad", bagisci.Ad);
                cmd.Parameters.AddWithValue("@Soyad", bagisci.Soyad);
                cmd.Parameters.AddWithValue("@Email", bagisci.Email);
                cmd.Parameters.AddWithValue("@Telefon", (object)bagisci.Telefon ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@TCKimlikNo", (object)bagisci.TCKimlikNo ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Adres", (object)bagisci.Adres ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Sifre", bagisci.Sifre); // Gerçek uygulamada hash'lenmeli

                return (int)cmd.ExecuteScalar();
            }
        }

        public BagisciModel? GetByEmail(string email)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT BagisciID, Ad, Soyad, Email, Telefon, TCKimlikNo, Adres, Aktif
                    FROM Bagiscilar 
                    WHERE Email = @Email AND Aktif = 1", conn);
                cmd.Parameters.AddWithValue("@Email", email);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new BagisciModel
                        {
                            BagisciID = (int)reader["BagisciID"],
                            Ad = reader["Ad"]?.ToString() ?? string.Empty,
                            Soyad = reader["Soyad"]?.ToString() ?? string.Empty,
                            Email = reader["Email"]?.ToString() ?? string.Empty,
                            Telefon = reader["Telefon"]?.ToString() ?? string.Empty,
                            TCKimlikNo = reader["TCKimlikNo"]?.ToString(),
                            Adres = reader["Adres"]?.ToString(),
                            Aktif = reader["Aktif"] != DBNull.Value && (bool)reader["Aktif"]
                        };
                    }
                }
            }
            return null;
        }

        public BagisciModel? GirisYap(string email, string sifre)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT BagisciID, Ad, Soyad, Email, Telefon, TCKimlikNo, Adres, Aktif
                    FROM Bagiscilar 
                    WHERE Email = @Email AND Sifre = @Sifre AND Aktif = 1", conn);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@Sifre", sifre); // Gerçek uygulamada hash karşılaştırması yapılmalı

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new BagisciModel
                        {
                            BagisciID = (int)reader["BagisciID"],
                            Ad = reader["Ad"]?.ToString() ?? string.Empty,
                            Soyad = reader["Soyad"]?.ToString() ?? string.Empty,
                            Email = reader["Email"]?.ToString() ?? string.Empty,
                            Telefon = reader["Telefon"]?.ToString() ?? string.Empty,
                            TCKimlikNo = reader["TCKimlikNo"]?.ToString(),
                            Adres = reader["Adres"]?.ToString(),
                            Aktif = reader["Aktif"] != DBNull.Value && (bool)reader["Aktif"]
                        };
                    }
                }
            }
            return null;
        }
    }
}

