using Deploy.Lib.Deployment.Profiles;

namespace DeployWizard.Lib.Steps.Views
{
    public interface ISetUpBackupStepView : IStepView
    {
        event CreateDirectoryEvent CreateDirectory;
        BackupSettings Settings { get; set; }
    }
}