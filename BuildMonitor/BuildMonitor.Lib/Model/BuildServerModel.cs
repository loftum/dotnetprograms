using System.Collections.Generic;
using BuildMonitor.Common.ExtensionMethods;
using BuildMonitor.Lib.Configuration;

namespace BuildMonitor.Lib.Model
{
    public class BuildServerModel
    {
        public string Name { get; set; }
        public string Host { get; set; }

        public IList<ProjectModel> Projects { get; set; }

        public bool IsValid
        {
            get { return !(Host.IsNullOrWhiteSpace() || Name.IsNullOrWhiteSpace()); }
        }

        public BuildServerModel()
        {
            Projects = new List<ProjectModel>();
        }

        public BuildServerModel(BuildServerConfig config) : this()
        {
            Name = config.Name;
            Host = config.Host;
        }
    }
}