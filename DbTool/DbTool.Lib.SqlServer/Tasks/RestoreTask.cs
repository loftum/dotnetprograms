using System.IO;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.SqlServer.Tasks
{
    public class RestoreTask : SqlServerProgressTaskBase, IRestoreTask
    {
        public RestoreTask(IDbToolLogger logger, IDbToolSettings settings)
            : base(logger, settings)
        {
        }

        public void Restore(BackupParameters parameters)
        {
            var databaseName = parameters.DatabaseName;
            var filePath = GetFullPathFrom(parameters.FilePath);

            Logger.WriteLine("Restoring {0} from file {1}", databaseName, filePath);
            var server = new Server(parameters.Server);
            try
            {
                server.ConnectionContext.LoginSecure = true;
                server.ConnectionContext.Connect();
                var restore = new Restore { Database = databaseName, Action = RestoreActionType.Database };
                restore.Devices.AddDevice(filePath, DeviceType.File);
                restore.ReplaceDatabase = true;

                var database = server.Databases[databaseName];
                if (database != null)
                {
                    for (var ii = 0; ii < database.LogFiles.Count; ii++)
                    {
                        var logFile = database.LogFiles[ii];
                        Logger.WriteLine("Logfile: {0}", logFile);
                        var physicalFilename = string.Format("{0}_log.ldf", databaseName);
                        var physicalPath = Path.Combine(Settings.LogDirectory, physicalFilename);
                        restore.RelocateFiles.Add(new RelocateFile(logFile.Name, physicalPath));
                        Logger.WriteLine("Relocating log file [{0}] to {1}", logFile.Name, physicalPath);
                    }

                    for (var ii = 0; ii < database.FileGroups.Count; ii++)
                    {
                        var group = database.FileGroups[ii];
                        Logger.WriteLine("Group: " + group.Name);
                        for (var  jj = 0; jj < group.Files.Count; jj++)
                        {
                            var file = group.Files[jj];
                            Logger.WriteLine("Name: " + file.Name);
                            Logger.WriteLine("IsPrimary: " + file.IsPrimaryFile);
                            Logger.WriteLine("FileName: " + file.FileName);
                            if (file.IsPrimaryFile)
                            {
                                var physicalFilename = string.Format("{0}.mdf", databaseName);
                                var physicalPath = Path.Combine(Settings.DataDirectory, physicalFilename);
                                restore.RelocateFiles.Add(new RelocateFile(file.Name, physicalPath));
                                Logger.WriteLine("Relocating data file [" + file.Name + "] to " + physicalPath);
                            }
                        }
                    }
                    server.ConnectionContext.Disconnect();
                    server.ConnectionContext.Connect();
                }

                restore.PercentComplete += HandlePercentComplete;
                restore.Complete += HandleComplete;
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

        private string GetFullPathFrom(string filePath)
        {
            return Path.IsPathRooted(filePath)
                ? filePath
                : Path.Combine(Settings.BackupDirectory, filePath);
        }
    }
}