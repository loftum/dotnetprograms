using Deploy.Lib.Deployment.Profiles;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Lib.Steps
{
    public class SetUpBackupStep : WizardStepBase<ISetUpBackupStepView>
    {
        public SetUpBackupStep(WizardModel model, ISetUpBackupStepView view) : base(model, view)
        {
        }

        protected override void DoValidate()
        {
            

        }

        public override void Prepare()
        {
            if (Model.CurrentProfile.BackupSettings == null)
            {
                Model.CurrentProfile.BackupSettings = new BackupSettings();
            }

        }
    }
}