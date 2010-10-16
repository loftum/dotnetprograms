using Deploy.Lib.Deployment.Profiles;
using Deploy.Lib.FileManagement;
using DeployWizard.Lib.Models;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Lib.Steps
{
    public class SetUpGenerateWebConfigStep : WizardStepBase<IGenerateWebConfigStepView>
    {
        private readonly IFileSystemManager _fileSystemManager;

        public SetUpGenerateWebConfigStep(WizardModel model, IGenerateWebConfigStepView view, IFileSystemManager fileSystemManager) : base(model, view)
        {
            _fileSystemManager = fileSystemManager;
        }

        public override void Prepare()
        {
            if (Model.CurrentProfile.WebConfigSettings == null)
            {
                Model.CurrentProfile.WebConfigSettings = new WebConfigSettings();
            }
            View.Settings = Model.CurrentProfile.WebConfigSettings;
        }

        protected override void DoValidate()
        {
            var webConfigPath = Model.CurrentProfile.WebConfigSettings.NewWebConfigPath;
            if (string.IsNullOrEmpty(webConfigPath) || !_fileSystemManager.FileExists(webConfigPath))
            {
                throw new WizardStepException(webConfigPath + " does not exist");
            }
        }
    }
}