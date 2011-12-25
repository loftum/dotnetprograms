using System.Linq;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.SqlServer.Tasks
{
    public class PopulateContextTask : TaskBase, IPopulateContextTask
    {
        public PopulateContextTask(IDbToolLogger logger, IDbToolSettings settings)
            : base(logger, settings)
        {
        }

        public void PopulateContext()
        {
            PopulateContext(false);
        }

        public void RepopulateContext()
        {
            PopulateContext(true);
        }

        private void PopulateContext(bool overwriteExisting)
        {
            var server = new Server(Settings.DefaultConnection.Host);
            try
            {
                var context = Settings.CurrentContext;
                for (var ii = 0; ii < server.Databases.Count; ii++)
                {
                    var name = server.Databases[ii].Name;
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
            finally
            {
                if (server.ConnectionContext.IsOpen)
                {
                    Logger.Write("Disconnecting...");
                    server.ConnectionContext.Disconnect();
                    Logger.WriteLine("OK");
                }
            }
        }
    }
}