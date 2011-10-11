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

        public void ListDatabases()
        {
            var server = new Server(Settings.DefaultConnection.Host);
            try
            {
                Logger.WriteLine("Databases:");
                for (var ii = 0; ii < server.Databases.Count; ii++)
                {
                    var database = server.Databases[ii];
                    Logger.WriteLine("Name: " + database.Name);
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