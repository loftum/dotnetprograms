using System.Collections.Generic;
using System.Linq;

namespace BuildMonitor.Lib.Model
{
    public class ProjectModel
    {
        public string Status
        {
            get { return BuildTypes.All(b => b.Status == BuildStatus.Success) ? BuildStatus.Success : BuildStatus.Failure; }
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public IList<BuildTypeModel> BuildTypes { get; set;}

        public ProjectModel()
        {
            BuildTypes = new BuildTypeModel[0];
        }
    }
}