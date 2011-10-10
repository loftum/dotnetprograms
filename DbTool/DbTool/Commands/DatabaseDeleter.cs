using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Commands
{
    public class DatabaseDeleter : TaskCommandBase
    {
        public DatabaseDeleter(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("delete", "<database>", "MyDatabase", logger, settings, taskFactory)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            return args.Count > 1 && !string.IsNullOrWhiteSpace(args[1]);
        }

        public override void DoExecute(IList<string> args)
        {
            var databaseName = args[1];

            var server = new Server(Settings.DefaultConnection.Host);
            try
            {
                server.ConnectionContext.LoginSecure = true;
                server.ConnectionContext.Connect();
                if (!server.Databases.Contains(databaseName))
                {
                    Logger.WriteLine("Did not find database '" + databaseName + "'");
                    return;
                }
                Logger.Write("Dropping database [" + databaseName + "]...");
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