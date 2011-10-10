using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;

namespace DbTool.Commands
{
    public abstract class TaskCommandBase : CommandBase
    {
        protected ITaskFactory TaskFactory { get; private set; }

        protected TaskCommandBase(string name, string usage, string example, IDbToolLogger logger, IDbToolSettings settings, ITaskFactory taskFactory)
            : base(name, usage, example, logger, settings)
        {
            TaskFactory = taskFactory;
        }
    }
}