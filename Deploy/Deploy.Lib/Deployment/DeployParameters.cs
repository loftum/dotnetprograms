using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Text;
using Deploy.Lib.Configuration;
using Deploy.Lib.Profiles;

namespace Deploy.Lib.Deployment
{
    [Serializable]
    public class DeployParameters
    {
        private const string PackagePathName = "package";
        private const string DestinationFolderName = "dest";
        private const string BackupFolderName = "backup";
        private const string NewWebConfigLocationName = "newWebConfig";
        private const string WebConfigLocationName = "webconfigLocation";
        private const string DeployStatusLocationName = "statusLocation";

        public string PackagePath { get; set; }
        public string DestinationFolder { get; set; }
        public string BackupFolder { get; set; }
        public string NewWebConfigPath { get; set; }
        public string WebConfigPath { get; set; }
        public string DeployStatusPath { get; set; }

        public DeployParameters()
        {
        }

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
            if (args.Length < 3)
            {
                return ParseFromProfile(args);
            }
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

        private static DeployParameters ParseFromProfile(string[] args)
        {
            try
            {
                var profile = new ProfileReader(DeploymentConfiguration.ProfileFolder).GetProfile(args[1]);
            }
            catch (Exception)
            {
                throw new InvalidParametersException("Could not parse " + args);
                
            }
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
