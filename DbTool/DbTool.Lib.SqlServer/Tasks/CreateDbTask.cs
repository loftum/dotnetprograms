using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.SqlServer.Tasks
{
    public class CreateDbTask : TaskBase, ICreateDbTask
    {
        public CreateDbTask(IDbToolLogger logger, IDbToolSettings settings)
            : base(logger, settings)
        {
        }

        public void Create(string databaseName)
        {
            var server = new Server(Settings.DefaultConnection.Host);
            try
            {
                if (server.Databases.Contains(databaseName))
                {
                    Logger.WriteLine("Database {0} already exists.", databaseName);
                    return;
                }
                Logger.Write("Creating database [{0}] ...", databaseName);
                var database = new Database(server, databaseName);
                database.Create();
                Logger.WriteLine("OK");
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