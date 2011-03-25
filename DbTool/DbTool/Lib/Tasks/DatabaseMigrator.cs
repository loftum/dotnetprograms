using System;
using System.Collections.Generic;
using System.Reflection;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DbTool.Lib.Logging;
using Migrator.Framework.Loggers;

namespace DbTool.Lib.Tasks
{
    public class DatabaseMigrator : TaskBase
    {
        public DatabaseMigrator(IDbToolLogger logger, IDbToolSettings settings)
            : base("migrate", "<database> [version]", "MyDatabase 1234", logger, settings)
        {
        }

        public override bool AreValid(IList<string> args)
        {
            var count = args.Count;
            return count > 1 &&
                   !string.IsNullOrWhiteSpace(args[1]) &&
                   (count < 2 || !string.IsNullOrWhiteSpace(args[2]));
        }

        public override void DoExecute(IList<string> args)
        {
            var databaseName = args[1];
            var versionString = args.Count > 2 ? args[2] : string.Empty;

            if (!Settings.HasConnectionString(databaseName))
            {
                throw new DbToolException("No connection for " + databaseName + " is defined.");
            }
            var connectionString = Settings.GetConnectionString(databaseName);
            var assembly = Assembly.LoadFrom(Settings.MigrationPath);
            var migrator = new Migrator.Migrator("sqlserver", connectionString, assembly, true, new Logger(true, Logger));

            if (string.IsNullOrWhiteSpace(versionString))
            {
                migrator.MigrateToLastVersion();
            }
            else
            {
                var version = TryGetVersion(versionString);
                migrator.MigrateTo(version);
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
                throw new DbToolException(version + " is not a valid number.");
            }
        }
    }
}