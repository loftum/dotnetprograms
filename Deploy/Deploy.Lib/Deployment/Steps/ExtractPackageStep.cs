using System;
using System.IO;
using System.Linq;
using Deploy.Lib.FileManagement;
using Deploy.Lib.Logging;
using ICSharpCode.SharpZipLib.Zip;

namespace Deploy.Lib.Deployment.Steps
{
    public class ExtractPackageStep : DeploymentStepBase
    {
        private readonly IFileSystemManager _fileSystemManager;
        private const string WebConfigName = "web.config";

        public ExtractPackageStep(DeployParameters parameters, IFileSystemManager fileSystemManager, ILogger logger)
            : base(parameters, "Exctract package", logger)
        {
            _fileSystemManager = fileSystemManager;
        }

        protected override DeploymentStepStatus DoExecute()
        {
            var tempDirectory = _fileSystemManager.CreateTempDirectory();
            Status.AppendDetailsLine("Created temp directory " + tempDirectory.FullName);
            Status.AppendDetailsLine("Extracting " + Parameters.PackagePath + " to tempdir " + tempDirectory.FullName);
            
            using (var fileStream = new FileStream(Parameters.PackagePath, FileMode.Open, FileAccess.Read))
            {
                WriteAllEntries(fileStream, tempDirectory);
            }
            var rootDirectory = GetWebRootDirectory(tempDirectory);
            if (rootDirectory == null)
            {
                Status.AppendDetailsLine("Could not find web root directory in " + tempDirectory.FullName);
                Status.Status = DeploymentStepStatus.Fail;
                return Status;
            }
            Status.AppendDetailsLine("Copying contents of " + rootDirectory.FullName + " to " + Parameters.DestinationFolder);
            _fileSystemManager.CopyContentsOf(rootDirectory).To(Parameters.DestinationFolder);

            Status.AppendDetailsLine("Removing tempdir " + tempDirectory.FullName);
            _fileSystemManager.DeleteDirectory(tempDirectory);

            Status.Status = DeploymentStepStatus.Ok;
            return Status;
        }

        private static DirectoryInfo GetWebRootDirectory(DirectoryInfo directory)
        {
            if (ContainsWebConfig(directory))
            {
                return directory;
            }
            foreach (var subDirectory in directory.GetDirectories())
            {
                if (ContainsWebConfig(subDirectory))
                {
                    return subDirectory;
                }
            }
            foreach(var subDirectory in directory.GetDirectories())
            {
                var rootDirectory = GetWebRootDirectory(subDirectory);
                if (rootDirectory != null)
                {
                    return rootDirectory;
                }
            }
            return null;
        }

        private static bool ContainsWebConfig(DirectoryInfo tempDirectory)
        {
            return tempDirectory
                .GetFiles()
                .Any(file => file.Name.Equals(WebConfigName, StringComparison.InvariantCultureIgnoreCase));
        }

        private void WriteAllEntries(Stream fileStream, DirectoryInfo tempDirectory)
        {
            using(var zipInStream = new ZipInputStream(fileStream))
            {
                ZipEntry entry;
                while((entry = zipInStream.GetNextEntry()) != null)
                {
                    WriteEntry(zipInStream, entry, tempDirectory);
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
