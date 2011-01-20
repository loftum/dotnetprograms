using System.Configuration;

namespace HourGlass.Lib.Configurating
{
    public class HourGlassConfig : IHourGlassConfig
    {
        public string SqliteFilePath
        {
            get { return GetAppSetting("SqliteFilePath"); }
        }

        private static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }
    }
}