using BuildMonitor.Lib.Model;

namespace BuildMonitor.Lib.Api.TeamCity
{
    public class TeamCityBuild
    {
        public string id { get; set; }
        public string number { get; set; }
        public string status { get; set; }
        public string buildTypeId { get; set; }
        public string href { get; set; }
        public string startDate { get; set; }
        public string webUrl { get; set; }

        public BuildModel ToBuildModel()
        {
            return new BuildModel
                {
                    Id = id,
                    Number = number,
                    Status = status,
                    BuildTypeId = buildTypeId
                };
        }
    }
}