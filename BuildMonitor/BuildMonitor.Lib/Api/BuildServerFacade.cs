using System.Collections.Generic;
using BuildMonitor.Lib.Model;

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