using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.SqlServer.Tasks
{
    public class DeleteDbTask : TaskBase, IDeleteDbTask
    {
        public DeleteDbTask(IDbToolLogger logger, IDbToolSettings settings)
            : base(logger, settings)
        {
        }

        public void Delete(string databaseName)
        {
            var server = new Server(Settings.DefaultConnection.Host);
            try
            {
                server.ConnectionContext.LoginSecure = true;
                server.ConnectionContext.Connect();
                if (!server.Databases.Contains(databaseName))
                {
                    Logger.WriteLine("Did not find database '{0}'", databaseName);
                    return;
                }
                Logger.Write("Dropping database [{0}]...", databaseName);
                server.Databases[databaseName].Drop();
                Logger.WriteLine("OK");
            }
            finally
            {
                if (server.ConnectionContext.IsOpen)
                {
                    Logger.Write("Closing connection...");
                    server.ConnectionContext.Disconnect();
                    Logger.WriteLine("OK");
                }
            }
        }
    }
}