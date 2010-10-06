using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace Deploy.Deployment.Steps
{
    public class BackupStep : DeploymentStepBase
    {
        public BackupStep(DeployParameters parameters)
            : base(parameters, "Backup")
        {
        }

        public override DeploymentStepStatus Execute()
        {
            if (Directory.Exists(Parameters.DestinationFolder))
            {
                if (!Directory.Exists(Parameters.BackupFolder))
                {
                    Directory.CreateDirectory(Parameters.BackupFolder);
                }
                Backup();
            }
            return new DeploymentStepStatus(true, DeploymentStepStatus.Ok);
        }

        private void Backup()
        {
            var backupFilename = GenerateBackupFilePath();
            using (var zipOutputStream = new ZipOutputStream(File.Create(backupFilename)))
            {
                var zipEntry = new ZipEntry(Parameters.DestinationFolder);
                zipOutputStream.PutNextEntry(zipEntry);
                
            }
        }

        private string GenerateBackupFilePath()
        {
            var filename = new StringBuilder(DateTime.Now.ToString()).Append(".zip").ToString();
            return Path.Combine(Parameters.BackupFolder, filename);
        }
    }
}
