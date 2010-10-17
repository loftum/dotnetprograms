using Deploy.Lib.Deployment;
using Deploy.Lib.Logging;
using DeployWizard.Lib.Models;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Lib.Steps
{
    public class FinishStep : WizardStepBase<IFinishStepView>
    {
        public FinishStep(WizardModel model, IFinishStepView view) :
            base(model, view)
        {
        }

        public override void Prepare()
        {
            
        }

        protected override void DoValidate()
        {
            var parameters = new DeployParameters(Model.Package, Model.CurrentProfile);
            var deployer = new Deployer(parameters);
            deployer.Logger.InfoMessageLogged += AppendMessage;
            deployer.Logger.ProgressChanged += ReportProgress;
            deployer.Deploy();
        }

        private void ReportProgress(object sender, ProgressEventArgs args)
        {
            View.ReportProgress(args.Current, args.Total);
        }

        private void AppendMessage(object sender, LogMessageEventArgs args)
        {
            View.AppendMessage(args.Message);
        }
    }
}