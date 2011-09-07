using System;
using System.Configuration;
using System.IO;

namespace DbTool.Lib.Configuration
{
    public class DbToolConfig : IDbToolConfig
    {
        private static DbToolSettings _settings;
        public DbToolSettings Settings
        {
            get
            {
                if (_settings == null)
                {
                    if (File.Exists(SettingsPath))
                    {
                        _settings = DbToolSettings.From(SettingsPath);    
                    }
                    else
                    {
                        _settings = DbToolSettings.Default;
                    }
                }
                return _settings;
            }
        }

        public string SettingsPath
        {
            get { return GetAppSetting("SettingsPath"); }
        }

        private static string GetAppSetting(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        public void SaveSettings()
        {
            if (!File.Exists(SettingsPath))
            {
                using (File.Create(SettingsPath))
                {
                }
            }
            Settings.Save(SettingsPath);
        }
    }
}