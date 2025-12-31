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
                var cmd = new SqlCommand(@"
                    SELECT 
                        ISNULL(SUM(Tutar), 0) as ToplamGelenBagis,
                        ISNULL(SUM(CASE WHEN Durum = 'Beklemede' THEN Tutar ELSE 0 END), 0) as BeklemedeBagis,
                        ISNULL(SUM(CASE WHEN Durum = 'Onaylandi' THEN Tutar ELSE 0 END), 0) as OnaylananBagis,
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
    }
}

