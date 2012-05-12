using BuildMonitor.Lib.Configuration;

namespace BuildMonitor.Models.Admin
{
    public class EditSettingsViewModel
    {
        public MonitorConfiguration Config { get; set; }

        public EditSettingsViewModel()
        {
            Config = new MonitorConfiguration();
        }

        public EditSettingsViewModel(MonitorConfiguration monitor)
        {
            Config = monitor;
        }
    }
}