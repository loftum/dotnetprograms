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
            Migrate();
            return Status;
        }

        private void Migrate()
        {
            var databaseType = Parameters.Profile.MigrateDatabaseSettings.DatabaseType;
            var connectionString = Parameters.Profile.MigrateDatabaseSettings.ConnectionString;
            var migrationAssembly = Assembly.LoadFrom(Parameters.Profile.MigrateDatabaseSettings.MigrationAssemblyPath);

            Status.AppendDetailsLine("Migrating database");
            var migrator = new Migrator.Migrator(databaseType, connectionString, migrationAssembly);
            migrator.MigrateToLastVersion();
        }
    }
}