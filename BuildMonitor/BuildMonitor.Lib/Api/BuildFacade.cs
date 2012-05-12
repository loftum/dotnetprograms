using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using BuildMonitor.Common.ExtensionMethods;
using BuildMonitor.Lib.Api.TeamCity;
using BuildMonitor.Lib.Configuration;
using BuildMonitor.Lib.Model;
using Newtonsoft.Json;

namespace BuildMonitor.Lib.Api
{
    public class BuildFacade : IBuildFacade
    {
        private readonly IBuildMonitorSettings _settings;

        public BuildFacade(IBuildMonitorSettings settings)
        {
            _settings = settings;
        }

        public MonitorModel GetMonitor()
        {
            var projects = GetProjects();
            return new MonitorModel(projects, _settings.BuildHost);
        }

        public BuildModel GetLatestBuild(string buildTypeId)
        {
            var settings = _settings.BuildServer;
            var info = new MonitorInfo(settings.Host);
            var json = ReadJson(info.LatestBuildOf(buildTypeId));
            var builds = JsonConvert.DeserializeObject<KjempemongisDusteTeamCityBuilds>(json);
            return builds.build.First().ToBuildModel();
        }

        private IEnumerable<ProjectModel> GetProjects()
        {
            return _settings.BuildServer.ProjectIds.Select(GetProject).ToList();
        }

        private ProjectModel GetProject(string projectId)
        {
            var settings = _settings.BuildServer;
            var info = new MonitorInfo(settings.Host);
            var json = ReadJson(info.ProjectPathTo(projectId));
            var response = JsonConvert.DeserializeObject<TeamCityProject>(json);
            return response.ToProjectModel();
        }

        private string ReadJson(string url)
        {
            var settings = _settings.BuildServer;
            var request = (HttpWebRequest)WebRequest.Create(url);
            var auth = string.Format("{0}:{1}", settings.Username, settings.Password).ToBase64();
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