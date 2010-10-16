using System.Windows.Media;
using Deploy.Lib.Deployment.Profiles;
using Deploy.Lib.Validation;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Wpf.Steps.Views
{
    public partial class WpfSetUpGenerateWebConfigStepView : IGenerateWebConfigStepView
    {
        private WebConfigSettings _settings;
        private readonly IValidator<string> _validator = new FilePathValidator();

        public WebConfigSettings Settings
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
            Binder.Bind(_settings, "NewWebConfigPath")
                .WithTargetNullValue(string.Empty)
                .ToTextBox(NewWebConfigInput);
            Binder.Bind(_settings, "Skip")
                .ToCheckBox(SkipBox);
        }

        public WpfSetUpGenerateWebConfigStepView()
        {
            InitializeComponent();
            ValidateAll();
        }

        public void ValidateAll()
        {
            if (!_validator.IsValid(NewWebConfigInput.Text))
            {
                NewWebConfigInput.Background = Brushes.IndianRed;
            }
            else
            {
                NewWebConfigInput.Background = Brushes.AliceBlue;
            }
        }

        private void NewWebConfigInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ValidateAll();
        }
    }
}
