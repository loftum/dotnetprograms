using System.Collections.Generic;
using BuildMonitor.Lib.Model;
using BuildMonitor.Lib.Model.Build;

namespace BuildMonitor.Lib.Api
{
    public class BuildServerFacade : IBuildServerFacade
    {


        public IEnumerable<BuildServerModel> GetBuildServers()
        {
            yield break;
        }
    }
}