using System.Linq;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.SqlServer.Tasks
{
    public class ListDbTask : TaskBase, IListDbTask
    {
        public ListDbTask(IDbToolLogger logger, IDbToolSettings settings)
            : base(logger, settings)
        {
        }

        public void ListDatabases(bool showAll)
        {
            var server = new Server(Settings.DefaultConnection.Host);
            var databaseNames = Settings.CurrentContext.Databases.Select(d => d.Database);
            try
            {
                if (showAll)
                {
                    Logger.WriteLine("All databases on host {0}", Settings.DefaultConnection.Host);
                }
                else
                {
                    Logger.WriteLine("Databases in context {0}:", Settings.CurrentContext.Name);
                }
                
                for (var ii = 0; ii < server.Databases.Count; ii++)
                {
                    var database = server.Databases[ii];
                    if (showAll || databaseNames.Contains(database.Name))
                    {
                        Logger.WriteLine("Name: " + database.Name);    
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