using System;
using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public class MigrateDatabase : TaskCommandBase
    {
        public MigrateDatabase(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("migrate", "<database> [version]", "MyDatabase 1234", logger, settings, taskFactory)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            var count = args.Count;
            return count > 1 && !string.IsNullOrWhiteSpace(args[1]);
        }

        public override void DoExecute(IList<string> args)
        {
            var databaseName = args[1];
            var versionString = args.Count > 2 ? args[2] : string.Empty;
            var database = Settings.GetDatabase(databaseName);
            var migrateDbTask = TaskFactory.CreateMigrateDbTask(database);

            if (string.IsNullOrWhiteSpace(versionString))
            {
                migrateDbTask.MigrateToLatest();
            }
            else
            {
                var version = TryGetVersion(versionString);
                migrateDbTask.MigrateTo(version);
            }
        }

        private static long TryGetVersion(string version)
        {
            try
            {
                return long.Parse(version);
            }
            catch (Exception)
            {
                throw new DbToolException("{0} is not a valid number.", version);
            }
        }
    }
}