using System.Windows.Media;
using DeployWizard.Lib.Models;
using DeployWizard.Lib.Steps.Views;
using DeployWizard.Lib.Validation;

namespace DeployWizard.Wpf.Steps.Views
{
    public partial class WpfSelectPackageStepView : ISelectPackageStepView
    {
        private readonly IValidator<string> _validator = new FilePathValidator();

        private WizardModel _model;

        public WizardModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                Bind();
            }
        }

        public WpfSelectPackageStepView()
        {
            InitializeComponent();
            ValidateAll();
        }

        private void Bind()
        {
            Binder.Bind(_model, "Package")
                .WithTargetNullValue(string.Empty)
                .ToTextBox(PackageInput);
        }

        public void ValidateAll()
        {
            var text = PackageInput.Text;
            if (string.IsNullOrEmpty(text) || !_validator.IsValid(text))
            {
                PackageInput.Background = Brushes.IndianRed;
            }
            else
            {
                PackageInput.Background = Brushes.AliceBlue;
            }
        }

        private void PackageInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ValidateAll();
        }
    }
}
