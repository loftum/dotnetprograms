using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.Exceptions;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Serializing;
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
        [JsonIgnore]
        public DbToolContext CurrentContext{ get { return GetCurrentOrFirstContext(); }}
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

        public void SetCurrentContext(string contextName)
        {
            var context = Contexts.FirstOrDefault(c => c.Name.EqualsIgnoreCase(contextName));
            if (context == null)
            {
                throw new UserException(ExceptionType.UnknownContext, contextName);
            }
            CurrentContextName = context.Name;
        }

        public void Addcontext(string contextName)
        {
            if (Contexts.Any(c => c.Name.EqualsIgnoreCase(contextName)))
            {
                throw new UserException(ExceptionType.ContextAlreadyExists, contextName);
            }
            Contexts.Add(DbToolContext.Default(contextName));
        }

        public void DeleteContext(string contextName)
        {
            var context = Contexts.FirstOrDefault(c => c.Name.EqualsIgnoreCase(contextName));
            if (context == null)
            {
                throw new UserException(ExceptionType.UnknownContext, contextName);
            }
            Contexts.Remove(context);
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
            get { return CurrentContext.GetDefaultConnection(); }
        }

        public ConnectionData GetConnection(string name)
        {
            var database = GetDatabase(name);
            return database.GetConnectionData();
        }

        public DbToolDatabase GetDatabase(string name)
        {
            var database = CurrentContext.Databases.Where(d => d.Name.Equals(name)).FirstOrDefault();
            if (database == null)
            {
                throw new UserException(ExceptionType.UnknownDatabase, name);
            }
            return database;
        }

        public override string ToString()
        {
            return DbToolSettingsSerializer.Serialize(this);
        }
    }
}