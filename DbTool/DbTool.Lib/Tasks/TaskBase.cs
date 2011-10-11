using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;

namespace DbTool.Lib.Tasks
{
    public abstract class TaskBase
    {
        protected IDbToolLogger Logger;
        protected IDbToolSettings Settings;

        protected TaskBase(IDbToolLogger logger, IDbToolSettings settings)
        {
            Logger = logger;
            Settings = settings;
        }
    }
}