using System.IO;
using Deploy.Lib.Logging;

namespace Deploy.Lib.Deployment.Steps
{
    public class ReplaceWebConfigStep : DeploymentStepBase
    {
        private const string WebConfigName = "web.config";
        private readonly string _webConfigPath;

        public ReplaceWebConfigStep(DeployParameters parameters, IDeployLogger logger)
            : base(parameters, "Replace web.config", logger)
        {
            _webConfigPath = Path.Combine(parameters.DestinationFolder, WebConfigName);
        }

        protected override DeploymentStepStatus DoExecute()
        {
            if (Parameters.Profile.WebConfigSettings.Skip)
            {
                SetStatusSkipped();
                return Status;
            }
            File.Copy(Parameters.NewWebConfigPath, _webConfigPath, true);
            Status.AppendDetailsLine("Copying " + Parameters.NewWebConfigPath + " to " + _webConfigPath);
            Status.Status = DeploymentStepStatus.Ok;
            return Status;
        }
    }
}
