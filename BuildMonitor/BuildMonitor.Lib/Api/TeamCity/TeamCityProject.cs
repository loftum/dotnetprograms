using System.Linq;
using BuildMonitor.Lib.Model;

namespace BuildMonitor.Lib.Api.TeamCity
{
    public class TeamCityProject
    {
        public string id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string href { get; set; }
        public string projectId { get; set; }
        public string projectName { get; set; }
        public string webUrl { get; set; }

        public KjempemongisDusteTeamCityBuildTypes buildTypes { get; set; }

        public TeamCityProject()
        {
            buildTypes = new KjempemongisDusteTeamCityBuildTypes();
        }

        public ProjectModel ToProjectModel()
        {
            return new ProjectModel
                {
                    Id = id,
                    Description = description,
                    Name = name,
                    BuildTypes = buildTypes.buildType.Select(t => t.ToBuildType()).ToList()
                };
        }
    }
}