using System;
using System.IO;
using Deploy.Lib.FileManagement;
using Deploy.Lib.Logging;
using ICSharpCode.SharpZipLib.Zip;

namespace Deploy.Lib.Deployment.Steps
{
    public class ExtractPackageStep : DeploymentStepBase
    {
        private readonly IFileSystemManager _fileSystemManager;

        public ExtractPackageStep(DeployParameters parameters, IFileSystemManager fileSystemManager, IDeployLogger logger)
            : base(parameters, "Exctract package", logger)
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
            try
            {
                var tempDirectory = CreateTempDirectory();
                WriteAllEntriesTo(tempDirectory);
                Status.Status = DeploymentStepStatus.Ok;
            }
            catch (Exception e)
            {
                Status.AppendDetailsLine("Extract package failed");
                Status.AppendDetailsLine(e.ToString());
                Status.Status = DeploymentStepStatus.Fail;
                Status.CanProceed = false;
            }
            return Status;
        }

        private DirectoryInfo CreateTempDirectory()
        {
            var tempDirectory = _fileSystemManager.CreateTempDirectory();
            Parameters.TempDirectoryPath = tempDirectory.FullName;
            Status.AppendDetailsLine("Created temp directory " + tempDirectory.FullName);
            return tempDirectory;
        }

        private void WriteAllEntriesTo(DirectoryInfo tempDirectory)
        {
            Status.AppendDetailsLine("Extracting " + Parameters.PackagePath + " to tempdir " + tempDirectory.FullName);
            using (var fileStream = new FileStream(Parameters.PackagePath, FileMode.Open, FileAccess.Read))
            {
                using (var zipInStream = new ZipInputStream(fileStream))
                {
                    ZipEntry entry;
                    while ((entry = zipInStream.GetNextEntry()) != null)
                    {
                        WriteEntry(zipInStream, entry, tempDirectory);
                    }
                }    
            }
        }

        private void WriteEntry(Stream zipInStream, ZipEntry entry, FileSystemInfo tempDirectory)
        {
            if (!entry.IsFile)
            {
                return;
            }
            CreateDirectoryFor(tempDirectory, entry);
            var fullEntryPath = Path.Combine(tempDirectory.FullName, entry.Name);
            using (var fileOutStream = 
                new FileStream(fullEntryPath, FileMode.CreateNew, FileAccess.Write))
            {
                int size;
                var buffer = new byte[4096];
                do
                {
                    size = zipInStream.Read(buffer, 0, buffer.Length);
                    fileOutStream.Write(buffer, 0, size);
                } while (size > 0);
            }
        }

        private void CreateDirectoryFor(FileSystemInfo tempDirectory, ZipEntry entry)
        {
            var directoryPath = Path.Combine(tempDirectory.FullName, Path.GetDirectoryName(entry.Name));
            if (!Directory.Exists(directoryPath))
            {
                Status.AppendDetailsLine("Creating directory " + directoryPath);
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
