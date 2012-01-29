using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
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
                if (database == null)
                {
                    Logger.WriteLine("Database {0} does not exist", databaseName);
                    return;
                }

                var users = server.GetLoginUsers(database);
                var usernames = users.Select(user => user.Name);
                Logger.WriteLine("User mappings to restore: {0}", usernames.StringJoin(",", "None"));

                RelocateFiles(database, restore);
                
                server.RenewConnection();

                restore.PercentComplete += HandlePercentComplete;
                restore.Complete += HandleComplete;
                restore.SqlRestore(server);

                RestoreUserMappings(server, users, databaseName);
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

        private void RestoreUserMappings(Server server, IEnumerable<SqlServerUser> users, string databaseName)
        {
            server.Refresh();
            var database = server.Databases[databaseName];
            database.Refresh();
            if (!users.Any())
            {
                Logger.WriteLine("No user mappings to restore");
                return;
            }
            Logger.WriteLine("Restoring user mappings:");
            foreach (var user in users)
            {
                TryCreate(database, user);
            }
        }

        private void TryCreate(Database database, SqlServerUser user)
        {
            try
            {
                Logger.WriteLine("User: {0}, Roles: {0}", user.Name, user.Roles.StringJoin(",", "None"));
                var newUser = new User(database, user.Name)
                    {
                        Login = user.Login
                    };
                newUser.Create();
                foreach (var role in user.Roles)
                {
                    newUser.AddToRole(role);
                }
            }
            catch (FailedOperationException ex)
            {
                Logger.WriteLine("Adding user {0} failed. Reason: {1} ", user.Name, ex.Message);
            }
        }

        private void RelocateFiles(Database database, Restore restore)
        {
            RelocateLogFiles(database, restore);
            RelocateDataFiles(database, restore);
        }

        private void RelocateDataFiles(Database database, Restore restore)
        {
            for (var ii = 0; ii < database.FileGroups.Count; ii++)
            {
                var group = database.FileGroups[ii];
                Logger.WriteLine("Group: " + @group.Name);
                for (var jj = 0; jj < @group.Files.Count; jj++)
                {
                    var file = @group.Files[jj];
                    Logger.WriteLine("Name: {0}", file.Name);
                    Logger.WriteLine("IsPrimary: {0}", file.IsPrimaryFile);
                    Logger.WriteLine("FileName: {0}", file.FileName);
                    if (file.IsPrimaryFile)
                    {
                        var physicalFilename = string.Format("{0}.mdf", database.Name);
                        var physicalPath = Path.Combine(Settings.DataDirectory, physicalFilename);
                        restore.RelocateFiles.Add(new RelocateFile(file.Name, physicalPath));
                        Logger.WriteLine("Relocating data file [" + file.Name + "] to " + physicalPath);
                    }
                }
            }
        }

        private void RelocateLogFiles(Database database, Restore restore)
        {
            for (var ii = 0; ii < database.LogFiles.Count; ii++)
            {
                var logFile = database.LogFiles[ii];
                Logger.WriteLine("Logfile: {0}", logFile);
                var physicalFilename = string.Format("{0}_log.ldf", database.Name);
                var physicalPath = Path.Combine(Settings.LogDirectory, physicalFilename);
                restore.RelocateFiles.Add(new RelocateFile(logFile.Name, physicalPath));
                Logger.WriteLine("Relocating log file [{0}] to {1}", logFile.Name, physicalPath);
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