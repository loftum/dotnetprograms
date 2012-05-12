namespace BuildMonitor.Lib.Api.TeamCity
{
    public class KjempemongisTeamCityBuilds
    {
        public TeamCityBuild[] build { get; set; }

        public KjempemongisTeamCityBuilds()
        {
            build = new TeamCityBuild[0];
        }
    }
}