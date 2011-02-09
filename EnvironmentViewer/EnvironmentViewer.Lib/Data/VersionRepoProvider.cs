using System;
using System.Data.SqlClient;
using EnvironmentViewer.Lib.SessionFactories;
using MySql.Data.MySqlClient;

namespace EnvironmentViewer.Lib.Data
{
    public class VersionRepoProvider : IVersionRepoProvider
    {
        private readonly IVersionSessionFactoryProvider _sessionFactoryProvider;

        public VersionRepoProvider(IVersionSessionFactoryProvider sessionFactoryProvider)
        {
            _sessionFactoryProvider = sessionFactoryProvider;
        }

        public IVersionRepo GetVersionRepo(DatabaseCredentials credentials)
        {
            return new VersionRepo(_sessionFactoryProvider.Create(credentials));
        }

        public string TestConnection(DatabaseCredentials credentials)
        {
            if (!credentials.IsValid)
            {
                return "Invalid credentials";
            }
            switch(credentials.DatabaseType)
            {
                case "sqlserver":
                    return TestSqlServerConnection(credentials);
                case "mysql":
                    return TestMySql(credentials);
                default:
                    return "Invalid databasetype";
            }
        }

        private static string TestMySql(DatabaseCredentials credentials)
        {
            try
            {
                using (var connection = new MySqlConnection(credentials.BuildConnectionString()))
                {
                    connection.Open();
                    connection.Close();
                    return "OK";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        private static string TestSqlServerConnection(DatabaseCredentials credentials)
        {
            try
            {
                using (var connection = new SqlConnection(credentials.BuildConnectionString()))
                {
                    connection.Open();
                    connection.Close();
                    return "OK";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}