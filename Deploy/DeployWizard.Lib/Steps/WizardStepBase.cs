using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Lib.Steps
{
    public abstract class WizardStepBase<TView> : IWizardStep<TView> where TView : IStepView
    {
        public WizardModel Model
        {
            get; private set;
        }

        public TView View
        {
            get;private set;
        }

        protected WizardStepBase(WizardModel model, TView view)
        {
            Model = model;
            View = view;
        }
    }
}