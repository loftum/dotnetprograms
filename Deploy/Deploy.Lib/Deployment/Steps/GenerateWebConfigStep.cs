using Deploy.Lib.ConfigGenerating;

namespace Deploy.Lib.Deployment.Steps
{
    public class GenerateWebConfigStep : DeploymentStepBase
    {
        private ConfigValuesReader _reader;
        private ConfigValuesReplacer _replacer;

        public GenerateWebConfigStep(DeployParameters parameters, ConfigValuesReader reader, ConfigValuesReplacer replacer) 
            : base(parameters, "Generate web.config")
        {
            _reader = reader;
            _replacer = replacer;
        }

        public override DeploymentStepStatus Execute()
        {
            var configValues = _reader.GetValues(Parameters.ConfigFilePath);
            _replacer.ReplaceIn(Parameters.WebConfigPath);

            return new DeploymentStepStatus(true, DeploymentStepStatus.Ok);
        }
    }
}
