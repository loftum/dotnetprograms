using BuildMonitor.Lib.Configuration;

namespace BuildMonitor.Models.Admin
{
    public class EditSettingsViewModel
    {
        public MonitorConfig Config { get; set; }
        public EditBuildServerViewModel BuildServer { get; set; }

        public EditSettingsViewModel()
        {
            Config = new MonitorConfig();
            BuildServer = new EditBuildServerViewModel();
        }

        public EditSettingsViewModel(MonitorConfig config)
        {
            Config = config;
            BuildServer = new EditBuildServerViewModel(config.BuildServerConfig);
        }
    }
}