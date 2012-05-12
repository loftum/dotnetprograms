using BuildMonitor.Common.Urls;

namespace BuildMonitor.Lib.Api.TeamCity
{
    public class TeamCityRestUrls
    {
        public string BuildHost { get; private set; }
        public string BuildPath
        {
            get { return VirtualPath.Combine(BuildHost, "httpAuth/app/rest/builds"); }
        }

        public string ProjectPath
        {
            get { return VirtualPath.Combine(BuildHost, "httpAuth/app/rest/projects"); }
        }

        public TeamCityRestUrls(string buildHost)
        {
            BuildHost = buildHost;
        }

        public string ProjectPathTo(string projectId)
        {
            return VirtualPath.Combine(ProjectPath, string.Format("id:{0}", projectId));
        }

        public string LatestBuildOf(string buildTypeId)
        {
            return VirtualPath.Combine(BuildPath, string.Format("?locator=buildType:(id:{0}),count:1", buildTypeId));
        }
    }
}