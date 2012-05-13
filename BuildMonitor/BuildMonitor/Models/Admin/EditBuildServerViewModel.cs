using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BuildMonitor.Lib.Configuration;

namespace BuildMonitor.Models.Admin
{
    public class EditBuildServerViewModel
    {
        public BuildServerConfig Config { get; set; }

        public IEnumerable<SelectListItem> AvailableProjects { get; set; }

        public EditBuildServerViewModel()
        {
            Config = new BuildServerConfig();
            AvailableProjects = Enumerable.Empty<SelectListItem>();
        }

        public EditBuildServerViewModel(BuildServerConfig config)
        {
            Config = config;
            AvailableProjects = Enumerable.Empty<SelectListItem>();
        }
    }
}