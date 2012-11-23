using System;
using System.Collections.Generic;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using DotNetPrograms.Common.ExtensionMethods;

namespace DbTool.Commands
{
    public class MigrateDatabase : TaskCommandBase
    {
        public MigrateDatabase(IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base("migrate", logger, settings, taskFactory)
        {
        }

        protected override IEnumerable<string> GetUsages()
        {
            return "<database> [version]".AsArray();
        }

        protected override IEnumerable<string> GetExamples()
        {
            return new[] {"MyDatabase", "MyDatabase 1234"};
        }

        public override bool AreValid(CommandArgs args)
        {
            return args.HasArguments;
        }

        public override void DoExecute(CommandArgs args)
        {
            var databaseName = args.Arguments[0];
            var versionString = args.AllArguments.Count > 1 ? args.Arguments[1] : string.Empty;
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