using System;
using System.Configuration;
using System.Linq.Expressions;
using BasicManifest.Common.Exceptions;
using DotNetPrograms.Common.ExtensionMethods;

namespace BasicManifest.Common.Configuration
{
    public class BMConfig : IBMConfig
    {
        public bool ShowSql { get { return GetAppSettingOrDefault(() => ShowSql); } }
        public bool EnableNHibernateDiagnostics { get { return GetAppSettingOrDefault(() => EnableNHibernateDiagnostics); } }
        public string BasicManifestConnectionString { get { return BasicManifest.ConnectionString; } }

        private static T GetAppSettingOrThrow<T>(Expression<Func<T>> expression)
        {
            var settingString = GetAppSetting(expression);
            if (settingString == null)
            {
                throw new BMException(string.Format("Missing appsetting '{0}'", expression.GetPropertyName()));
            }
            return settingString.ConvertTo<T>();
        }

        private static T GetAppSettingOrDefault<T>(Expression<Func<T>> expression, T defaultValue = default(T))
        {
            var settingString = GetAppSetting(expression);
            return settingString == null ? defaultValue : settingString.ConvertTo<T>();
        }

        private static string GetAppSetting<T>(Expression<Func<T>> expression)
        {
            var name = expression.GetPropertyName();
            return ConfigurationManager.AppSettings[name];
        }

        private ConnectionStringSettings BasicManifest { get { return GetConnectionString(() => BasicManifest); } }

        private static ConnectionStringSettings GetConnectionString(Expression<Func<ConnectionStringSettings>> expression)
        {
            var name = expression.GetPropertyName();
            return ConfigurationManager.ConnectionStrings[name];
        }
    }
}