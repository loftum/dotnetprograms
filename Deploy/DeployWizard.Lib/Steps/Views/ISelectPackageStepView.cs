using DeployWizard.Lib.Models;

namespace DeployWizard.Lib.Steps.Views
{
    public interface ISelectPackageStepView : IStepView
    {
        WizardModel Model { get; set; }
    }
}