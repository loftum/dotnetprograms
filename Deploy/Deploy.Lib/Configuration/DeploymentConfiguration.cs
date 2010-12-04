using System.Configuration;

namespace Deploy.Lib.Configuration
{
    public class DeploymentConfiguration
    {
        public static string ProfileFolder
        {
            get { return GetAppSetting("profileFolder"); }
        }

        public static string DefaultPackageFolder
        {
            get
            {
                var defaultPackage = GetAppSetting("defaultPackageFolder");
                return string.IsNullOrEmpty(defaultPackage) ? string.Empty : defaultPackage;
            }
        }

        private static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}
