using System.IO;
using ICSharpCode.SharpZipLib.Zip;

namespace Deploy.Lib.Deployment.Steps
{
    public class ExtractPackageStep : DeploymentStepBase
    {
        public ExtractPackageStep(DeployParameters parameters)
            : base(parameters, "Exctract package")
        {
        }

        public override DeploymentStepStatus Execute()
        {
            using (var fileStream = new FileStream(Parameters.PackagePath, FileMode.Open, FileAccess.Read))
            {
                WriteAllEntriesFrom(fileStream);
            }
            return new DeploymentStepStatus(true, DeploymentStepStatus.Ok);
        }

        private void WriteAllEntriesFrom(Stream fileStream)
        {
            using(var zipInStream = new ZipInputStream(fileStream))
            {
                ZipEntry entry;
                while((entry = zipInStream.GetNextEntry()) != null)
                {
                    WriteEntry(zipInStream, entry);
                }
            }
        }

        private void WriteEntry(Stream zipInStream, ZipEntry entry)
        {
            CreateDirectoryFor(entry);
            using (var fileOutStream = 
                new FileStream(Parameters.DestinationFolder + @"\" + entry.Name, FileMode.CreateNew,
                               FileAccess.Write))
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

        private void CreateDirectoryFor(ZipEntry entry)
        {
            var directoryPath = Path.Combine(Parameters.DestinationFolder, Path.GetDirectoryName(entry.Name));
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
    }
}
