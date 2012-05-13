using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using BuildMonitor.Common.ExtensionMethods;
using BuildMonitor.Lib.Api.TeamCity;
using BuildMonitor.Lib.Configuration;
using BuildMonitor.Lib.Data;
using BuildMonitor.Lib.Model;

namespace BuildMonitor.Lib.Api
{
    public class MonitorFacade : IMonitorFacade
    {
        private readonly IBuildMonitorRepo _repo;

        public MonitorFacade(IBuildMonitorRepo repo)
        {
            _repo = repo;
        }

        public BuildServerConfig GetBuildServerConfig()
        {
            return _repo.GetBuildServerConfig();
        }

        public MonitorConfig GetConfig()
        {
            return _repo.GetConfig();
        }

        public void SaveBuildServer(BuildServerConfig config)
        {
            SetPasswordFor(config);
            config.Validate().OrThrow();
            var existing = _repo.GetBuildServerConfig();
            existing.UpdateFrom(config);
            _repo.Save();
        }

        public MonitorModel GetMonitor()
        {
            var config = _repo.GetBuildServerConfig();

            return config.IsValid
                       ? new MonitorModel { BuildServer = GetBuildServer(config) }
                       : new MonitorModel();
        }

        private BuildServerModel GetBuildServer(BuildServerConfig config)
        {
            var buildServer = new BuildServerModel(config);
            foreach (var projectId in config.ProjectIds)
            {
                buildServer.Projects.Add(GetProject(projectId));
            }
            return buildServer;
        }

        public BuildModel GetLatestBuild(string buildTypeId)
        {
            var config = _repo.GetConfig();
            var rest = new TeamCityRestUrls(config.BuildServerConfig.Host);
            var json = ReadJson(rest.LatestBuildOf(buildTypeId));
            var builds = json.FromJsonTo<KjempemongisTeamCityBuilds>();
            return builds.build.First().ToBuildModel();
        }

        private void SetPasswordFor(BuildServerConfig buildServer)
        {
            if (buildServer.Password.IsNullOrEmpty())
            {
                var existing = _repo.GetConfig();
                buildServer.Password = existing.BuildServerConfig.Password;
            }
        }

        public IEnumerable<ProjectModel> GetAvailableProjectsFor(BuildServerConfig buildServer)
        {
            SetPasswordFor(buildServer);
            if (!buildServer.HasCredentials)
            {
                return Enumerable.Empty<ProjectModel>();
            }

            var url = new TeamCityRestUrls(buildServer.Host).ProjectPath;
            var projects = ReadJson(url, buildServer.Username, buildServer.Password).FromJsonTo<KjempemongisTeamCityProjects>();
            return projects.project.Select(p => p.ToProjectModel());
        }

        private ProjectModel GetProject(string projectId)
        {
            var config = GetBuildServerConfig();
            var info = new TeamCityRestUrls(config.Host);
            var json = ReadJson(info.ProjectPathTo(projectId));
            var response = json.FromJsonTo<TeamCityProject>();
            return response.ToProjectModel();
        }

        private string ReadJson(string url)
        {
            var config = GetBuildServerConfig();
            return ReadJson(url, config.Username, config.Password);
        }

        private static string ReadJson(string url, string username, string password)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var auth = string.Format("{0}:{1}", username, password).ToBase64();
            request.Headers["Authorization"] = string.Format("Basic {0}", auth);
            request.Method = "GET";
            request.Accept = "application/json";
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var stream = response.GetResponseStream())
                {
                    if (stream == null)
                    {
                        return string.Empty;
                    }
                    using (var reader = new StreamReader(stream))
                    {
                        return reader.ReadToEnd();
                    }    
                }
            }
        }
    }
}