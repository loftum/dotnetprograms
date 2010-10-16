using System.Windows.Media;
using Deploy.Lib.Deployment.Profiles;
using Deploy.Lib.Validation;
using DeployWizard.Lib.Events.FileSystem;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Wpf.Steps.Views
{
    public partial class WpfSetUpDeployStatusStepView : ISetUpDeployStatusStepView
    {
        public event CreateDirectoryEvent CreateDirectory;

        private DeployStatusSettings _settings;
        private readonly IValidator<string> _validator = new DirectoryPathValidator();

        public DeployStatusSettings Settings
        {
            get { return _settings; }
            set
            {
                _settings = value;
                Bind();
            }
        }

        public WpfSetUpDeployStatusStepView()
        {
            InitializeComponent();
            ValidateAll();
        }

        private void Bind()
        {
            Binder.Bind(_settings, "Folder")
                .WithTargetNullValue(string.Empty)
                .ToTextBox(DeployStatusInput);
            Binder.Bind(_settings, "Skip")
                .ToCheckBox(SkipBox);
        }

        public void ValidateAll()
        {
            if (_validator.IsValid(DeployStatusInput.Text))
            {
                DeployStatusInput.Background = Brushes.AliceBlue;
                CreateDirectoryButton.IsEnabled = false;
            }
            else
            {
                DeployStatusInput.Background = Brushes.IndianRed;
                CreateDirectoryButton.IsEnabled = true;
            }
        }

        private void DeployStatusInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ValidateAll();
        }

        private void CreateDirectoryButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CreateDirectory(sender, new PathEventArgs(DeployStatusInput.Text));
        }
    }
}
