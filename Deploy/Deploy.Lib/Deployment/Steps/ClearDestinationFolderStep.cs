using System.IO;
using Deploy.Lib.Logging;

namespace Deploy.Lib.Deployment.Steps
{
    public class ClearDestinationFolderStep : DeploymentStepBase
    {
        public ClearDestinationFolderStep(DeployParameters parameters, ILogger logger)
            : base(parameters, "Clear destination folder", logger)
        {
        }

        protected override DeploymentStepStatus DoExecute()
        {
            if (Directory.Exists(Parameters.DestinationFolder))
            {
                CleanDestinationFolder();
                Status.Status = DeploymentStepStatus.Ok;
            }
            else
            {
                Directory.CreateDirectory(Parameters.DestinationFolder);    
            }
            return Status;
        }

        private void CleanDestinationFolder()
        {
            Status.AppendDetailsLine("Cleaning " + Parameters.DestinationFolder);
            var destinationFolder = new DirectoryInfo(Parameters.DestinationFolder);
            foreach(var subDirectory in destinationFolder.GetDirectories())
            {
                subDirectory.Delete(true);
            }
            foreach (var file in destinationFolder.GetFiles())
            {
                file.Delete();
            }
        }
    }
}
