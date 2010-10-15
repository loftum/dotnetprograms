using Deploy.Lib.Deployment.Profiles;

namespace DeployWizard.Lib.Steps.Views
{
    public interface ISetUpDeployStatusStepView : IStepView
    {
        event CreateDirectoryEvent CreateDirectory;
        DeployStatusSettings Settings { get; set; }
    }
}
