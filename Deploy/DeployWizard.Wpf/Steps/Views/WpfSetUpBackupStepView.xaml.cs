using System;
using Deploy.Lib.Deployment.Profiles;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Wpf.Steps.Views
{
    public partial class WpfSetUpBackupStepView : ISetUpBackupStepView
    {
        private BackupSettings _settings;

        public BackupSettings Settings
        {
            get
            {
                return _settings;
            }
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
                .ToTextBox(BackupFolderInput);
            Binder.Bind(_settings, "Skip")
                .ToCheckBox(SkipBox);
        }

        public WpfSetUpBackupStepView()
        {
            InitializeComponent();
        }

        private void BackupFolderInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            
        }
    }
}
