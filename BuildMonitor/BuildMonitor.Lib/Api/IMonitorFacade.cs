using BuildMonitor.Lib.Configuration;
using BuildMonitor.Lib.Model;

namespace BuildMonitor.Lib.Api
{
    public interface IMonitorFacade
    {
        MonitorModel GetMonitor();
        BuildModel GetLatestBuild(string buildTypeId);
        MonitorConfiguration GetConfiguration();
        void SaveConfiguration(MonitorConfiguration config);
    }
}