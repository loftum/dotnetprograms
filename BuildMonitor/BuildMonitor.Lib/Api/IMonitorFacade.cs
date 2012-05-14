using System.Collections.Generic;
using BuildMonitor.Lib.Configuration;
using BuildMonitor.Lib.Model;
using BuildMonitor.Lib.Model.Build;

namespace BuildMonitor.Lib.Api
{
    public interface IMonitorFacade
    {
        MonitorModel GetMonitor();
        BuildModel GetLatestBuild(string buildTypeId);
        BuildServerConfig GetBuildServerConfig();
        void SaveBuildServer(BuildServerConfig config);
        IEnumerable<ProjectModel> GetAvailableProjectsFor(BuildServerConfig buildServer);
        MonitorConfig GetConfig();
        string ReadJson(string url);
    }
}