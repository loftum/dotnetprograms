using Deploy.Lib.FileManagement;
using DeployWizard.Lib.Models;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Lib.Steps
{
    public class SelectPackageStep : WizardStepBase<ISelectPackageStepView>
    {
        private readonly IFileSystemManager _fileSystemManager;

        public SelectPackageStep(WizardModel model, ISelectPackageStepView view, IFileSystemManager fileSystemManager) : base(model, view)
        {
            _fileSystemManager = fileSystemManager;
        }

        protected override void DoValidate()
        {
            var packagePath = Model.Package;
            ValidateFileExists(packagePath);
        }

        private void ValidateFileExists(string path)
        {
            if (!_fileSystemManager.FileExists(path))
            {
                throw new WizardStepException(path + " does not exist");
            }
        }

        public override void Prepare()
        {
            View.Model = Model;
            View.ValidateAll();
        }
    }
}