using System.Collections.Generic;
using System.IO;
using System.Text;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Tasks
{
    public class DatabaseRestorer : TaskBase
    {
        public DatabaseRestorer(IDbToolLogger logger, IDbToolSettings settings)
            : base("restore", "<database> <filepath>", @"MyDatabase C:\mydatabase.bak", logger, settings)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            return args.Count > 2 &&
                !string.IsNullOrWhiteSpace(args[1]) &&
                !string.IsNullOrWhiteSpace(args[2]);
        }

        public override void DoExecute(IList<string> args)
        {
            var databaseName = args[1];
            var filePath = args[2];

            Logger.WriteLine("Restoring " + databaseName + " from file " + filePath);
            var server = new Server("(local)");
            try
            {
                server.ConnectionContext.LoginSecure = true;
                server.ConnectionContext.Connect();
                var restore = new Restore {Database = databaseName, Action = RestoreActionType.Database};
                restore.Devices.AddDevice(filePath, DeviceType.File);
                restore.ReplaceDatabase = true;

                var database = server.Databases[databaseName];
                if (database != null)
                {
                    for (var ii = 0; ii < database.LogFiles.Count; ii++)
                    {
                        var logFile = database.LogFiles[ii];
                        Logger.WriteLine("Logfile: " + logFile);
                        var physicalFilename = new StringBuilder().Append(databaseName).Append("_log.ldf").ToString();
                        var physicalPath = Path.Combine(Settings.LogDirectory, physicalFilename);
                        restore.RelocateFiles.Add(new RelocateFile(logFile.Name, physicalPath));
                        Logger.WriteLine("Relocating log file [" + logFile.Name + "] to " + physicalPath);
                    }

                    for (var ii = 0; ii < database.FileGroups.Count; ii++)
                    {
                        var group = database.FileGroups[ii];
                        Logger.WriteLine("Group: " + group.Name);
                        for (var jj = 0; jj < group.Files.Count; jj++)
                        {
                            var file = group.Files[jj];
                            Logger.WriteLine("Name: " + file.Name);
                            Logger.WriteLine("IsPrimary: " + file.IsPrimaryFile);
                            Logger.WriteLine("FileName: " + file.FileName);
                            if (file.IsPrimaryFile)
                            {
                                var physicalFilename = new StringBuilder().Append(databaseName).Append(".mdf").ToString();
                                var physicalPath = Path.Combine(Settings.DataDirectory, physicalFilename);
                                restore.RelocateFiles.Add(new RelocateFile(file.Name, physicalPath));
                                Logger.WriteLine("Relocating data file [" + file.Name + "] to " + physicalPath);
                            }
                        }
                    }
                    server.ConnectionContext.Disconnect();
                    server.ConnectionContext.Connect();
                }

                restore.PercentComplete += PrintPercentage;
                restore.Complete += TaskComplete;
                restore.SqlRestore(server);
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