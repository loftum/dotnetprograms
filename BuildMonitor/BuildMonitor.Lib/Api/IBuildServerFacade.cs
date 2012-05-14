using System.Collections.Generic;
using BuildMonitor.Lib.Model;
using BuildMonitor.Lib.Model.Build;

namespace BuildMonitor.Lib.Api
{
    public interface IBuildServerFacade
    {
        IEnumerable<BuildServerModel> GetBuildServers();
    }
}