using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace BursOtomasyon.Desktop.Data
{
    public class DatabaseHelper
    {
        // SQL Server Express için instance adı gereklidir
        // Eğer farklı bir instance kullanıyorsanız (ör: MSSQLSERVER), localhost veya . kullanabilirsiniz
        private static string connectionString = "Server=localhost\\SQLEXPRESS;Database=BursOtomasyonDB;Integrated Security=True;TrustServerCertificate=True;";

        public static string ConnectionString
        {
            get => connectionString;
            set => connectionString = value;
        }

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        public static bool TestConnection()
        {
            try
            {
                using (var connection = GetConnection())
                {
                    connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}

