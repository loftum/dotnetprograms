using System.Collections.Generic;
using System.IO;
using System.Linq;
using DbTool.Lib.Exceptions;
using Newtonsoft.Json;

namespace DbTool.Lib.Configuration
{
    public class DbToolSettings : IDbToolSettings
    {
        public string DataDirectory { get; set; }
        public string LogDirectory { get; set; }
        public string BackupDirectory { get; set; }
        public string MigrationPath { get; set; }
        public IList<DbConnection> Connections { get; set; }

        public static DbToolSettings Default
        {
            get
            {
                return new DbToolSettings
                    {
                        DataDirectory = "dataDir",
                        LogDirectory = "logDir",
                        BackupDirectory = "backupDir",
                        MigrationPath = "migrationPath",
                        Connections = new List<DbConnection>
                        {
                            new DbConnection
                            {
                                Name = "default",
                                Host = @".\SQLEXPRESS",
                                ConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=MyDB;Integrated Security=True;MultipleActiveResultSets=True",
                                Default = true
                            }
                        }
                    };
            }
        }

        [JsonIgnore]
        public DbConnection DefaultConnection
        {
            get { return Connections.Where(c => c.Default).FirstOrDefault(); }
        }

        public bool HasConnectionString(string name)
        {
            return Connections.Any(c => c.Name.Equals(name));
        }

        public string GetConnectionString(string name)
        {
            return (from connection in Connections 
                    where connection.Name.Equals(name)
                    select connection.ConnectionString).FirstOrDefault();
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