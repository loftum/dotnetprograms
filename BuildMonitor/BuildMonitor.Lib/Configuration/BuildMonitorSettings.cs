using System;
using System.Configuration;

namespace BuildMonitor.Lib.Configuration
{
    public class BuildMonitorSettings : IBuildMonitorSettings
    {
        private static T GetAppSetting<T>(string name)
        {
            return ConvertTo<T>(ConfigurationManager.AppSettings[name]);
        }

        private static T ConvertTo<T>(object value)
        {
            return (T) Convert.ChangeType(value, typeof (T));
        }

        public string ConfigFile
        {
            get { return GetAppSetting<string>("ConfigFile"); }
        }
    }
}