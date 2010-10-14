using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Lib.Steps
{
    public class SelectPathsStep : WizardStepBase<ISelectPathsStepView>
    {
        public SelectPathsStep(WizardModel model, ISelectPathsStepView view) : base(model, view)
        {
        }
    }
}
