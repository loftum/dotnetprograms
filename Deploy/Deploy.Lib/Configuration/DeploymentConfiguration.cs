using System.Configuration;

namespace Deploy.Lib.Configuration
{
    public class DeploymentConfiguration
    {
        private const string ProfileFolderSetting = "profileFolder";
        private const string DefaultPackageFolderSetting = "defaultPackageFolder";
        
        public static string ProfileFolder
        {
            get { return ConfigurationManager.AppSettings[ProfileFolderSetting]; }
        }

        public static string DefaultPackageFolder
        {
            get
            {
                var defaultPackage = ConfigurationManager.AppSettings[DefaultPackageFolderSetting];
                return string.IsNullOrEmpty(defaultPackage) ? string.Empty : defaultPackage;
            }
        }
    }
}
