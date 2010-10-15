using Deploy.Lib.Deployment.Profiles;

namespace DeployWizard.Lib.Steps.Views
{
    public interface ISetUpBackupStepView : IStepView
    {
        BackupSettings Settings { get; set; }
    }
}