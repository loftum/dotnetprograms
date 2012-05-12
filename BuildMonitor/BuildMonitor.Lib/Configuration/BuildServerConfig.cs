using System.Collections.Generic;
using System.Linq;
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
                return new ModelValidator<BuildServerConfig>(this)
                    .Require(m => m.Name, m => m.Host, m => m.Username, m => m.Password)
                    .IsValid;
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
            ProjectIdsPiped = string.Empty;
        }

        public IEnumerable<string> ProjectIds
        {
            get { return ProjectIdsPiped.IsNullOrWhiteSpace() ? Enumerable.Empty<string>() : ProjectIdsPiped.Split('|'); }
            set { ProjectIdsPiped = value.IsNullOrEmpty() ? string.Empty : string.Join("|", value); }
        }

        private string _projectIdsPiped;
        public string ProjectIdsPiped
        {
            get { return _projectIdsPiped.IsNullOrWhiteSpace() ? string.Empty : _projectIdsPiped; }
            set { _projectIdsPiped = value.IsNullOrWhiteSpace() ? string.Empty : value; }
        }
    }
}