using Newtonsoft.Json;

namespace BuildMonitor.Lib.Api.TeamCity
{
    public class KjempemongisTeamCityVcsRoots
    {
        [JsonProperty("vcs-root")]
        public TeamCityVcsRoot[] vcsRoot { get; set; }

        public KjempemongisTeamCityVcsRoots()
        {
            vcsRoot = new TeamCityVcsRoot[0];
        }
    }
}