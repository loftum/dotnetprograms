using System.IO;

namespace Deploy.Deployment
{
    public class DeployParameters
    {
        public string PackagePath { get; private set; }
        public string DestinationFolder { get; private set; }
        public string BackupFolder { get; private set; }

        private DeployParameters(string packagePath, string destinationFolder, string backupFolder)
        {
            PackagePath = packagePath;
            DestinationFolder = destinationFolder;
            BackupFolder = backupFolder;
        }

        public static DeployParameters Parse(string[] args)
        {
            if (args.Length < 3)
            {
                throw new InvalidParametersException("missing arguments");
            }
            VerifyExists(new FileInfo(args[0]));
            return new DeployParameters(args[0], args[1], args[2]);
        }

        private static void VerifyExists(FileSystemInfo info)
        {
            if (!info.Exists)
            {
                throw new InvalidParametersException(info.FullName + " does not exist.");
            }
        }
    }
}
