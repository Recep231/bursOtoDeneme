using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using BursOtomasyon.Desktop.Data;
using BursOtomasyon.Desktop.Models;

namespace BursOtomasyon.Desktop.Services
{
    public class OgrenciService
    {
        public List<Ogrenci> GetAll()
        {
            var list = new List<Ogrenci>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Ogrenciler ORDER BY KayitTarihi DESC", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapOgrenci(reader));
                    }
                }
            }
            return list;
        }

        public Ogrenci? GetById(int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Ogrenciler WHERE OgrenciID = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapOgrenci(reader);
                }
            }
            return null;
        }

        public Ogrenci? GetByTC(string tc)
        {
            if (string.IsNullOrWhiteSpace(tc))
                return null;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT * FROM Ogrenciler WHERE TC = @TC", conn);
                cmd.Parameters.AddWithValue("@TC", tc.Trim());
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapOgrenci(reader);
                }
            }
            return null;
        }

        public bool TCExists(string tc, int? excludeOgrenciId = null)
        {
            if (string.IsNullOrWhiteSpace(tc))
                return false;

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string sql = "SELECT COUNT(*) FROM Ogrenciler WHERE TC = @TC";
                if (excludeOgrenciId.HasValue)
                {
                    sql += " AND OgrenciID != @OgrenciID";
                }
                
                var cmd = new SqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@TC", tc.Trim());
                if (excludeOgrenciId.HasValue)
                {
                    cmd.Parameters.AddWithValue("@OgrenciID", excludeOgrenciId.Value);
                }
                
                var count = (int)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        public bool Add(Ogrenci ogrenci)
        {
            // TC kontrolü yap
            if (TCExists(ogrenci.TC))
            {
                throw new InvalidOperationException($"Bu TC Kimlik Numarası ({ogrenci.TC}) zaten sistemde kayıtlı!");
            }

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    INSERT INTO Ogrenciler (Ad, Soyad, TC, DogumTarihi, Telefon, Email, Universite, Fakulte, Bolum, Sinif, NotOrtalamasi, KardesSayisi, AileGeliri, AnneMeslek, BabaMeslek, BursPuani, ProfilFotoYolu)
                    VALUES (@Ad, @Soyad, @TC, @DogumTarihi, @Telefon, @Email, @Universite, @Fakulte, @Bolum, @Sinif, @NotOrtalamasi, @KardesSayisi, @AileGeliri, @AnneMeslek, @BabaMeslek, @BursPuani, @ProfilFotoYolu)", conn);

                AddParameters(cmd, ogrenci);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Update(Ogrenci ogrenci)
        {
            // TC kontrolü yap (mevcut öğrenci hariç)
            if (TCExists(ogrenci.TC, ogrenci.OgrenciID))
            {
                throw new InvalidOperationException($"Bu TC Kimlik Numarası ({ogrenci.TC}) başka bir öğrenci tarafından kullanılıyor!");
            }

            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    UPDATE Ogrenciler SET 
                        Ad = @Ad, Soyad = @Soyad, TC = @TC, DogumTarihi = @DogumTarihi, 
                        Telefon = @Telefon, Email = @Email, Universite = @Universite, 
                        Fakulte = @Fakulte, Bolum = @Bolum, Sinif = @Sinif, 
                        NotOrtalamasi = @NotOrtalamasi, KardesSayisi = @KardesSayisi, 
                        AileGeliri = @AileGeliri, AnneMeslek = @AnneMeslek, 
                        BabaMeslek = @BabaMeslek, BursPuani = @BursPuani, ProfilFotoYolu = @ProfilFotoYolu
                    WHERE OgrenciID = @OgrenciID", conn);

                cmd.Parameters.AddWithValue("@OgrenciID", ogrenci.OgrenciID);
                AddParameters(cmd, ogrenci);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool Delete(int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Ogrenciler WHERE OgrenciID = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", id);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private void AddParameters(SqlCommand cmd, Ogrenci ogrenci)
        {
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
        }

        private Ogrenci MapOgrenci(SqlDataReader reader)
        {
            var aileGeliriValue = reader["AileGeliri"];
            var bursPuaniValue = reader["BursPuani"];
            
            return new Ogrenci
            {
                OgrenciID = reader["OgrenciID"] != DBNull.Value ? (int)reader["OgrenciID"] : 0,
                Ad = reader["Ad"]?.ToString() ?? string.Empty,
                Soyad = reader["Soyad"]?.ToString() ?? string.Empty,
                TC = reader["TC"]?.ToString() ?? string.Empty,
                DogumTarihi = reader["DogumTarihi"] != DBNull.Value ? (DateTime)reader["DogumTarihi"] : DateTime.Now,
                Telefon = reader["Telefon"]?.ToString(),
                Email = reader["Email"]?.ToString(),
                Universite = reader["Universite"]?.ToString(),
                Fakulte = reader["Fakulte"]?.ToString(),
                Bolum = reader["Bolum"]?.ToString(),
                Sinif = reader["Sinif"]?.ToString() ?? "1",
                NotOrtalamasi = reader["NotOrtalamasi"] != DBNull.Value ? (decimal)reader["NotOrtalamasi"] : 0,
                KardesSayisi = reader["KardesSayisi"]?.ToString() ?? "0",
                AileGeliri = aileGeliriValue != null && aileGeliriValue != DBNull.Value ? (decimal)aileGeliriValue : 0,
                AnneMeslek = reader["AnneMeslek"]?.ToString(),
                BabaMeslek = reader["BabaMeslek"]?.ToString(),
                BursPuani = bursPuaniValue != null && bursPuaniValue != DBNull.Value ? (decimal)bursPuaniValue : 0,
                KayitTarihi = reader["KayitTarihi"] != DBNull.Value ? (DateTime)reader["KayitTarihi"] : DateTime.Now,
                ProfilFotoYolu = reader["ProfilFotoYolu"]?.ToString(),
                KlasikSoru1Cevap = reader["KlasikSoru1Cevap"]?.ToString(),
                KlasikSoru2Cevap = reader["KlasikSoru2Cevap"]?.ToString(),
                KlasikSoru3Cevap = reader["KlasikSoru3Cevap"]?.ToString()
            };
        }
    }
}

