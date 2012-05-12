using BuildMonitor.Lib.Model;

namespace BuildMonitor.Lib.Api.TeamCity
{
    public class TeamCityBuildType
    {
        public string id { get; set; }
        public string name { get; set; }
        public string projectId { get; set; }
        public string projectName { get; set; }
        public string href { get; set; }
        public string webUrl { get; set; }

        public BuildTypeModel ToBuildType()
        {
            return new BuildTypeModel
                       {
                           Id = id,
                           Name = name,
                           ProjectId = projectId,
                       };
        }
    }
}