using System;
using System.Configuration;

namespace WebShop.Common.Configuration
{
    public class ConfigSettings : IConfigSettings
    {
        public string MasterDataConnectionString { get { return MasterData.ConnectionString; } }

        public bool EnableNhDiagnostics { get { return GetAppSettingOrDefault<bool>("EnableNhDiagnostics"); } }
        public bool ShowNhSql { get { return GetAppSettingOrDefault<bool>("ShowNhSql"); } }

        private static T GetAppSettingOrDefault<T>(string name, T defaultValue = default(T))
        {
            var value = ConfigurationManager.AppSettings[name];
            return value == null ? defaultValue : (T) Convert.ChangeType(value, typeof (T));
        }

        private ConnectionStringSettings MasterData { get { return ConfigurationManager.ConnectionStrings["MasterData"]; } }
    }
}