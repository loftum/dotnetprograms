using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using BuildMonitor.Lib.Configuration;

namespace BuildMonitor.Models.Admin
{
    public class EditSettingsViewModel
    {
        public MonitorConfiguration Config { get; set; }

        public IEnumerable<SelectListItem> AvailableProjects { get; set; }

        public EditSettingsViewModel()
        {
            Config = new MonitorConfiguration();
            AvailableProjects = Enumerable.Empty<SelectListItem>();
        }

        public EditSettingsViewModel(MonitorConfiguration config)
        {
            Config = config;
            AvailableProjects = Enumerable.Empty<SelectListItem>();
        }
    }
}