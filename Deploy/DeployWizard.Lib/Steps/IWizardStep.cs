using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Lib.Steps
{
    public interface IWizardStep<out TView> where TView : IStepView
    {
        WizardModel Model { get; }
        TView View { get; }
    }
}