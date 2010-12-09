using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Deploy.Lib.FileManagement;
using Deploy.Lib.Logging;

namespace Deploy.Lib.Deployment.Steps
{
    public class MigrateDatabaseStep : DeploymentStepBase
    {
        private readonly IFileSystemManager _fileSystemManager;
        public MigrateDatabaseStep(DeployParameters parameters, IFileSystemManager fileSystemManager, IDeployLogger logger)
            : base(parameters, "Migrate database", logger)
        {
            _fileSystemManager = fileSystemManager;
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
            var migrationFile = GetMigrationFile();
            if (migrationFile == null)
            {
                Status.AppendDetailsLine("Could not find migration file " + Parameters.Profile.MigrateDatabaseSettings.MigrationAssemblyName);
                Status.Status = DeploymentStepStatus.Fail;
                Status.CanProceed = false;
                return;
            }

            var migrationAssembly = Assembly.LoadFrom(migrationFile.FullName);
            
            var migratorLogger = new Migrator.Framework.Loggers.Logger(true, new MigratorToDeployStatusAdapter(Status));
            var migrator = new Migrator.Migrator(databaseType, connectionString, migrationAssembly,true, migratorLogger);
            migrator.MigrateToLastVersion();
            
            AppendMigrationInfo(migrator.AppliedMigrations);
            

            Status.Status = DeploymentStepStatus.Ok;
        }

        private FileInfo GetMigrationFile()
        {
            var migrationAssemblyName = Parameters.Profile.MigrateDatabaseSettings.MigrationAssemblyName;
            var migrationFile = _fileSystemManager.SearchForFile(migrationAssemblyName, Parameters.DestinationFolder);
            if (migrationFile == null)
            {
                migrationFile = _fileSystemManager.SearchForFile(migrationAssemblyName, Parameters.TempDirectoryPath);
            }
            return migrationFile;
        }

        private void AppendMigrationInfo(ICollection<long> appliedMigrations)
        {
            Status.AppendDetailsLine("Migration successful. ");
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