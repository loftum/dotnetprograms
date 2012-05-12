using System.IO;
using System.Linq;
using System.Net;
using BuildMonitor.Common.ExtensionMethods;
using BuildMonitor.Lib.Api.TeamCity;
using BuildMonitor.Lib.Configuration;
using BuildMonitor.Lib.Data;
using BuildMonitor.Lib.Model;
using Newtonsoft.Json;

namespace BuildMonitor.Lib.Api
{
    public class MonitorFacade : IMonitorFacade
    {
        private readonly IBuildMonitorRepo _repo;

        public MonitorFacade(IBuildMonitorRepo repo)
        {
            _repo = repo;
        }

        public MonitorConfiguration GetConfiguration()
        {
            return _repo.GetConfig();
        }

        public void SaveConfiguration(MonitorConfiguration config)
        {
            _repo.Save(config);
        }

        public MonitorModel GetMonitor()
        {
            var config = _repo.GetConfig().BuildServerConfig;

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
            var info = new MonitorInfo(config.BuildServerConfig.Host);
            var json = ReadJson(info.LatestBuildOf(buildTypeId));
            var builds = JsonConvert.DeserializeObject<KjempemongisDusteTeamCityBuilds>(json);
            return builds.build.First().ToBuildModel();
        }

        private ProjectModel GetProject(string projectId)
        {
            var config = GetConfiguration();
            var info = new MonitorInfo(config.BuildServerConfig.Host);
            var json = ReadJson(info.ProjectPathTo(projectId));
            var response = JsonConvert.DeserializeObject<TeamCityProject>(json);
            return response.ToProjectModel();
        }

        private string ReadJson(string url)
        {
            var config = GetConfiguration().BuildServerConfig;
            var request = (HttpWebRequest)WebRequest.Create(url);
            var auth = string.Format("{0}:{1}", config.Username, config.Password).ToBase64();
            request.Headers["Authorization"] = string.Format("Basic {0}", auth);
            request.Method = "GET";
            request.Accept = "application/json";
            var response = (HttpWebResponse)request.GetResponse();

            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }
    }
}