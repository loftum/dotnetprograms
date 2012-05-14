using System.Linq;

namespace BuildMonitor.Lib.Api.TeamCity
{
    public class KjempemongisTeamCityProperties
    {
        public TeamCityProperty[] property { get; set; }

        public KjempemongisTeamCityProperties()
        {
            property = new TeamCityProperty[0];
        }

        public string this[string key]
        {
            get { return property.Where(p => p.name.Equals(key)).Select(p => p.value).FirstOrDefault(); }
        }
    }
}