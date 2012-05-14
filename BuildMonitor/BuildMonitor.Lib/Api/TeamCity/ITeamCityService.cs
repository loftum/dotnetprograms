using System.Collections.Generic;
using BuildMonitor.Lib.Configuration;
using BuildMonitor.Lib.Model.Build;

namespace BuildMonitor.Lib.Api.TeamCity
{
    public interface ITeamCityService
    {
        BuildServerModel GetBuildServer();
        BuildModel GetLatestBuild(string buildTypeId);
        IEnumerable<ProjectModel> GetProjects();
        ProjectModel GetProject(string projectId);
        IEnumerable<ProjectModel> GetProjectsFor(BuildServerConfig buildServer);
    }
}