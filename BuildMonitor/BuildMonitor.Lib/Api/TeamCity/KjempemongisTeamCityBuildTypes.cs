namespace BuildMonitor.Lib.Api.TeamCity
{
    public class KjempemongisTeamCityBuildTypes
    {
        public TeamCityBuildType[] buildType { get; set; }

        public KjempemongisTeamCityBuildTypes()
        {
            buildType = new TeamCityBuildType[0];
        }
    }
}