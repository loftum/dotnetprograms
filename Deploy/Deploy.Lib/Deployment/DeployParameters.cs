using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Deploy.Lib.Deployment.ProfileManagement;
using Deploy.Lib.Deployment.Profiles;
using Deploy.Lib.Validation;

namespace Deploy.Lib.Deployment
{
    [Serializable]
    public class DeployParameters
    {
        private const string PackagePathName = "package";
        private const string DestinationFolderName = "dest";
        private const string BackupFolderName = "backup";
        private const string NewWebConfigLocationName = "newWebConfig";
        private const string DeployStatusLocationName = "statusLocation";
        private const string DeploymentProfileName = "profile";

        public string PackagePath { get; set; }
        public string DestinationFolder { get { return Profile.DestinationSettings.Folder; } }
        public string BackupFolder { get { return Profile.BackupSettings.Folder; } }
        public string NewWebConfigPath { get { return Profile.WebConfigSettings.NewWebConfigPath; } }
        public string DeployStatusFolder { get { return Profile.DeployStatusSettings.Folder; } }

        public DeploymentProfile Profile { get; set; }

        public DeployParameters()
        {
        }

        public DeployParameters(string packagePath, DeploymentProfile profile)
        {
            PackagePath = packagePath;
            Profile = profile;
            ValidateAll();
        }

        public static DeployParameters Parse(string[] args)
        {
            var arguments = new Arguments(args);
            if (args.Length < 2)
            {
                throw new InvalidParametersException("missing arguments");
            }
            var parameters = new DeployParameters(
                arguments.ByNameOrIndex(PackagePathName, 0),
                GetProfile(arguments));
            return parameters;
        }

        private static DeploymentProfile GetProfile(Arguments arguments)
        {
            if (arguments.Count == 2)
            {
                return ProfileManager.Instance.Get(arguments.ByNameOrIndex(DeploymentProfileName, 1));
            }
            return new DeploymentProfile
                {
                    Name = "Command line",
                    DestinationSettings =
                        new DestinationSettings {Folder = arguments.ByNameOrIndex(DestinationFolderName, 1)},
                    BackupSettings = new BackupSettings {Folder = arguments.ByNameOrIndex(BackupFolderName, 2)},
                    WebConfigSettings =
                        new WebConfigSettings
                            {NewWebConfigPath = arguments.ByNameOrIndex(NewWebConfigLocationName, 3)},
                    DeployStatusSettings =
                        new DeployStatusSettings {Folder = arguments.ByNameOrIndex(DeployStatusLocationName, 4)}
                };
        }

        private void ValidateAll()
        {
            var validator = new PathValidator();
            var messages = new List<string>();
            messages.AddRange(Validate(validator.VerifyDirectoryExists, 
                Profile.BackupSettings.Folder,
                Profile.DeployStatusSettings.Folder,
                Profile.DestinationSettings.Folder
                ));
            messages.AddRange(Validate(validator.VerifyFileExists,
                PackagePath, 
                Profile.WebConfigSettings.NewWebConfigPath));
            
            if (messages.Count > 0)
            {
                var builder = new StringBuilder();
                foreach (var message in messages)
                {
                    builder.AppendLine(message);
                }
                throw new InvalidParametersException(builder.ToString());
            }
        }

        private static IEnumerable<string> Validate(Action<string> action,  params string[] files)
        {
            var errorMessages = new List<string>();
            foreach (var file in files)
            {
                try
                {
                    action(file);
                }
                catch (ValidationException e)
                {
                    errorMessages.Add(e.Message);
                }
            }
            return errorMessages;
        }

        public static string GetUsage()
        {
            var processName = Process.GetCurrentProcess().ProcessName;
            return new StringBuilder(processName).Append(" ")
                .Append(Parameter(PackagePathName, "<packagePath>")).Append(" ")
                .Append(Parameter(DestinationFolderName, "<destinationFolder>")).Append(" ")
                .Append(Parameter(BackupFolderName, "<backupFolder>")).Append(" ")
                .Append(Parameter(NewWebConfigLocationName, "<newWebconfigPath>")).Append(" ")
                .Append(Parameter(DeployStatusLocationName, "<deployStatusLocation>")).Append(" ")
                .AppendLine().AppendLine()
                .Append(processName)
                .Append(Parameter(PackagePathName, "<packagePath>"))
                .Append(Parameter(DeploymentProfileName, "<profilename>"))
                .ToString();
        }

        private static string Parameter(string parameterName, string parameterValue)
        {
            return new StringBuilder("[").Append(parameterName).Append(":]").Append(parameterValue).ToString();
        }
    }
}
