using Deploy.Lib.Deployment;
using Migrator.Framework.Loggers;

namespace Deploy.Lib.Logging
{
    public class MigratorToDeployStatusAdapter : ILogWriter
    {
        private readonly DeploymentStepStatus _status;

        public MigratorToDeployStatusAdapter(DeploymentStepStatus status)
        {
            _status = status;
        }

        public void Write(string message, params object[] args)
        {
            _status.AppendDetails(string.Format(message, args));
        }

        public void WriteLine(string message, params object[] args)
        {
            _status.AppendDetailsLine(string.Format(message, args));
        }
    }
}