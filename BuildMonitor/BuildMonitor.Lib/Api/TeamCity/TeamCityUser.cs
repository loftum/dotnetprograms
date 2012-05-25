using BuildMonitor.Lib.Model.Build;

namespace BuildMonitor.Lib.Api.TeamCity
{
    public class TeamCityUser
    {
        public string id { get; set; }
        public string name { get; set; }
        public string username { get; set; }
        public string href { get; set; }

        public BuildUser ToBuildUserModel()
        {
            return new BuildUser
                {
                    Id = id,
                    Href = href,
                    Username = username,
                    Name = name
                };
        }
    }
}