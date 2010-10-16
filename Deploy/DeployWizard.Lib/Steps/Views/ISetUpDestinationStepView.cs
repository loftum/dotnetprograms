using Deploy.Lib.Deployment.Profiles;

namespace DeployWizard.Lib.Steps.Views
{
    public interface ISetUpDestinationStepView : IStepView
    {
        event CreateDirectoryEvent CreateDirectory;
        DestinationSettings Settings { get; set; }
    }
}