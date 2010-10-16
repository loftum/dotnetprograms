using Deploy.Lib.Deployment.Profiles;
using Deploy.Lib.FileManagement;
using DeployWizard.Lib.Events.FileSystem;
using DeployWizard.Lib.Models;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Lib.Steps
{
    public class SetUpDeployStatusStep : WizardStepBase<ISetUpDeployStatusStepView>
    {
        private readonly IFileSystemManager _fileSystemManager;

        public SetUpDeployStatusStep(WizardModel model, ISetUpDeployStatusStepView view, IFileSystemManager fileSystemManager) :
            base(model, view)
        {
            _fileSystemManager = fileSystemManager;
            View.CreateDirectory += CreateNewDirectory;
        }

        private void CreateNewDirectory(object sender, PathEventArgs args)
        {
            _fileSystemManager.CreateNewDirectory(args.Path);
            View.ValidateAll();
        }

        protected override void DoValidate()
        {
            var settings = Model.CurrentProfile.DeployStatusSettings;
            if (!settings.Skip)
            {
                var folder = settings.Folder;
                if (string.IsNullOrEmpty(folder) || !_fileSystemManager.DirectoryExists(folder))
                {
                    throw new WizardStepException("Deploy status folder " + folder + " does not exist");
                }
            }
        }

        public override void Prepare()
        {
            if (Model.CurrentProfile.DeployStatusSettings == null)
            {
                Model.CurrentProfile.DeployStatusSettings = new DeployStatusSettings();
            }
            View.Settings = Model.CurrentProfile.DeployStatusSettings;
        }
    }
}
