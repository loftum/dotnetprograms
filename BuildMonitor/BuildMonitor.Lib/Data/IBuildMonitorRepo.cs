using BuildMonitor.Lib.Configuration;

namespace BuildMonitor.Lib.Data
{
    public interface IBuildMonitorRepo
    {
        MonitorConfig GetConfig();
        BuildServerConfig GetBuildServerConfig();
        void Save();
        void Revert();
    }
}