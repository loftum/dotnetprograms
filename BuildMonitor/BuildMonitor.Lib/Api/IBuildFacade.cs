using BuildMonitor.Lib.Model;

namespace BuildMonitor.Lib.Api
{
    public interface IBuildFacade
    {
        MonitorModel GetMonitor();
        BuildModel GetLatestBuild(string buildTypeId);
    }
}