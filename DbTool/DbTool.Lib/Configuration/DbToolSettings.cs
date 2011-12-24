using System.Collections.Generic;
using System.IO;
using System.Linq;
using DbTool.Lib.Exceptions;
using DbTool.Lib.ExtensionMethods;
using Newtonsoft.Json;

namespace DbTool.Lib.Configuration
{
    public class DbToolSettings : IDbToolSettings
    {
        public string WorksheetFile { get; set; }
        public bool LoadSchema { get; set; }
        public int MaxResult { get; set; }
        public string DataDirectory { get; set; }
        public string LogDirectory { get; set; }
        public string BackupDirectory { get; set; }
        public string CurrentContextName { get; set; }
        public IList<DbToolContext> Contexts { get; set; }
        public IDictionary<string, string> AssemblyMap { get; set; }

        public DbToolSettings()
        {
            Contexts = new List<DbToolContext>();
            AssemblyMap = new Dictionary<string, string>();
        }

        public static DbToolSettings Default
        {
            get { return DbToolSettingsFactory.CreateDefault(); }
        }
        
        public void AddContext(DbToolContext context)
        {
            Contexts.Add(context);
        }

        public DbToolSettings WithContext(DbToolContext context)
        {
            AddContext(context);
            return this;
        }

        [JsonIgnore]
        public DbToolContext CurrentContext
        {
            get { return GetCurrentOrFirstContext(); }
        }

        private DbToolContext GetCurrentOrFirstContext()
        {
            if (Contexts.IsNullOrEmpty())
            {
                throw new UserException(ExceptionType.NoContextExists);
            }
            var currentContext = Contexts.Where(c => c.Name.Equals(CurrentContextName)).FirstOrDefault();
            if (currentContext == null)
            {
                currentContext = Contexts.First();
                CurrentContextName = currentContext.Name;
            }
            return currentContext;
        }

        [JsonIgnore]
        public ConnectionData DefaultConnection
        {
            get { return CurrentContext.Connections.Where(c => c.Default).FirstOrDefault(); }
        }

        public ConnectionData GetConnection(string name)
        {
            return CurrentContext.Connections.Where(c => c.Name.Equals(name)).FirstOrDefault();
        }

        public bool HasConnectionString(string name)
        {
            return CurrentContext.Connections.Any(c => c.Name.Equals(name));
        }

        public string GetConnectionString(string name)
        {
            return (from connection in CurrentContext.Connections 
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