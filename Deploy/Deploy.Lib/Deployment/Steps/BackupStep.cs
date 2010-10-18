using System;
using System.IO;
using System.Text;
using Deploy.Lib.Logging;
using ICSharpCode.SharpZipLib.Zip;

namespace Deploy.Lib.Deployment.Steps
{
    public class BackupStep : DeploymentStepBase
    {
        public BackupStep(DeployParameters parameters, ILogger logger)
            : base(parameters, "Backup", logger)
        {
        }

        protected override DeploymentStepStatus DoExecute()
        {
            if (Parameters.Profile.BackupSettings.Skip)
            {
                SetStatusSkipped();
                return Status;
            }
            try
            {
                BackupIfDestinationExists();
                Status.Status = DeploymentStepStatus.Ok;
            }
            catch (Exception e)
            {
                Status.Status = DeploymentStepStatus.Fail;
                Status.Error = e.ToString();
            }
            return Status;
        }

        private void BackupIfDestinationExists()
        {
            if (Directory.Exists(Parameters.Profile.DestinationSettings.Folder))
            {
                CreateBackupFolderIfNotExists();
                Backup();
            }
        }

        private void CreateBackupFolderIfNotExists()
        {
            if (!Directory.Exists(Parameters.BackupFolder))
            {
                Directory.CreateDirectory(Parameters.BackupFolder);
                Status.AppendDetailsLine("Creating backup folder: " + Parameters.BackupFolder);
            }
        }

        private void Backup()
        {
            var backupFilePath = GenerateBackupFilePath();
            Status.AppendDetailsLine("Backup file: " + backupFilePath);
            using (var zipOutputStream = new ZipOutputStream(File.Create(backupFilePath)))
            {
                ZipFolder(Parameters.DestinationFolder, zipOutputStream);
            }
        }

        private void ZipFolder(string folder, ZipOutputStream zipOutputStream)
        {
            foreach(var subFolder in Directory.GetDirectories(folder))
            {
                ZipFolder(subFolder, zipOutputStream);
            }
            foreach (var file in Directory.GetFiles(folder))
            {
                AddFile(file, zipOutputStream);
            }
        }

        private void AddFile(string file, ZipOutputStream zipOutputStream)
        {
            var relativePath = GetRelativePathFor(file);
            var entry = new ZipEntry(relativePath);
            zipOutputStream.PutNextEntry(entry);
            using (var fileStream = File.OpenRead(file))
            {
                var buffer = new byte[4096];
                int size;
                do
                {
                    size = fileStream.Read(buffer, 0, buffer.Length);
                    zipOutputStream.Write(buffer, 0, buffer.Length);
                } while (size > 0);
            }
        }

        private string GetRelativePathFor(string folder)
        {
            return new StringBuilder(folder).
                Remove(0, Parameters.DestinationFolder.Length)
                .ToString();
        }


        private string GenerateBackupFilePath()
        {
            var now = DateTime.Now;
            var filename = new StringBuilder("backup_")
                .Append(now.ToShortDateString().Replace(".", string.Empty))
                .Append("_")
                .Append(now.ToShortTimeString().Replace(":", string.Empty))
                .Append(".zip").ToString();
            return Path.Combine(Parameters.BackupFolder, filename);
        }
    }
}
