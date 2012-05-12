using BuildMonitor.Lib.Configuration;

namespace BuildMonitor.Lib.Data
{
    public interface IBuildMonitorRepo
    {
        MonitorConfiguration GetConfig();
        void Save(MonitorConfiguration monitor);
    }
}