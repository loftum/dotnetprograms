using Deploy.Lib.Deployment.Profiles;
using Deploy.Lib.FileManagement;
using DeployWizard.Lib.Events.FileSystem;
using DeployWizard.Lib.Models;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Lib.Steps
{
    public class SetUpDestinationStep : WizardStepBase<ISetUpDestinationStepView>
    {
        private readonly IFileSystemManager _fileSystemManager;

        public SetUpDestinationStep(WizardModel model, ISetUpDestinationStepView view, IFileSystemManager fileSystemManager) :
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
            var folder = Model.CurrentProfile.DestinationSettings.Folder;
            if (string.IsNullOrEmpty(folder) || !_fileSystemManager.DirectoryExists(folder))
            {
                throw new WizardStepException("Destination " + folder + " does not exist");
            }
        }

        public override void Prepare()
        {
            if (Model.CurrentProfile.DestinationSettings == null)
            {
                Model.CurrentProfile.DestinationSettings = new DestinationSettings();
            }
            View.Settings = Model.CurrentProfile.DestinationSettings;
        }
    }
}