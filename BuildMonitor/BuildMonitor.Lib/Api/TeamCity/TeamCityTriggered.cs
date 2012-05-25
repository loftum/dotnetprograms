namespace BuildMonitor.Lib.Api.TeamCity
{
    public class TeamCityTriggered
    {
        public string date { get; set; }
        public TeamCityUser user { get; set; }

        public TeamCityTriggered()
        {
            user = new TeamCityUser();
        }
    }
}