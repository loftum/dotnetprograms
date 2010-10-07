using System.IO;

namespace Deploy.Lib.Deployment.Steps
{
    public class ClearDestinationFolderStep : DeploymentStepBase
    {
        public ClearDestinationFolderStep(DeployParameters parameters) 
            : base(parameters, "Clear destination folder")
        {
        }

        public override DeploymentStepStatus Execute()
        {
            if (Directory.Exists(Parameters.DestinationFolder))
            {
                CleanDestinationFolder();    
            }
            else
            {
                Directory.CreateDirectory(Parameters.DestinationFolder);    
            }
            return new DeploymentStepStatus(true, DeploymentStepStatus.Ok);
        }

        private void CleanDestinationFolder()
        {
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
