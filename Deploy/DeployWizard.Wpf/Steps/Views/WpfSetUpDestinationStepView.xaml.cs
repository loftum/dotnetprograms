using System.Windows.Media;
using Deploy.Lib.Deployment.Profiles;
using DeployWizard.Lib.Events.FileSystem;
using DeployWizard.Lib.Steps.Views;
using DeployWizard.Lib.Validation;

namespace DeployWizard.Wpf.Steps.Views
{
    public partial class WpfSetUpDestinationStepView : ISetUpDestinationStepView
    {
        public event CreateDirectoryEvent CreateDirectory;

        private DestinationSettings _settings;
        public DestinationSettings Settings
        {
            get { return _settings; }
            set 
            { 
                _settings = value;
                Bind();
            }
        }
        private readonly IValidator<string> _validator = new DirectoryPathValidator();

        private void Bind()
        {
            Binder.Bind(_settings, "Folder")
                .WithTargetNullValue(string.Empty)
                .ToTextBox(DestinationInput);
            Binder.Bind(_settings, "Skip")
                .ToCheckBox(SkipBox);
        }

        public WpfSetUpDestinationStepView()
        {
            InitializeComponent();
        }

        public void ValidateAll()
        {
            if (_validator.IsValid(DestinationInput.Text))
            {
                DestinationInput.Background = Brushes.AliceBlue;
            }
            else
            {
                DestinationInput.Background = Brushes.IndianRed;
            }
        }

        private void DestinationInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ValidateAll();
        }

        private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CreateDirectory(sender, new PathEventArgs(DestinationInput.Text));
        }
    }
}
