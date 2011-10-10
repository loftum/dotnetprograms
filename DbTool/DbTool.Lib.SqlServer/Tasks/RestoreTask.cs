using System.IO;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.SqlServer.Tasks
{
    public class RestoreTask : SqlServerProgressTaskBase, IRestoreTask
    {
        private readonly IDbToolLogger _logger;
        private readonly IDbToolSettings _settings;

        public RestoreTask(IDbToolLogger logger, IDbToolSettings settings)
        {
            _settings = settings;
            _logger = logger;
        }

        public void Restore(BackupParameters parameters)
        {
            var databaseName = parameters.DatabaseName;
            var filePath = GetFullPathFrom(parameters.FilePath);

            _logger.WriteLine("Restoring {0} from file {1}", databaseName, filePath);
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
                        _logger.WriteLine("Logfile: {0}", logFile);
                        var physicalFilename = string.Format("{0}_log.ldf", databaseName);
                        var physicalPath = Path.Combine(_settings.LogDirectory, physicalFilename);
                        restore.RelocateFiles.Add(new RelocateFile(logFile.Name, physicalPath));
                        _logger.WriteLine("Relocating log file [{0}] to {1}", logFile.Name, physicalPath);
                    }

                    for (var ii = 0; ii < database.FileGroups.Count; ii++)
                    {
                        var group = database.FileGroups[ii];
                        _logger.WriteLine("Group: " + group.Name);
                        for (var  jj = 0; jj < group.Files.Count; jj++)
                        {
                            var file = group.Files[jj];
                            _logger.WriteLine("Name: " + file.Name);
                            _logger.WriteLine("IsPrimary: " + file.IsPrimaryFile);
                            _logger.WriteLine("FileName: " + file.FileName);
                            if (file.IsPrimaryFile)
                            {
                                var physicalFilename = string.Format("{0}.mdf", databaseName);
                                var physicalPath = Path.Combine(_settings.DataDirectory, physicalFilename);
                                restore.RelocateFiles.Add(new RelocateFile(file.Name, physicalPath));
                                _logger.WriteLine("Relocating data file [" + file.Name + "] to " + physicalPath);
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
                    _logger.Write("Closing connection...");
                    server.ConnectionContext.Disconnect();
                    _logger.WriteLine("OK");
                }
            }
        }

        private string GetFullPathFrom(string filePath)
        {
            return Path.IsPathRooted(filePath)
                ? filePath
                : Path.Combine(_settings.BackupDirectory, filePath);
        }
    }
}