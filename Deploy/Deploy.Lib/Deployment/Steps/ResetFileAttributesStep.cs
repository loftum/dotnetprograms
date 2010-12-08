using System.IO;
using Deploy.Lib.Logging;

namespace Deploy.Lib.Deployment.Steps
{
    public class ResetFileAttributesStep : DeploymentStepBase
    {
        public ResetFileAttributesStep(DeployParameters parameters, IDeployLogger logger) : 
            base(parameters, "Reset file attributes", logger)
        {
        }

        protected override DeploymentStepStatus DoExecute()
        {
            SetNormalAttributes(new DirectoryInfo(Parameters.DestinationFolder));
            Status.Status = DeploymentStepStatus.Ok;
            return Status;
        }

        private void SetNormalAttributes(DirectoryInfo directory)
        {
            Status.AppendDetailsLine("Applying normal attributes to directory " + directory.FullName);
            directory.Attributes = FileAttributes.Normal;
            foreach(var file in directory.GetFiles())
            {
                Status.AppendDetailsLine("Applying normal attributes to file " + file.FullName);
                File.SetAttributes(file.FullName, FileAttributes.Normal);
            }
            foreach(var subDir in directory.GetDirectories())
            {
                SetNormalAttributes(subDir);
            }
        }
    }
}