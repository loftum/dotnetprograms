using System.Collections.Generic;
using System.Configuration;

namespace BuildMonitor.Lib.Configuration
{
    public class BuildServerSettings : ConfigurationSection, IBuildServerSettings
    {
        [ConfigurationProperty("host")]
        public string Host
        {
            get { return (string) this["host"]; }
        }

        [ConfigurationProperty("username")]
        public string Username
        {
            get { return (string) this["username"]; }
        }

        [ConfigurationProperty("password")]
        public string Password
        {
            get { return (string) this["password"]; }
        }

        public IEnumerable<string> ProjectIds
        {
            get { return ProjectIdsPiped.Split('|'); }
        }

        [ConfigurationProperty("projectIds")]
        public string ProjectIdsPiped
        {
            get { return Get<string>("projectIds"); }
        }

        private T Get<T>(string key)
        {
            return (T) this[key];
        }
    }
}