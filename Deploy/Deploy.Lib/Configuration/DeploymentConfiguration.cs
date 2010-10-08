using System.Configuration;

namespace Deploy.Lib.Configuration
{
    public class DeploymentConfiguration
    {
        private const string ProfileFolderSetting = "profileFolder";
        private const string BackupFolderSetting = "backupFolder";
        private const string DeploymentStatusFolderSetting = "deploymentStatusFolder";

        public static string BackupFolder
        {
            get {return ConfigurationManager.AppSettings[BackupFolderSetting];}
        }

        public static string DeploymentStatusFolder
        {
            get { return ConfigurationManager.AppSettings[DeploymentStatusFolderSetting]; }
        }

        public static string ProfileFolder
        {
            get { return ConfigurationManager.AppSettings[ProfileFolderSetting]; }
        }
    }
}
