using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Commands
{
    public class DatabaseCreator : TaskCommandBase
    {
        public DatabaseCreator(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("create", "<databasename>", "MyDatabase", logger, settings, taskFactory)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            return args.Count > 1 && !string.IsNullOrWhiteSpace(args[1]);
        }

        public override void DoExecute(IList<string> args)
        {
            var databaseNAme = args[1];
            
            var server = new Server(Settings.DefaultConnection.Host);
            try
            {
                if (server.Databases.Contains(databaseNAme))
                {
                    Logger.WriteLine("Database " + databaseNAme + " already exists.");
                    return;
                }
                Logger.Write("Creating database [" + databaseNAme + "] ...");
                var database = new Database(server, databaseNAme);
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