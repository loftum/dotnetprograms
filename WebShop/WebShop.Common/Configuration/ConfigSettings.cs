using System;
using System.Configuration;
using System.Linq.Expressions;
using DotNetPrograms.Common.ExtensionMethods;

namespace WebShop.Common.Configuration
{
    public class ConfigSettings : IConfigSettings
    {
        public string MasterDataConnectionString { get { return MasterData.ConnectionString; } }
        public string OrderDbConnectionString { get { return OrderDb.ConnectionString; } }

        public bool EnableNhDiagnostics { get { return GetAppSettingOrDefault(() => EnableNhDiagnostics); } }
        public bool ShowNhSql { get { return GetAppSettingOrDefault(() => ShowNhSql); } }

        private static T GetAppSettingOrDefault<T>(Expression<Func<T>> property, T defaultValue = default(T))
        {
            var name = property.GetMemberName();
            return GetAppSettingOrDefault(name, defaultValue);
        }

        private static T GetAppSettingOrDefault<T>(string name, T defaultValue = default(T))
        {
            var value = ConfigurationManager.AppSettings[name];
            return value == null ? defaultValue : (T) Convert.ChangeType(value, typeof (T));
        }

        private ConnectionStringSettings MasterData { get { return GetConnectionString(() => MasterData); } }
        public ConnectionStringSettings OrderDb { get { return GetConnectionString(() => OrderDb); } }

        private ConnectionStringSettings GetConnectionString(Expression<Func<ConnectionStringSettings>> property)
        {
            var name = property.GetMemberName();
            return ConfigurationManager.ConnectionStrings[name];
        }
    }
}