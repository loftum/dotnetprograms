using BuildMonitor.Lib.Model;

namespace BuildMonitor.Models.Monitor
{
    public class MonitorIndexViewModel
    {
        public BuildMonitorModel Monitor { get; set; }

        public MonitorIndexViewModel()
        {
            Monitor = new BuildMonitorModel();
        }

        public MonitorIndexViewModel(BuildMonitorModel monitor)
        {
            Monitor = monitor;
        }
    }
}