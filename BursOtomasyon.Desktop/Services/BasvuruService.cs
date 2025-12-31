using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BursOtomasyon.Desktop.Data;
using BursOtomasyon.Desktop.Models;

namespace BursOtomasyon.Desktop.Services
{
    public class BasvuruService
    {
        public List<Basvuru> GetAll()
        {
            var list = new List<Basvuru>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT b.*, 
                           o.Ad + ' ' + o.Soyad as OgrenciAdSoyad, 
                           o.TC as OgrenciTC,
                           o.Email as OgrenciEmail,
                           o.Telefon as OgrenciTelefon,
                           o.Universite as OgrenciUniversite,
                           o.Bolum as OgrenciBolum,
                           o.Sinif as OgrenciSinif,
                           o.NotOrtalamasi as OgrenciNotOrtalamasi,
                           o.BursPuani as OgrenciBursPuani
                    FROM Basvurular b
                    INNER JOIN Ogrenciler o ON b.OgrenciID = o.OgrenciID
                    ORDER BY b.BasvuruTarihi DESC", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapBasvuru(reader));
                    }
                }
            }
            return list;
        }

        public Basvuru? GetById(int id)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT b.*, 
                           o.Ad + ' ' + o.Soyad as OgrenciAdSoyad, 
                           o.TC as OgrenciTC,
                           o.Email as OgrenciEmail,
                           o.Telefon as OgrenciTelefon,
                           o.Universite as OgrenciUniversite,
                           o.Bolum as OgrenciBolum,
                           o.Sinif as OgrenciSinif,
                           o.NotOrtalamasi as OgrenciNotOrtalamasi,
                           o.BursPuani as OgrenciBursPuani
                    FROM Basvurular b
                    INNER JOIN Ogrenciler o ON b.OgrenciID = o.OgrenciID
                    WHERE b.BasvuruID = @ID", conn);
                cmd.Parameters.AddWithValue("@ID", id);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                        return MapBasvuru(reader);
                }
            }
            return null;
        }

        public bool Add(int ogrenciId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    INSERT INTO Basvurular (OgrenciID, BasvuruDurumu)
                    VALUES (@OgrenciID, 'Beklemede')", conn);
                cmd.Parameters.AddWithValue("@OgrenciID", ogrenciId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateDurum(int basvuruId, string durum, int? adminId = null)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    UPDATE Basvurular SET 
                        BasvuruDurumu = @Durum,
                        OnayTarihi = @OnayTarihi,
                        OnaylayanAdminID = @AdminID
                    WHERE BasvuruID = @BasvuruID", conn);
                cmd.Parameters.AddWithValue("@BasvuruID", basvuruId);
                cmd.Parameters.AddWithValue("@Durum", durum);
                cmd.Parameters.AddWithValue("@OnayTarihi", DateTime.Now);
                cmd.Parameters.AddWithValue("@AdminID", (object)adminId ?? DBNull.Value);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool UpdateAIYorum(int basvuruId, string aiYorum)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    UPDATE Basvurular SET AIYorum = @AIYorum
                    WHERE BasvuruID = @BasvuruID", conn);
                cmd.Parameters.AddWithValue("@BasvuruID", basvuruId);
                cmd.Parameters.AddWithValue("@AIYorum", aiYorum);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public bool DeleteByOgrenciId(int ogrenciId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Basvurular WHERE OgrenciID = @OgrenciID", conn);
                cmd.Parameters.AddWithValue("@OgrenciID", ogrenciId);
                return cmd.ExecuteNonQuery() >= 0; // >= 0 çünkü başvuru olmayabilir
            }
        }

        public bool Delete(int basvuruId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("DELETE FROM Basvurular WHERE BasvuruID = @BasvuruID", conn);
                cmd.Parameters.AddWithValue("@BasvuruID", basvuruId);
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        private Basvuru MapBasvuru(SqlDataReader reader)
        {
            var onayTarihiValue = reader["OnayTarihi"];
            var adminIdValue = reader["OnaylayanAdminID"];
            
            return new Basvuru
            {
                BasvuruID = reader["BasvuruID"] != DBNull.Value ? (int)reader["BasvuruID"] : 0,
                OgrenciID = reader["OgrenciID"] != DBNull.Value ? (int)reader["OgrenciID"] : 0,
                BasvuruDurumu = reader["BasvuruDurumu"]?.ToString() ?? "Beklemede",
                BasvuruTarihi = reader["BasvuruTarihi"] != DBNull.Value ? (DateTime)reader["BasvuruTarihi"] : DateTime.Now,
                AIYorum = reader["AIYorum"]?.ToString(),
                OnayTarihi = onayTarihiValue != null && onayTarihiValue != DBNull.Value ? (DateTime?)onayTarihiValue : null,
                OnaylayanAdminID = adminIdValue != null && adminIdValue != DBNull.Value ? (int?)adminIdValue : null,
                OgrenciAdSoyad = reader["OgrenciAdSoyad"]?.ToString() ?? string.Empty,
                OgrenciTC = reader["OgrenciTC"]?.ToString() ?? string.Empty,
                OgrenciEmail = reader["OgrenciEmail"]?.ToString(),
                OgrenciTelefon = reader["OgrenciTelefon"]?.ToString(),
                OgrenciUniversite = reader["OgrenciUniversite"]?.ToString(),
                OgrenciBolum = reader["OgrenciBolum"]?.ToString(),
                OgrenciSinif = reader["OgrenciSinif"]?.ToString(),
                OgrenciNotOrtalamasi = reader["OgrenciNotOrtalamasi"] != DBNull.Value ? (decimal)reader["OgrenciNotOrtalamasi"] : 0,
                OgrenciBursPuani = reader["OgrenciBursPuani"] != DBNull.Value ? (decimal)reader["OgrenciBursPuani"] : 0
            };
        }
    }
}

