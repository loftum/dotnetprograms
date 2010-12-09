using System.IO;
using Deploy.Lib.FileManagement;
using Deploy.Lib.Logging;

namespace Deploy.Lib.Deployment.Steps
{
    public class CleanUpStep : DeploymentStepBase
    {
        private readonly IFileSystemManager _fileSystemManager;

        public CleanUpStep(IFileSystemManager fileSystemManager, DeployParameters parameters, IDeployLogger logger)
            : base(parameters, "Cleanup", logger)
        {
            _fileSystemManager = fileSystemManager;
        }

        protected override DeploymentStepStatus DoExecute()
        {
            if (_fileSystemManager.DirectoryExists(Parameters.TempDirectoryPath))
            {
                var tempDirectory = new DirectoryInfo(Parameters.TempDirectoryPath);
                Status.AppendDetailsLine("Removing tempdir " + tempDirectory.FullName);
                _fileSystemManager.DeleteDirectory(tempDirectory);    
            }
            else
            {
                Status.AppendDetailsLine(Parameters.TempDirectoryPath + " does not exitst. Nothing to remove.");
            }
            return Status;
        }
    }
}