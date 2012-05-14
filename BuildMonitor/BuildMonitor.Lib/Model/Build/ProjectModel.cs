using System.Collections.Generic;
using System.Linq;

namespace BuildMonitor.Lib.Model.Build
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
        public bool HasVcsRoot { get { return VcsRoot != null; } }
        public VcsRootModel VcsRoot { get; set; }

        public string NameDisplay
        {
            get { return HasVcsRoot ? string.Format("{0} ({1})", Name, VcsRoot.BranchNameDisplay) : Name; }
        }

        public ProjectModel()
        {
            BuildTypes = new BuildTypeModel[0];
        }
    }
}