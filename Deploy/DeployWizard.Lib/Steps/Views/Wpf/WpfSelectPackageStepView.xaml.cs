using System;

namespace DeployWizard.Lib.Steps.Views.Wpf
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
