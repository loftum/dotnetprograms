using System.Collections.Generic;
using BuildMonitor.Common.ExtensionMethods;
using BuildMonitor.Lib.Validation;

namespace BuildMonitor.Lib.Configuration
{
    public class BuildServerConfig
    {
        private string _name;
        public string Name
        {
            get { return _name.IsNullOrWhiteSpace() ? "Untitled" : _name; }
            set { _name = value.IsNullOrEmpty() ? string.Empty : value; }
        }
        public string Host { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsValid
        {
            get
            {
                return Validate().IsValid;
            }
        }

        public bool HasCredentials
        {
            get
            {
                return new ModelValidator<BuildServerConfig>(this)
                  .Require(m => m.Host, m => m.Username, m => m.Password)
                  .IsValid;
            }
        }

        public BuildServerConfig()
        {
            ProjectIds = new List<string>();
        }

        public IList<string> ProjectIds { get; set; }

        public ModelValidator<BuildServerConfig> Validate()
        {
            return new ModelValidator<BuildServerConfig>(this)
                .Require(m => m.Name, m => m.Host, m => m.Username, m => m.Password);
        }

        public void UpdateFrom(BuildServerConfig config)
        {
            Name = config.Name;
            Host = config.Host;
            Username = config.Username;
            Password = config.Password;
            ProjectIds = config.ProjectIds;
        }
    }
}