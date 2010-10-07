using System;
using System.IO;
using System.Text;
using ICSharpCode.SharpZipLib.Zip;

namespace Deploy.Lib.Deployment.Steps
{
    public class BackupStep : DeploymentStepBase
    {
        private readonly DeploymentStepStatus _status = new DeploymentStepStatus();

        public BackupStep(DeployParameters parameters)
            : base(parameters, "Backup")
        {
        }

        public override DeploymentStepStatus Execute()
        {
            try
            {
                BackupIfDestinationExists();
                _status.Status = DeploymentStepStatus.Ok;
            }
            catch (Exception e)
            {
                _status.Status = DeploymentStepStatus.Fail;
                _status.Exception = e.ToString();
            }
            return _status;
        }

        private void BackupIfDestinationExists()
        {
            if (Directory.Exists(Parameters.DestinationFolder))
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
                _status.AppendCommentLine("Creating backup folder: " + Parameters.BackupFolder);
            }
        }

        private void Backup()
        {
            var backupFilePath = GenerateBackupFilePath();
            _status.AppendCommentLine("Backup file: " + backupFilePath);
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
