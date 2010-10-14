namespace DeployWizard.Lib.Steps.Views
{
    public interface ISelectPackageStepView : IStepView
    {
        string PackagePath { get; }
    }
}