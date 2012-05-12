namespace BuildMonitor.Lib.Api.TeamCity
{
    public class KjempemongisDusteTeamCityBuilds
    {
        public TeamCityBuild[] build { get; set; }

        public KjempemongisDusteTeamCityBuilds()
        {
            build = new TeamCityBuild[0];
        }
    }
}