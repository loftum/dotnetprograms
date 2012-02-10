using System.Configuration;
using System.IO;
using DbTool.Lib.Serializing;

namespace DbTool.Lib.Configuration
{
    public class DbToolConfig : IDbToolConfig
    {
        private readonly string _initialSerializedSettings;
        private static DbToolSettings _settings;
        public DbToolSettings Settings
        {
            get
            {
                return _settings ?? (_settings = File.Exists(SettingsPath)
                                                     ? Read()
                                                     : DbToolSettings.Default);
            }
        }

        public DbToolConfig()
        {
            var settings = Settings;
            _initialSerializedSettings = DbToolSettingsSerializer.Serialize(settings);
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
            var serialized = DbToolSettingsSerializer.Serialize(Settings);
            if (!serialized.Equals(_initialSerializedSettings))
            {
                DoSave(serialized);

            }
        }

        private DbToolSettings Read()
        {
            return DbToolSettingsSerializer.From(SettingsPath);
        }

        private void DoSave(string serializedSettings)
        {
            using (var writer = new StreamWriter(File.Create(SettingsPath)))
            {
                writer.Write(serializedSettings);
                writer.Flush();
                writer.Close();
            }
        }
    }
}