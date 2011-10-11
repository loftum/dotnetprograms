using DbTool.Lib.Configuration;
using DbTool.Lib.Logging;
using DbTool.Lib.Tasks;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;

namespace DbTool.Lib.SqlServer.Tasks
{
    public abstract class SqlServerProgressTaskBase : TaskBase
    {
        protected SqlServerProgressTaskBase(IDbToolLogger logger, IDbToolSettings settings)
            : base(logger, settings)
        {
        }

        public event TaskProgressEventHandler PercentComplete;
        public event TaskProgressCompleteEventHandler Complete;

        protected void HandleComplete(object sender, ServerMessageEventArgs e)
        {
            if (Complete != null)
            {
                Complete(sender, new TaskProgressCompleteEventArgs());
            }
        }

        protected void HandlePercentComplete(object sender, PercentCompleteEventArgs e)
        {
            if (PercentComplete != null)
            {
                PercentComplete(sender, new TaskProgressEventArgs(e.Percent));
            }
        }
    }
}