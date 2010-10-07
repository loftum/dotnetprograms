using System.Diagnostics;
using System.IO;
using System.Text;

namespace Deploy.Lib.Deployment
{
    public class DeployParameters
    {
        private const string PackagePathName = "package";
        private const string DestinationFolderName = "dest";
        private const string BackupFolderName = "backup";
        private const string NewWebConfigLocationName = "newWebConfig";
        private const string WebConfigLocationName = "webconfigLocation";
        private const string DeployStatusLocationName = "statusLocation";

        public string PackagePath { get; private set; }
        public string DestinationFolder { get; private set; }
        public string BackupFolder { get; private set; }
        public string NewWebConfigPath { get; private set; }
        public string WebConfigPath { get; private set; }
        public string DeployStatusPath { get; private set; }

        private DeployParameters(string packagePath, string destinationFolder, 
            string backupFolder, string configFilePath, string webConfigPath,
            string deployStatusPath)
        {
            PackagePath = packagePath;
            DestinationFolder = destinationFolder;
            BackupFolder = backupFolder;
            NewWebConfigPath = configFilePath;
            WebConfigPath = webConfigPath;
            DeployStatusPath = deployStatusPath;
        }

        public static DeployParameters Parse(string[] args)
        {
            if (args.Length < 5)
            {
                throw new InvalidParametersException("missing arguments");
            }
            var arguments = new Arguments(args);
            var parameters = new DeployParameters(
                arguments.ByNameOrIndex(PackagePathName, 0),
                arguments.ByNameOrIndex(DestinationFolderName, 1),
                arguments.ByNameOrIndex(BackupFolderName, 2),
                arguments.ByNameOrIndex(NewWebConfigLocationName, 3),
                arguments.ByNameOrIndex(WebConfigLocationName, 4),
                arguments.ByNameOrIndex(DeployStatusLocationName, 5));
            VerifyExists(new FileInfo(parameters.PackagePath));
            VerifyExists(new FileInfo(parameters.NewWebConfigPath));
            return parameters;
        }

        private static void VerifyExists(FileSystemInfo info)
        {
            if (!info.Exists)
            {
                throw new InvalidParametersException(info.FullName + " does not exist.");
            }
        }

        public static string GetUsage()
        {
            return new StringBuilder(Process.GetCurrentProcess().ProcessName).Append(" ")
                .Append(Parameter(PackagePathName, "<packagePath>")).Append(" ")
                .Append(Parameter(DestinationFolderName, "<destinationFolder>")).Append(" ")
                .Append(Parameter(BackupFolderName, "<backupFolder>")).Append(" ")
                .Append(Parameter(NewWebConfigLocationName, "<newWebconfigPath>")).Append(" ")
                .Append(Parameter(WebConfigLocationName, "<webConfigPath>")).Append(" ")
                .Append(Parameter(DeployStatusLocationName, "<deployStatusLocation>")).Append(" ")
                .AppendLine().ToString();
        }

        private static string Parameter(string parameterName, string parameterValue)
        {
            return new StringBuilder("[").Append(parameterName).Append(":]").Append(parameterValue).ToString();
        }
    }
}
