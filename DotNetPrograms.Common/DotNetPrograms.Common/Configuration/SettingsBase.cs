using System;
using System.Configuration;
using System.Linq.Expressions;
using DotNetPrograms.Common.Exceptions;
using DotNetPrograms.Common.ExtensionMethods;

namespace DotNetPrograms.Common.Configuration
{
    public abstract class AppSettingsBase
    {
        protected T GetAppSetting<T>(Expression<Func<T>> expression)
        {
            var name = expression.GetPropertyName();
            return GetAppSetting(name).ConvertTo<T>();
        }

        protected T GetAppSettingOrDefault<T>(Expression<Func<T>> expression, T defaultValue = default(T))
        {
            var name = expression.GetPropertyName();
            return GetAppSetting(name).ConvertToOrDefault(defaultValue);
        }

        protected T GetAppSettingOrThrow<T>(Expression<Func<T>> expression)
        {
            var name = expression.GetPropertyName();
            try
            {
                var value = GetAppSetting(name);
                if (value.IsNullOrWhiteSpace())
                {
                    throw new UserException("You must specify appSetting '{0}'", name);
                }
                return value.ConvertTo<T>();
            }
            catch (Exception)
            {
                throw new UserException("You must specify appSetting '{0}'", name);
            }
        }

        private static string GetAppSetting(string name)
        {
            return ConfigurationManager.AppSettings[name];
        }
    }
}