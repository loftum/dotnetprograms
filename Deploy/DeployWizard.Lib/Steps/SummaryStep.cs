using DeployWizard.Lib.Models;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Lib.Steps
{
    public class SummaryStep : WizardStepBase<ISummaryStepView>
    {
        

        public SummaryStep(WizardModel model, ISummaryStepView view) : base(model, view)
        {
        }

        protected override void DoValidate()
        {
            
        }

        public override void Prepare()
        {
            View.Summary = Model.GetSummary();
        }
    }
}