using BuildMonitor.Common.DateAndTime;
using BuildMonitor.Lib.Model.Build;

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
        public string finishDate { get; set; }
        public string webUrl { get; set; }
        public TeamCityTriggered triggered { get; set; }

        public TeamCityBuild()
        {
            triggered = new TeamCityTriggered();
        }

        public BuildModel ToBuildModel()
        {
            return new BuildModel
                {
                    Id = id,
                    Number = number,
                    Status = status,
                    BuildTypeId = buildTypeId,
                    StartDate = DateTimeProvider.ParseWithTimeZone(startDate),
                    FinishDate = DateTimeProvider.ParseWithTimeZone(finishDate),
                    TriggeredDate = DateTimeProvider.ParseWithTimeZone(triggered.date),
                    TriggeredBy = triggered.user.ToBuildUserModel()
                };
        }
    }
}