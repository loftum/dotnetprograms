using Deploy.Lib.Deployment.Profiles;
using DeployWizard.Lib.Steps.Views;
using DeployWizard.Lib.SummaryFormatting;

namespace DeployWizard.Lib.Steps
{
    public class SummaryStep : WizardStepBase<ISummaryStepView>
    {
        private readonly ISummaryFormatter<DeploymentProfile> _formatter;

        public SummaryStep(WizardModel model, ISummaryStepView view, ISummaryFormatter<DeploymentProfile> formatter) : base(model, view)
        {
            _formatter = formatter;
        }

        protected override void DoValidate()
        {
            
        }

        public override void Prepare()
        {
            View.Summary = _formatter.Format(Model.CurrentProfile);
        }
    }
}