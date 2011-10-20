using System.Collections.Generic;
using System.IO;
using System.Linq;
using DbTool.Lib.Exceptions;
using Newtonsoft.Json;

namespace DbTool.Lib.Configuration
{
    public class DbToolSettings : IDbToolSettings
    {
        public bool LoadSchema { get; set; }
        public int MaxResult { get; set; }
        public string DataDirectory { get; set; }
        public string LogDirectory { get; set; }
        public string BackupDirectory { get; set; }
        public IList<ConnectionData> Connections { get; set; }
        public IDictionary<string, string> AssemblyMap { get; set; }

        public static DbToolSettings Default
        {
            get
            {
                return new DbToolSettings
                    {
                        LoadSchema = false,
                        MaxResult = 100,
                        DataDirectory = "dataDir",
                        LogDirectory = "logDir",
                        BackupDirectory = "backupDir",
                        AssemblyMap = new Dictionary<string, string>
                        {
                            {"mysql", "DbTool.Lib.MySql.dll"},
                            {"sqlserver", "DbTool.Lib.SqlServer.dll"}
                        },
                        Connections = new List<ConnectionData>
                        {
                            new ConnectionData
                            {
                                Name = "SqlServer",
                                DatabaseType = "sqlserver",
                                Host = @".\SQLEXPRESS",
                                Database = "MyDB",
                                IntegratedSecurity = true,
                                User = "",
                                Password = "",
                                Default = true,
                                MigrationPath = "SqlServerMigrationPath"
                            },
                            new ConnectionData
                            {
                                Name = "MySql",
                                DatabaseType = "mysql",
                                Host = @"localhost",
                                Database = "MyDB",
                                User = "root",
                                Password = "p455w0rD",
                                Default = false,
                                MigrationPath = "MySqlMigrationPath"
                            }
                        }
                    };
            }
        }

        [JsonIgnore]
        public ConnectionData DefaultConnection
        {
            get { return Connections.Where(c => c.Default).FirstOrDefault(); }
        }

        public ConnectionData GetConnection(string name)
        {
            return Connections.Where(c => c.Name.Equals(name)).FirstOrDefault();
        }

        public bool HasConnectionString(string name)
        {
            return Connections.Any(c => c.Name.Equals(name));
        }

        public string GetConnectionString(string name)
        {
            return (from connection in Connections 
                    where connection.Name.Equals(name)
                    select connection.GetConnectionString()).FirstOrDefault();
        }

        public static DbToolSettings From(string path)
        {
            var serialized = Read(path);
            return JsonConvert.DeserializeObject<DbToolSettings>(serialized);
        }

        private static string Read(string path)
        {
            if (!File.Exists(path))
            {
                throw new DbToolException("Missing settings file: " + path);
            }
            using (var reader = new StreamReader(File.OpenRead(path)))
            {
                return reader.ReadToEnd();
            }
        }

        public void Save(string path)
        {
            using (var writer = new StreamWriter(File.Create(path)))
            {
                var serialized = Serialize();
                writer.Write(serialized);
                writer.Flush();
                writer.Close();
            }
        }

        private string Serialize()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        public override string ToString()
        {
            return Serialize();
        }
    }
}