using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using DotNetPrograms.Common.ExtensionMethods;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.SqlServer.Tasks
{
    public class PopulateContextTask : TaskBase, IPopulateContextTask
    {
        public PopulateContextTask(IDbToolLogger logger, IDbToolSettings settings)
            : base(logger, settings)
        {
        }

        public void Populate(IEnumerable<string> databases, bool overwriteExisting)
        {
            var existingNames = GetExistingDatabaseNames();
            DoPopulate(databases.Where(n => existingNames.Any(e => e.EqualsIgnoreCase(n))), overwriteExisting);

            foreach (var name in databases.Where(n => !existingNames.Any(e => e.EqualsIgnoreCase(n))))
            {
                Logger.WriteLine("{0} does not exist in database", name);
            }
        }

        public void PopulateAll(bool overwriteExisting)
        {
            DoPopulate(GetExistingDatabaseNames(), overwriteExisting);
        }

        private void DoPopulate(IEnumerable<string> databases, bool overwriteExisting)
        {
            var context = Settings.CurrentContext;
            foreach (var name in databases)
            {
                var contextHasDatabase = context.Databases.Any(d => d.Name.Equals(name));
                var shallAdd = overwriteExisting || !contextHasDatabase;
                if (shallAdd)
                {
                    var database = new DbToolDatabase { Database = name };
                    if (contextHasDatabase)
                    {
                        Logger.WriteLine("Overwriting {0}", name);
                        context.RemoveDatabase(name);
                        context.AddDatabase(database);
                    }
                    else
                    {
                        Logger.WriteLine("Adding {0}", name);
                        context.AddDatabase(database);
                    }
                }
                else
                {
                    Logger.WriteLine("Skipping {0}", name);
                }
            }
        }

        private IEnumerable<string> GetExistingDatabaseNames()
        {
            var databases = new List<string>();
            var server = new Server(Settings.DefaultConnection.Host);
            try
            {
                for (var ii = 0; ii < server.Databases.Count; ii++)
                {
                    var name = server.Databases[ii].Name;
                    databases.Add(name);
                }
            }
            finally
            {
                if (server.ConnectionContext.IsOpen)
                {
                    Logger.Write("Disconnecting...");
                    server.ConnectionContext.Disconnect();
                    Logger.WriteLine("OK");
                }
            }
            return databases;
        }
    }
}