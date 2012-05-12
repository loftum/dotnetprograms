using BuildMonitor.Lib.Model;

namespace BuildMonitor.Models.Monitor
{
    public class MonitorIndexViewModel
    {
        public MonitorInfo Info { get; private set; }
        public MonitorModel Monitor { get; private set; }

        public MonitorIndexViewModel()
        {
            Monitor = new MonitorModel();
            Info = new MonitorInfo("/");
        }

        public MonitorIndexViewModel(MonitorModel monitor, MonitorInfo monitorInfo)
        {
            Monitor = monitor;
            Info = monitorInfo;
        }
    }
}