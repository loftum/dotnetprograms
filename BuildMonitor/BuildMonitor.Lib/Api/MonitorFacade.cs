using System.Collections.Generic;
using System.Linq;
using BuildMonitor.Common.ExtensionMethods;
using BuildMonitor.Lib.Api.TeamCity;
using BuildMonitor.Lib.Configuration;
using BuildMonitor.Lib.Data;
using BuildMonitor.Lib.Model;
using BuildMonitor.Lib.Model.Build;

namespace BuildMonitor.Lib.Api
{
    public class MonitorFacade : IMonitorFacade
    {
        private readonly IBuildMonitorRepo _repo;
        private readonly ITeamCityService _teamCityService;
        private readonly IHttpReader _httpReader;

        public MonitorFacade(IBuildMonitorRepo repo,
            ITeamCityService teamCityService,
            IHttpReader httpReader)
        {
            _repo = repo;
            _teamCityService = teamCityService;
            _httpReader = httpReader;
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
                       ? new MonitorModel { BuildServer = _teamCityService.GetBuildServer() }
                       : new MonitorModel();
        }


        public BuildModel GetLatestBuild(string buildTypeId)
        {
            return _teamCityService.GetLatestBuild(buildTypeId);
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
            return buildServer.HasCredentials
                ? _teamCityService.GetProjectsFor(buildServer)
                : Enumerable.Empty<ProjectModel>();

        }

        public string ReadJson(string url)
        {
            var config = GetBuildServerConfig();
            return _httpReader.ReadJsonBasichAuth(url, config.Username, config.Password);
        }
    }
}