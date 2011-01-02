using Deploy.Lib.Databases;
using Deploy.Lib.Deployment.Profiles;
using DeployWizard.Lib.Events.Connections;
using DeployWizard.Lib.Models;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Lib.Steps
{
    public class SetUpMigrationStep : WizardStepBase<ISetUpMigrationStepView>
    {
        private readonly IDatabaseConnectionTester _connectionTester;

        public SetUpMigrationStep(IDatabaseConnectionTester connectionTester,
            WizardModel model, ISetUpMigrationStepView view) : base(model, view)
        {
            _connectionTester = connectionTester;
            view.TestConnection += HandleTestConnection;
        }

        private void HandleTestConnection(object sender, ConnectionEventArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.ConnectionString))
            {
                View.ShowConnectionStatus("Invalid connection string");
                return;
            }
            TestConnection(args.ConnectionString);
        }

        private void TestConnection(string connectionString)
        {
            View.ShowConnectionStatus(string.Empty);
            try
            {
                _connectionTester.TestConnection(connectionString);
                View.ShowConnectionStatus("Success");
            }
            catch (CouldNotConnectException e)
            {
                View.ShowConnectionStatus("Connection failed:" + e.Message);
            }
        }

        protected override void DoValidate()
        {
            var settings = Model.CurrentProfile.MigrateDatabaseSettings;
            if (!Model.CurrentProfile.MigrateDatabaseSettings.Skip)
            {
                if (string.IsNullOrWhiteSpace(settings.MigrationAssemblyName))
                {
                    throw new WizardStepException("Migration assembly must be set.");
                }
                if (string.IsNullOrWhiteSpace(settings.DatabaseType))
                {
                    throw new WizardStepException("Database type must be set.");
                }
                _connectionTester.TestConnection(settings.ConnectionString);
            }
        }

        public override void Prepare()
        {
            if (Model.CurrentProfile.MigrateDatabaseSettings == null)
            {
                Model.CurrentProfile.MigrateDatabaseSettings = new MigrateDatabaseSettings();
            }
            View.Settings = Model.CurrentProfile.MigrateDatabaseSettings;
            View.ShowConnectionStatus(string.Empty);
        }
    }
}