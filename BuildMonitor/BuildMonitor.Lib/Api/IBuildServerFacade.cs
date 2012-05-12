using System.Collections.Generic;
using BuildMonitor.Lib.Model;

namespace BuildMonitor.Lib.Api
{
    public interface IBuildServerFacade
    {
        IEnumerable<BuildServerModel> GetBuildServers();
    }
}