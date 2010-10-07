using Deploy.Lib.ConfigGenerating;

namespace Deploy.Lib.Deployment.Steps
{
    public class GenerateWebConfigStep : DeploymentStepBase
    {
        private readonly ConfigValuesReader _reader;
        private readonly ConfigValuesReplacer _replacer;

        public GenerateWebConfigStep(DeployParameters parameters, ConfigValuesReader reader, ConfigValuesReplacer replacer) 
            : base(parameters, "Generate web.config")
        {
            _reader = reader;
            _replacer = replacer;
        }

        protected override DeploymentStepStatus DoExecute()
        {
            var configValues = _reader.GetValues(Parameters.NewWebConfigPath);
            _replacer.ReplaceIn(Parameters.WebConfigPath);
            return Status;
        }
    }
}
