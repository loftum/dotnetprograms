using System.Windows.Media;
using Deploy.Lib.Deployment.Profiles;
using Deploy.Lib.Validation;
using DeployWizard.Lib.Events.FileSystem;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Wpf.Steps.Views
{
    public partial class WpfSetUpBackupStepView : ISetUpBackupStepView
    {
        public event CreateDirectoryEvent CreateDirectory;

        private BackupSettings _settings;
        private readonly IValidator<string> _validator = new DirectoryPathValidator();

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

        public WpfSetUpBackupStepView()
        {
            InitializeComponent();
            ValidateAll();
        }

        private void Bind()
        {
            Binder.Bind(_settings, "Folder")
                .WithTargetNullValue(string.Empty)
                .ToTextBox(BackupFolderInput);
            Binder.Bind(_settings, "Skip")
                .ToCheckBox(SkipBox);
        }

        private void BackupFolderInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ValidateAll();
        }

        private void CreateButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            CreateDirectory(sender, new PathEventArgs(BackupFolderInput.Text));
        }

        public void ValidateAll()
        {
            var text = BackupFolderInput.Text;
            if (_validator.IsValid(text))
            {
                BackupFolderInput.Background = Brushes.AliceBlue;
                CreateButton.IsEnabled = false;
            }
            else
            {
                BackupFolderInput.Background = Brushes.IndianRed;
                CreateButton.IsEnabled = true;
            }
        }
    }
}
