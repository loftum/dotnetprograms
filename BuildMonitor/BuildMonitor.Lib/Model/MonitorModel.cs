using System.Collections.Generic;
using System.Linq;

namespace BuildMonitor.Lib.Model
{
    public class MonitorModel
    {
        public string BuildHost { get; private set; }

        public IEnumerable<ProjectModel> Projects { get; set; }

        public MonitorModel()
        {
            Projects = Enumerable.Empty<ProjectModel>();
        }

        public MonitorModel(IEnumerable<ProjectModel> groups, string buildHost)
        {
            BuildHost = buildHost;
            Projects = groups;
        }
    }
}