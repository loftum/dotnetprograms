using Deploy.Lib.Deployment.Profiles;

namespace DeployWizard.Lib.Steps.Views.Wpf
{
    public partial class WpfSetUpDeployStatusStepView : ISetUpDeployStatusStepView
    {
        private DeployStatusSettings _settings;

        public DeployStatusSettings Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                Bind();
            }
        }

        private void Bind()
        {
            Binder.Bind(_settings, "Folder")
                .WithTargetNullValue(string.Empty)
                .ToTextBox(DeployStatusInput);
            Binder.Bind(_settings, "Skip")
                .ToCheckBox(SkipBox);
        }

        public WpfSetUpDeployStatusStepView()
        {
            InitializeComponent();
        }
    }
}
