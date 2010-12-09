using System;
using System.IO;
using System.Linq;
using Deploy.Lib.FileManagement;
using Deploy.Lib.Logging;

namespace Deploy.Lib.Deployment.Steps
{
    public class DeployFilesStep : DeploymentStepBase
    {
        private readonly IFileSystemManager _fileSystemManager;
        private const string GlobalAsaxName = "global.asax";

        public DeployFilesStep(IFileSystemManager fileSystemManager, DeployParameters parameters, IDeployLogger logger) 
            : base(parameters, "Deploy files", logger)
        {
            _fileSystemManager = fileSystemManager;
        }

        protected override DeploymentStepStatus DoExecute()
        {
            if (Parameters.Profile.DestinationSettings.Skip)
            {
                SetStatusSkipped();
                return Status;
            }

            var tempDirectory = new DirectoryInfo(Parameters.TempDirectoryPath);
            var rootDirectory = GetWebRootDirectory(tempDirectory);
            if (rootDirectory == null)
            {
                Status.AppendDetailsLine("Could not find web root directory in " + tempDirectory.FullName);
                Status.Status = DeploymentStepStatus.Fail;
                return Status;
            }
            Status.AppendDetailsLine("Copying contents of " + rootDirectory.FullName + " to " + Parameters.DestinationFolder);
            _fileSystemManager.CopyContentsOf(rootDirectory).To(Parameters.DestinationFolder);

            Status.Status = DeploymentStepStatus.Ok;
            return Status;
        }

        private static DirectoryInfo GetWebRootDirectory(DirectoryInfo directory)
        {
            if (IsWebRootDirectory(directory))
            {
                return directory;
            }
            foreach (var subDirectory in directory.GetDirectories())
            {
                if (IsWebRootDirectory(subDirectory))
                {
                    return subDirectory;
                }
            }
            foreach (var subDirectory in directory.GetDirectories())
            {
                var rootDirectory = GetWebRootDirectory(subDirectory);
                if (rootDirectory != null)
                {
                    return rootDirectory;
                }
            }
            return null;
        }

        private static bool IsWebRootDirectory(DirectoryInfo tempDirectory)
        {
            return tempDirectory
                .GetFiles()
                .Any(file => file.Name.Equals(GlobalAsaxName, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}