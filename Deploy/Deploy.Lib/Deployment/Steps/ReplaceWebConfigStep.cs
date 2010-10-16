using System.IO;

namespace Deploy.Lib.Deployment.Steps
{
    public class ReplaceWebConfigStep : DeploymentStepBase
    {
        private const string WebConfigName = "web.config";
        private readonly string _webConfigPath;

        public ReplaceWebConfigStep(DeployParameters parameters)
            : base(parameters, "Replace web.config")
        {
            _webConfigPath = Path.Combine(parameters.DestinationFolder, WebConfigName);
        }

        protected override DeploymentStepStatus DoExecute()
        {
            File.Copy(Parameters.NewWebConfigPath, _webConfigPath, true);
            Status.AppendDetailsLine("Copying " + Parameters.NewWebConfigPath + " to " + _webConfigPath);
            Status.Status = DeploymentStepStatus.Ok;
            return Status;
        }
    }
}
