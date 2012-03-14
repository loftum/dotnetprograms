using System;
using System.Configuration;
using VisualFarmStudio.Lib.ExtensionMethods;

namespace VisualFarmStudio.Lib.Configuration
{
    public class VFSConfig : IVFSConfig
    {
        private string _behave;
        public string Behave
        {
            get { return _behave.IsNullOrEmpty() ? AppSettingOrDefault<string>("Behave") : _behave; }
            set { _behave = value; }
        }

        public string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["Bondegard"].ConnectionString; }
        }

        private static T AppSettingOrDefault<T>(string name, T defaultValue = default(T))
        {
            try
            {
                return ConfigurationManager.AppSettings[name].ConvertTo<T>();
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}