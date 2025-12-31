using System.Data;
using Microsoft.Data.SqlClient;
using BursOtomasyon.Web.Models;

namespace BursOtomasyon.Web.Services
{
    public class OgrenciService
    {
        private readonly string _connectionString = "Server=localhost\\SQLEXPRESS;Database=BursOtomasyonDB;Integrated Security=True;TrustServerCertificate=True;";

        public int Add(OgrenciModel ogrenci)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    INSERT INTO Ogrenciler (Ad, Soyad, TC, DogumTarihi, Telefon, Email, Universite, Fakulte, Bolum, Sinif, NotOrtalamasi, KardesSayisi, AileGeliri, AnneMeslek, BabaMeslek, BursPuani, ProfilFotoYolu, KlasikSoru1Cevap, KlasikSoru2Cevap, KlasikSoru3Cevap)
                    VALUES (@Ad, @Soyad, @TC, @DogumTarihi, @Telefon, @Email, @Universite, @Fakulte, @Bolum, @Sinif, @NotOrtalamasi, @KardesSayisi, @AileGeliri, @AnneMeslek, @BabaMeslek, @BursPuani, @ProfilFotoYolu, @KlasikSoru1Cevap, @KlasikSoru2Cevap, @KlasikSoru3Cevap);
                    SELECT CAST(SCOPE_IDENTITY() as int);", conn);

                cmd.Parameters.AddWithValue("@Ad", ogrenci.Ad);
                cmd.Parameters.AddWithValue("@Soyad", ogrenci.Soyad);
                cmd.Parameters.AddWithValue("@TC", ogrenci.TC);
                cmd.Parameters.AddWithValue("@DogumTarihi", ogrenci.DogumTarihi);
                cmd.Parameters.AddWithValue("@Telefon", (object)ogrenci.Telefon ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", (object)ogrenci.Email ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Universite", (object)ogrenci.Universite ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Fakulte", (object)ogrenci.Fakulte ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Bolum", (object)ogrenci.Bolum ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@Sinif", ogrenci.Sinif);
                cmd.Parameters.AddWithValue("@NotOrtalamasi", ogrenci.NotOrtalamasi);
                cmd.Parameters.AddWithValue("@KardesSayisi", ogrenci.KardesSayisi);
                cmd.Parameters.AddWithValue("@AileGeliri", ogrenci.AileGeliri);
                cmd.Parameters.AddWithValue("@AnneMeslek", (object)ogrenci.AnneMeslek ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@BabaMeslek", (object)ogrenci.BabaMeslek ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@BursPuani", ogrenci.BursPuani);
                cmd.Parameters.AddWithValue("@ProfilFotoYolu", (object)ogrenci.ProfilFotoYolu ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@KlasikSoru1Cevap", (object)ogrenci.KlasikSoru1Cevap ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@KlasikSoru2Cevap", (object)ogrenci.KlasikSoru2Cevap ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@KlasikSoru3Cevap", (object)ogrenci.KlasikSoru3Cevap ?? DBNull.Value);

                return (int)cmd.ExecuteScalar();
            }
        }

        public OgrenciModel? GetById(int ogrenciId)
        {
            using (var conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT OgrenciID, Ad, Soyad, TC, DogumTarihi, Telefon, Email, 
                           Universite, Fakulte, Bolum, Sinif, NotOrtalamasi, 
                           KardesSayisi, AileGeliri, AnneMeslek, BabaMeslek, BursPuani,
                           ProfilFotoYolu, KlasikSoru1Cevap, KlasikSoru2Cevap, KlasikSoru3Cevap
                    FROM Ogrenciler 
                    WHERE OgrenciID = @OgrenciID", conn);
                cmd.Parameters.AddWithValue("@OgrenciID", ogrenciId);

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new OgrenciModel
                        {
                            OgrenciID = (int)reader["OgrenciID"],
                            Ad = reader["Ad"]?.ToString() ?? string.Empty,
                            Soyad = reader["Soyad"]?.ToString() ?? string.Empty,
                            TC = reader["TC"]?.ToString() ?? string.Empty,
                            DogumTarihi = (DateTime)reader["DogumTarihi"],
                            Telefon = reader["Telefon"]?.ToString() ?? string.Empty,
                            Email = reader["Email"]?.ToString() ?? string.Empty,
                            Universite = reader["Universite"]?.ToString() ?? string.Empty,
                            Fakulte = reader["Fakulte"]?.ToString() ?? string.Empty,
                            Bolum = reader["Bolum"]?.ToString() ?? string.Empty,
                            Sinif = reader["Sinif"]?.ToString() ?? "1",
                            NotOrtalamasi = reader["NotOrtalamasi"] != DBNull.Value ? (decimal)reader["NotOrtalamasi"] : 0,
                            KardesSayisi = reader["KardesSayisi"]?.ToString() ?? "0",
                            AileGeliri = reader["AileGeliri"] != DBNull.Value ? (decimal)reader["AileGeliri"] : 0,
                            AnneMeslek = reader["AnneMeslek"]?.ToString() ?? string.Empty,
                            BabaMeslek = reader["BabaMeslek"]?.ToString() ?? string.Empty,
                            BursPuani = reader["BursPuani"] != DBNull.Value ? (decimal)reader["BursPuani"] : 0,
                            ProfilFotoYolu = reader["ProfilFotoYolu"]?.ToString(),
                            KlasikSoru1Cevap = reader["KlasikSoru1Cevap"]?.ToString() ?? string.Empty,
                            KlasikSoru2Cevap = reader["KlasikSoru2Cevap"]?.ToString() ?? string.Empty,
                            KlasikSoru3Cevap = reader["KlasikSoru3Cevap"]?.ToString() ?? string.Empty
                        };
                    }
                }
            }
            return null;
        }
    }
}

