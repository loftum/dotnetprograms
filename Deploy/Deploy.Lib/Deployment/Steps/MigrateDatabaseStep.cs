using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Deploy.Lib.Logging;

namespace Deploy.Lib.Deployment.Steps
{
    public class MigrateDatabaseStep : DeploymentStepBase
    {
        public MigrateDatabaseStep(DeployParameters parameters, IDeployLogger logger) : base(parameters, "Migrate database", logger)
        {
        }

        protected override DeploymentStepStatus DoExecute()
        {
            if (Parameters.Profile.MigrateDatabaseSettings.Skip)
            {
                SetStatusSkipped();
                return Status;
            }
            TryMigrate();
            return Status;
        }

        private void TryMigrate()
        {
            Status.AppendDetailsLine("Migrating database");
            try
            {
                Migrate();
            }
            catch (Exception e)
            {
                HandleException(e);
            }
        }

        private void Migrate()
        {
            var databaseType = Parameters.Profile.MigrateDatabaseSettings.DatabaseType;
            var connectionString = Parameters.Profile.MigrateDatabaseSettings.ConnectionString;
            var migrationAssembly = Assembly.LoadFrom(Parameters.Profile.MigrateDatabaseSettings.MigrationAssemblyPath);
            var migratorLogger = new Migrator.Framework.Loggers.Logger(true, new MigratorToDeployStatusAdapter(Status));
            var migrator = new Migrator.Migrator(databaseType, connectionString, migrationAssembly,true, migratorLogger);
            migrator.MigrateToLastVersion();
            Status.AppendDetailsLine("Migration successful. ");
            AppendMigrationInfo(migrator.AppliedMigrations);
            Status.Status = DeploymentStepStatus.Ok;
        }

        private void AppendMigrationInfo(ICollection<long> appliedMigrations)
        {
            if (appliedMigrations == null || appliedMigrations.Count < 1)
            {
                Status.AppendDetailsLine("No migrations to run.");
                return;
            }
            var lastAppliedMigration = appliedMigrations.Last();
            Status.AppendDetailsLine("Last applied migration version: " + lastAppliedMigration);
        }

        private void HandleException(Exception e)
        {
            Status.AppendDetailsLine("Migration failed");
            Status.AppendDetailsLine(e.ToString());
            Status.Status = DeploymentStepStatus.Fail;
            Status.CanProceed = false;
        }
    }
}