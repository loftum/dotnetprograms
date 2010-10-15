using Deploy.Lib.Deployment.Profiles;

namespace DeployWizard.Lib.Steps.Views
{
    public interface IGenerateWebConfigStepView : IStepView
    {
        WebConfigSettings Settings { get; set; }
    }
}