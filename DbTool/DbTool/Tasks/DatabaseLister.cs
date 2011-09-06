using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Tasks
{
    public class DatabaseLister : TaskBase
    {
        public DatabaseLister(IDbToolLogger logger, IDbToolSettings settings)
            : base("list", string.Empty, string.Empty, logger, settings)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            return true;
        }

        public override void DoExecute(IList<string> args)
        {
            var server = new Server(Settings.DefaultConnection.Host);
            try
            {
                Logger.WriteLine("Databases:");
                for (var ii=0; ii<server.Databases.Count; ii++)
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