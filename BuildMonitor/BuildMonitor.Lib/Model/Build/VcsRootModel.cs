using System;
using BuildMonitor.Common.ExtensionMethods;

namespace BuildMonitor.Lib.Model.Build
{
    public class VcsRootModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string VcsName { get; set; }
        public string Href { get; set; }
        public bool Shared { get; set; }
        public string Status { get; set; }
        public DateTime LastChecked { get; set; }
        public string BranchName { get; set; }
        public ProjectModel Project { get; set; }
        public string ProjectId { get { return HasProject ? Project.Id : string.Empty; } }

        public bool HasProject { get { return Project != null; } }

        public string BranchNameDisplay
        {
            get { return BranchName.IsNullOrEmpty() ? "Unknown branch" : BranchName; }
        }
    }
}