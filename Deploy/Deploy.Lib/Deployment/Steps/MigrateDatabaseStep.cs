using System;
using System.Reflection;
using Deploy.Lib.Logging;

namespace Deploy.Lib.Deployment.Steps
{
    public class MigrateDatabaseStep : DeploymentStepBase
    {
        public MigrateDatabaseStep(DeployParameters parameters, ILogger logger) : base(parameters, "Migrate database", logger)
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
            var migrator = new Migrator.Migrator(databaseType, connectionString, migrationAssembly);
            migrator.MigrateToLastVersion();
            Status.AppendDetailsLine("Migration successful.");
            Status.Status = DeploymentStepStatus.Ok;
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