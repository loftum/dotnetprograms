using System;
using System.Configuration;

namespace BuildMonitor.Lib.Configuration
{
    public class BuildMonitorSettings : IBuildMonitorSettings
    {
        public string BuildHost
        {
            get { return GetAppSetting<string>("BuildHost"); }
        }

        private static T GetAppSetting<T>(string name)
        {
            return ConvertTo<T>(ConfigurationManager.AppSettings[name]);
        }

        private static T ConvertTo<T>(object value)
        {
            return (T) Convert.ChangeType(value, typeof (T));
        }

        public IBuildServerSettings BuildServer
        {
            get { return (IBuildServerSettings)ConfigurationManager.GetSection("buildServers/teamCity"); }
        }
    }
}