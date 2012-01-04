using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public abstract class TaskCommandBase : CommandBase
    {
        protected ITaskFactory TaskFactory { get; private set; }

        protected TaskCommandBase(string name, IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base(name, logger, settings)
        {
            TaskFactory = taskFactory;
        }
    }
}