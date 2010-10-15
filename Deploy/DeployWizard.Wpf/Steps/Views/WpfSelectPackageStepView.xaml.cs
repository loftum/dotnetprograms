using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Wpf.Steps.Views
{
    public partial class WpfSelectPackageStepView : ISelectPackageStepView
    {
        public WpfSelectPackageStepView()
        {
            InitializeComponent();
        }

        public string PackagePath
        {
            get { return PackageInput.Text; }
        }
    }
}
