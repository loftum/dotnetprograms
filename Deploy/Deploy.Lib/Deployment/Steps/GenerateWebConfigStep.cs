using System.IO;
using Deploy.Lib.ConfigGenerating;

namespace Deploy.Lib.Deployment.Steps
{
    public class GenerateWebConfigStep : DeploymentStepBase
    {
        private readonly ConfigValuesReader _reader;
        private readonly ConfigValuesReplacer _replacer;
        private const string WebConfigName = "web.config";

        public GenerateWebConfigStep(DeployParameters parameters, ConfigValuesReader reader, ConfigValuesReplacer replacer) 
            : base(parameters, "Generate web.config")
        {
            _reader = reader;
            _replacer = replacer;
        }

        protected override DeploymentStepStatus DoExecute()
        {
            var configValues = _reader.GetValues(Parameters.NewWebConfigPath);
            _replacer.ReplaceIn(Path.Combine(Parameters.DestinationFolder, WebConfigName));
            return Status;
        }
    }
}
