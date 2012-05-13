using System.Collections.Generic;
using BuildMonitor.Lib.Configuration;
using BuildMonitor.Lib.Model;

namespace BuildMonitor.Lib.Api
{
    public interface IMonitorFacade
    {
        MonitorModel GetMonitor();
        BuildModel GetLatestBuild(string buildTypeId);
        BuildServerConfig GetBuildServerConfig();
        void SaveBuildServer(BuildServerConfig config);
        IEnumerable<ProjectModel> GetAvailableProjectsFor(BuildServerConfig buildServer);
    }
}