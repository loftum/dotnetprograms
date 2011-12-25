using System.Collections.Generic;

namespace DbTool.Lib.Configuration
{
    public class DbToolSettingsFactory
    {
        public static DbToolSettings CreateDefault()
        {
            return CreateDefaultSettings()
                .WithContext(DefaultSqlServerContext())
                .WithContext(DefaultMySqlContext());
        }

        private static DbToolSettings CreateDefaultSettings()
        {
            return new DbToolSettings
            {
                WorksheetFile = "worksheet.sql",
                LoadSchema = false,
                MaxResult = 100,
                DataDirectory = "dataDir",
                LogDirectory = "logDir",
                BackupDirectory = "backupDir",
                AssemblyMap = new Dictionary<string, string>
                    {
                        {"mysql", "DbTool.Lib.MySql.dll"},
                        {"sqlserver", "DbTool.Lib.SqlServer.dll"}
                    }
            };
        }

        private static DbToolContext DefaultSqlServerContext()
        {
            return new DbToolContext
                       {
                           Name = "SqlServer",
                           DatabaseType = "sqlserver",
                           Host = "(local)"
                       }
                .WithCredentials(new DbToolCredentials
                                     {
                                         IntegratedSecurity = true,
                                         User = "",
                                         Password = "",
                                     })
                .WithDatabase(new DbToolDatabase
                                    {
                                        Database = "SqlServerDb",
                                        MigrationPath = "SqlServerMigrationPath",
                                    });
        }

        private static DbToolContext DefaultMySqlContext()
        {
            return new DbToolContext
                    {
                        Name = "MySql",
                        DatabaseType = "mysql",
                        Host = @"localhost"

                    }
                .WithCredentials(new DbToolCredentials
                {
                    User = "root",
                    Password = "p455w0rD",
                })
                .WithDatabase(new DbToolDatabase
                {
                    Database = "MySqlDb",
                    MigrationPath = "SqlServerMigrationPath",
                });
        }
    }
}