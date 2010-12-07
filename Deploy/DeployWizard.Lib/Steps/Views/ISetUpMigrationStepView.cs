using Deploy.Lib.Deployment.Profiles;

namespace DeployWizard.Lib.Steps.Views
{
    public interface ISetUpMigrationStepView : IStepView
    {
        event TestConnectionEvent TestConnection;
        MigrateDatabaseSettings Settings { get; set; }
        void ShowConnectionStatus(string status);
    }
}