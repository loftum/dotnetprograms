using System.Collections.Generic;
using System.Linq;
using BuildMonitor.Common.ExtensionMethods;
using BuildMonitor.Lib.Configuration;
using BuildMonitor.Lib.Data;
using BuildMonitor.Lib.Model.Build;

namespace BuildMonitor.Lib.Api.TeamCity
{
    public class TeamCityService : ITeamCityService
    {
        private readonly TeamCityRestUrls _urls;
        private readonly BuildServerConfig _buildServerConfig;
        private readonly IHttpReader _httpReader;

        public TeamCityService(IBuildMonitorRepo repo, IHttpReader httpReader)
        {
            _httpReader = httpReader;
            _buildServerConfig = repo.GetBuildServerConfig();
            _urls = new TeamCityRestUrls(_buildServerConfig.Host);
        }

        public BuildModel GetLatestBuild(string buildTypeId)
        {
            var rest = new TeamCityRestUrls(_buildServerConfig.Host);
            var buildId = GetLatestBuildIdFor(buildTypeId);
            var latestBuild = ReadJson(rest.BuildPathTo(buildId)).FromJsonTo<TeamCityBuild>();
            return latestBuild.ToBuildModel();
        }

        private string GetLatestBuildIdFor(string buildTypeId)
        {
            var rest = new TeamCityRestUrls(_buildServerConfig.Host);
            var json = ReadJson(rest.LatestBuildOf(buildTypeId));

            var builds = json.FromJsonTo<KjempemongisTeamCityBuilds>();
            return builds.build.First().ToBuildModel().Id;
        }

        public BuildServerModel GetBuildServer()
        {
            var buildServer = new BuildServerModel(_buildServerConfig);
            var vcsRoots = GetVcsRoots();
            foreach (var projectId in _buildServerConfig.ProjectIds)
            {
                var project = GetProject(projectId);
                project.VcsRoot = vcsRoots.FirstOrDefault(r => projectId.Equals(r.ProjectId));
                buildServer.Projects.Add(project);
            }
            return buildServer;
        }

        private IList<VcsRootModel> GetVcsRoots()
        {
            var url = new TeamCityRestUrls(_buildServerConfig.Host);

            var roots = new List<VcsRootModel>();
            var json = ReadJson(url.VcsRootsPath);
            var rootIds = json.FromJsonTo<KjempemongisTeamCityVcsRoots>();
            foreach (var rootId in rootIds.vcsRoot.Select(r => r.id))
            {
                var root = ReadJson(url.VcsRootPathTo(rootId)).FromJsonTo<TeamCityVcsRoot>();
                roots.Add(root.ToVcsRootModel());
            }
            return roots;
        }

        public IEnumerable<ProjectModel> GetProjects()
        {
            return GetProjectsFor(_buildServerConfig);
        }

        public IEnumerable<ProjectModel> GetProjectsFor(BuildServerConfig buildServer)
        {
            var url = new TeamCityRestUrls(buildServer.Host).ProjectPath;
            var projects = _httpReader.ReadJsonBasichAuth(url, buildServer.Username, buildServer.Password)
                .FromJsonTo<KjempemongisTeamCityProjects>();
            return projects.project.Select(p => p.ToProjectModel());
        }

        public ProjectModel GetProject(string projectId)
        {
            var json = ReadJson(_urls.ProjectPathTo(projectId));
            var response = json.FromJsonTo<TeamCityProject>();
            return response.ToProjectModel();
        }

        private string ReadJson(string url)
        {
            return _httpReader.ReadJsonBasichAuth(url, _buildServerConfig.Username, _buildServerConfig.Password);
        }
    }
}