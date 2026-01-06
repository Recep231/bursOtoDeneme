using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using BursOtomasyon.Desktop.Data;
using BursOtomasyon.Desktop.Models;

namespace BursOtomasyon.Desktop.Services
{
    public class BagisciService
    {
        public List<Bagisci> GetAll()
        {
            var list = new List<Bagisci>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT BagisciID, Ad, Soyad, Email, Telefon, TCKimlikNo, Adres, KayitTarihi, Aktif
                    FROM Bagiscilar
                    ORDER BY KayitTarihi DESC", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapBagisci(reader));
                    }
                }
            }
            return list;
        }

        public List<Bagis> GetBagislar()
        {
            var list = new List<Bagis>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT b.*, ba.Ad + ' ' + ba.Soyad as BagisciAdSoyad
                    FROM Bagislar b
                    INNER JOIN Bagiscilar ba ON b.BagisciID = ba.BagisciID
                    ORDER BY b.BagisTarihi DESC", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapBagis(reader));
                    }
                }
            }
            return list;
        }

        public KasaOzeti GetKasaOzeti()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                // Durum kontrolünü case-insensitive ve esnek yap
                var cmd = new SqlCommand(@"
                    SELECT 
                        ISNULL(SUM(Tutar), 0) as ToplamGelenBagis,
                        ISNULL(SUM(CASE WHEN LOWER(Durum) LIKE '%bekle%' THEN Tutar ELSE 0 END), 0) as BeklemedeBagis,
                        ISNULL(SUM(CASE WHEN LOWER(Durum) LIKE '%onayl%' OR LOWER(Durum) LIKE '%onay%' THEN Tutar ELSE 0 END), 0) as OnaylananBagis,
                        MAX(BagisTarihi) as SonGuncellemeTarihi
                    FROM Bagislar", conn);
                
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new KasaOzeti
                        {
                            ToplamGelenBagis = reader["ToplamGelenBagis"] != DBNull.Value ? (decimal)reader["ToplamGelenBagis"] : 0,
                            BeklemedeBagis = reader["BeklemedeBagis"] != DBNull.Value ? (decimal)reader["BeklemedeBagis"] : 0,
                            OnaylananBagis = reader["OnaylananBagis"] != DBNull.Value ? (decimal)reader["OnaylananBagis"] : 0,
                            SonGuncellemeTarihi = reader["SonGuncellemeTarihi"] != DBNull.Value ? (DateTime)reader["SonGuncellemeTarihi"] : DateTime.Now
                        };
                    }
                }
            }
            return new KasaOzeti { SonGuncellemeTarihi = DateTime.Now };
        }

        private Bagisci MapBagisci(SqlDataReader reader)
        {
            return new Bagisci
            {
                BagisciID = reader["BagisciID"] != DBNull.Value ? (int)reader["BagisciID"] : 0,
                Ad = reader["Ad"]?.ToString() ?? string.Empty,
                Soyad = reader["Soyad"]?.ToString() ?? string.Empty,
                Email = reader["Email"]?.ToString() ?? string.Empty,
                Telefon = reader["Telefon"]?.ToString(),
                TCKimlikNo = reader["TCKimlikNo"]?.ToString(),
                Adres = reader["Adres"]?.ToString(),
                KayitTarihi = reader["KayitTarihi"] != DBNull.Value ? (DateTime)reader["KayitTarihi"] : DateTime.Now,
                Aktif = reader["Aktif"] != DBNull.Value && (bool)reader["Aktif"]
            };
        }

        private Bagis MapBagis(SqlDataReader reader)
        {
            return new Bagis
            {
                BagisID = reader["BagisID"] != DBNull.Value ? (int)reader["BagisID"] : 0,
                BagisciID = reader["BagisciID"] != DBNull.Value ? (int)reader["BagisciID"] : 0,
                BagisciAdSoyad = reader["BagisciAdSoyad"]?.ToString() ?? string.Empty,
                Tutar = reader["Tutar"] != DBNull.Value ? (decimal)reader["Tutar"] : 0,
                BagisTarihi = reader["BagisTarihi"] != DBNull.Value ? (DateTime)reader["BagisTarihi"] : DateTime.Now,
                Aciklama = reader["Aciklama"]?.ToString(),
                IBAN = reader["IBAN"]?.ToString(),
                OdemeYontemi = reader["OdemeYontemi"]?.ToString() ?? string.Empty,
                Durum = reader["Durum"]?.ToString() ?? string.Empty
            };
        }

        public int GetToplamBagisciSayisi()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand("SELECT COUNT(*) FROM Bagiscilar WHERE Aktif = 1", conn);
                return (int)cmd.ExecuteScalar();
            }
        }

        public int GetDesteklenenOgrenciSayisi()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    SELECT COUNT(DISTINCT OgrenciID) 
                    FROM Basvurular 
                    WHERE BasvuruDurumu = 'Onaylandı'", conn);
                var result = cmd.ExecuteScalar();
                return result != DBNull.Value ? (int)result : 0;
            }
        }

        public decimal GetBuAyBagis()
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                // Tüm bağışları say (durum fark etmeksizin bu ay için)
                var cmd = new SqlCommand(@"
                    SELECT ISNULL(SUM(Tutar), 0) 
                    FROM Bagislar 
                    WHERE MONTH(BagisTarihi) = MONTH(GETDATE()) 
                    AND YEAR(BagisTarihi) = YEAR(GETDATE())", conn);
                var result = cmd.ExecuteScalar();
                return result != DBNull.Value ? (decimal)result : 0;
            }
        }

        public List<Bagis> GetSonBagislar(int limit = 10)
        {
            var list = new List<Bagis>();
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand($@"
                    SELECT TOP {limit} b.*, ba.Ad + ' ' + ba.Soyad as BagisciAdSoyad
                    FROM Bagislar b
                    INNER JOIN Bagiscilar ba ON b.BagisciID = ba.BagisciID
                    ORDER BY b.BagisTarihi DESC", conn);
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add(MapBagis(reader));
                    }
                }
            }
            return list;
        }

        public BagisciProfil? GetBagisciProfil(int bagisciId)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                // Tüm bağışları say (durum fark etmeksizin)
                var cmd = new SqlCommand(@"
                    SELECT 
                        ba.BagisciID, ba.Ad, ba.Soyad, ba.Email, ba.Telefon, ba.KayitTarihi,
                        ISNULL(SUM(b.Tutar), 0) as ToplamBagis,
                        COUNT(b.BagisID) as BagisSayisi,
                        MAX(b.BagisTarihi) as SonBagisTarihi
                    FROM Bagiscilar ba
                    LEFT JOIN Bagislar b ON ba.BagisciID = b.BagisciID
                    WHERE ba.BagisciID = @BagisciID
                    GROUP BY ba.BagisciID, ba.Ad, ba.Soyad, ba.Email, ba.Telefon, ba.KayitTarihi", conn);
                cmd.Parameters.AddWithValue("@BagisciID", bagisciId);
                
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        return new BagisciProfil
                        {
                            BagisciID = (int)reader["BagisciID"],
                            AdSoyad = $"{reader["Ad"]} {reader["Soyad"]}",
                            Email = reader["Email"]?.ToString() ?? "",
                            Telefon = reader["Telefon"]?.ToString(),
                            KayitTarihi = reader["KayitTarihi"] != DBNull.Value ? (DateTime)reader["KayitTarihi"] : DateTime.Now,
                            ToplamBagis = reader["ToplamBagis"] != DBNull.Value ? (decimal)reader["ToplamBagis"] : 0,
                            BagisSayisi = reader["BagisSayisi"] != DBNull.Value ? (int)reader["BagisSayisi"] : 0,
                            SonBagisTarihi = reader["SonBagisTarihi"] != DBNull.Value ? (DateTime?)reader["SonBagisTarihi"] : null
                        };
                    }
                }
            }
            return null;
        }

        public Dictionary<string, decimal> GetAylikBagislar()
        {
            var result = new Dictionary<string, decimal>();
            
            // Son 6 ayı başlangıçta 0 ile doldur
            for (int i = 5; i >= 0; i--)
            {
                var ay = DateTime.Now.AddMonths(-i).ToString("yyyy-MM");
                result[ay] = 0;
            }
            
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                // Tüm bağışları al (durum fark etmeksizin)
                var cmd = new SqlCommand(@"
                    SELECT 
                        FORMAT(BagisTarihi, 'yyyy-MM') as Ay,
                        SUM(Tutar) as Toplam
                    FROM Bagislar 
                    WHERE BagisTarihi >= DATEADD(MONTH, -6, GETDATE())
                    GROUP BY FORMAT(BagisTarihi, 'yyyy-MM')
                    ORDER BY Ay", conn);
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var ay = reader["Ay"]?.ToString() ?? "";
                        var toplam = reader["Toplam"] != DBNull.Value ? (decimal)reader["Toplam"] : 0;
                        if (!string.IsNullOrEmpty(ay))
                            result[ay] = toplam;
                    }
                }
            }
            return result;
        }

        public void AddBagis(int bagisciId, decimal tutar, string aciklama, string odemeYontemi, bool anonim = false)
        {
            using (var conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                var cmd = new SqlCommand(@"
                    INSERT INTO Bagislar (BagisciID, Tutar, BagisTarihi, Aciklama, OdemeYontemi, Durum)
                    VALUES (@BagisciID, @Tutar, GETDATE(), @Aciklama, @OdemeYontemi, 'Onaylandi')", conn);
                cmd.Parameters.AddWithValue("@BagisciID", bagisciId);
                cmd.Parameters.AddWithValue("@Tutar", tutar);
                cmd.Parameters.AddWithValue("@Aciklama", aciklama ?? "");
                cmd.Parameters.AddWithValue("@OdemeYontemi", odemeYontemi);
                cmd.ExecuteNonQuery();
            }
        }
    }

    public class BagisciProfil
    {
        public int BagisciID { get; set; }
        public string AdSoyad { get; set; } = "";
        public string Email { get; set; } = "";
        public string? Telefon { get; set; }
        public DateTime KayitTarihi { get; set; }
        public decimal ToplamBagis { get; set; }
        public int BagisSayisi { get; set; }
        public DateTime? SonBagisTarihi { get; set; }

        public string Rozet => ToplamBagis >= 50000 ? "🥇 Altın" : 
                               ToplamBagis >= 20000 ? "🥈 Gümüş" : 
                               ToplamBagis >= 5000 ? "🥉 Bronz" : "⭐ Yeni";
    }
}

