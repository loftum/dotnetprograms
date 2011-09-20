using System.IO;
using System.Text;
using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.Tasks
{
    public class RestoreTask
    {
        public event PercentCompleteEventHandler PercentComplete;
        public event ServerMessageEventHandler Complete;

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

            _logger.WriteLine("Restoring " + databaseName + " from file " + filePath);
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
                        _logger.WriteLine("Logfile: " + logFile);
                        var physicalFilename = new StringBuilder().Append(databaseName).Append("_log.ldf").ToString();
                        var physicalPath = Path.Combine(_settings.LogDirectory, physicalFilename);
                        restore.RelocateFiles.Add(new RelocateFile(logFile.Name, physicalPath));
                        _logger.WriteLine("Relocating log file [" + logFile.Name + "] to " + physicalPath);
                    }

                    for (var ii = 0; ii < database.FileGroups.Count; ii++)
                    {
                        var group = database.FileGroups[ii];
                        _logger.WriteLine("Group: " + group.Name);
                        for (var jj = 0; jj < group.Files.Count; jj++)
                        {
                            var file = group.Files[jj];
                            _logger.WriteLine("Name: " + file.Name);
                            _logger.WriteLine("IsPrimary: " + file.IsPrimaryFile);
                            _logger.WriteLine("FileName: " + file.FileName);
                            if (file.IsPrimaryFile)
                            {
                                var physicalFilename = new StringBuilder().Append(databaseName).Append(".mdf").ToString();
                                var physicalPath = Path.Combine(_settings.DataDirectory, physicalFilename);
                                restore.RelocateFiles.Add(new RelocateFile(file.Name, physicalPath));
                                _logger.WriteLine("Relocating data file [" + file.Name + "] to " + physicalPath);
                            }
                        }
                    }
                    server.ConnectionContext.Disconnect();
                    server.ConnectionContext.Connect();
                }

                restore.PercentComplete += PercentComplete;
                restore.Complete += Complete;
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