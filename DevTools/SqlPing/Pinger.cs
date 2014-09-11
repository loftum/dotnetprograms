using System;
using System.Data.SqlClient;

namespace SqlPing
{
    public class Pinger
    {
        public static void Ping(string thing)
        {
            try
            {
                var connectionString = GetConnectionString(thing);
                Console.WriteLine("Ping '{0}'", connectionString);
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                if (ex.Message.StartsWith("Login failed for user"))
                {
                    return;
                }
                throw;
            }
        }

        private static string GetConnectionString(string connectionStringOrServer)
        {
            return LooksLikeConnectionString(connectionStringOrServer)
                ? connectionStringOrServer
                : ConnectionStringForServer(connectionStringOrServer, 1);
        }

        private static string ConnectionStringForServer(string server, int timeout)
        {
            return string.Format("Data Source={0}; User Id=stupid;Password=user; Connection Timeout={1}", server, timeout);
        }

        private static bool LooksLikeConnectionString(string text)
        {
            var lower = text.ToLowerInvariant().Trim();
            return lower.Contains("server=") || lower.Contains("data source=");
        }
    }
}