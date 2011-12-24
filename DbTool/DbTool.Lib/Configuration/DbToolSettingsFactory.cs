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
                .WithConnection(new ConnectionData
                                    {
                                        Default = true,
                                        Database = "MyDB",
                                        MigrationPath = "SqlServerMigrationPath",
                                    });
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
                .WithConnection(new ConnectionData
                                    {
                                        Default = true,
                                        Database = "MyDB",
                                        MigrationPath = "SqlServerMigrationPath",
                                    });
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
    }
}