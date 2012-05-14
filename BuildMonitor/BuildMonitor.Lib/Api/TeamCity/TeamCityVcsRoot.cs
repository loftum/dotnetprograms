using BuildMonitor.Lib.Model;
using BuildMonitor.Lib.Model.Build;

namespace BuildMonitor.Lib.Api.TeamCity
{
    public class TeamCityVcsRoot
    {
        public string id { get; set; }
        public string name { get; set; }
        public string vcsName { get; set; }
        public bool shared { get; set; }
        public string status { get; set; }
        public string lastChecked { get; set; }
        public string href { get; set; }
        public TeamCityProject project { get; set; }

        public KjempemongisTeamCityProperties properties { get; set; }

        public TeamCityVcsRoot()
        {
            properties = new KjempemongisTeamCityProperties();
        }

        public VcsRootModel ToVcsRootModel()
        {
            return new VcsRootModel
                {
                    Id = id,
                    Name = name,
                    VcsName = vcsName,
                    //LastChecked = DateTime.ParseExact(lastChecked, "yyyyMMddhhmmssT", CultureInfo.InvariantCulture),
                    Href = href,
                    BranchName = properties["branchName"],
                    Project = project == null ? null : project.ToProjectModel()
                };
        }
    }
}