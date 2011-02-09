using System.Text;
using EnvironmentViewer.Lib.Exceptions;
using EnvironmentViewer.Lib.Extensions;

namespace EnvironmentViewer.Lib.SessionFactories
{
    public class DatabaseCredentials
    {
        public string DatabaseType { get; set; }
        public string Host { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IntegratedSecurity { get; set; }
        
        public bool IsValid
        {
            get { return !DatabaseType.IsNullOrWhiteSpace() && DatabaseType.EqualsOneOf("mysql", "sqlserver"); }
        }

        public string BuildConnectionString()
        {
            switch (DatabaseType)
            {
                case "mysql":
                    return CreateForMysql();
                case "sqlserver":
                    return CreateForSqlServer();
                default:
                    throw new UserException(ExceptionType.InvalidDatabaseType, DatabaseType);
            }
        }

        private string CreateForSqlServer()
        {
            var builder = new StringBuilder()
                .Append("Data Source=").Append(Host).Append(";")
                .Append("Initial Catalog=").Append(Database).Append(";");
            if (IntegratedSecurity)
            {
                builder.Append("Integrated Security=True;");
            }
            else
            {
                builder.Append("User Id=").Append(Username).Append(";")
                    .Append("Password=").Append(Password).Append(";");
            }
            return builder.ToString();
        }

        private string CreateForMysql()
        {
            return new StringBuilder()
                .Append("Server=").Append(Host).Append(";")
                .Append("Database=").Append(Database).Append(";")
                .Append("Uid=").Append(Username).Append(";")
                .Append("Pwd=").Append(Password).Append(";")
                .ToString();
        }
    }
}