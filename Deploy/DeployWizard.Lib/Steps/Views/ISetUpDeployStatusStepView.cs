using Deploy.Lib.Deployment.Profiles;

namespace DeployWizard.Lib.Steps.Views
{
    public interface ISetUpDeployStatusStepView : IStepView
    {
        DeployStatusSettings Settings { get; set; }
    }
}
