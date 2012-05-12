using BuildMonitor.Lib.Model;

namespace BuildMonitor.Models.Monitor
{
    public class MonitorIndexViewModel
    {
        public MonitorModel Monitor { get; private set; }

        public MonitorIndexViewModel()
        {
            Monitor = new MonitorModel();
        }

        public MonitorIndexViewModel(MonitorModel monitor)
        {
            Monitor = monitor;
        }
    }
}