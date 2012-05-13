using BuildMonitor.Lib.Configuration;

namespace BuildMonitor.Lib.Data
{
    public interface IBuildMonitorRepo
    {
        MonitorConfiguration GetConfig();
        BuildServerConfig GetBuildServerConfig();
        void Save();
        void Revert();
    }
}