namespace DeployWizard.Lib.Steps
{
    public interface IWizardStep<out TView> where TView : IStepView
    {
        TView View { get; }
    }
}