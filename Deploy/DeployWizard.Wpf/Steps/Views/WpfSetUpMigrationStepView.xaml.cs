using System.Collections.Generic;
using System.Windows.Controls;
using Deploy.Lib.Deployment.Profiles;
using DeployWizard.Lib.Events.Connections;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Wpf.Steps.Views
{
    public partial class WpfSetUpMigrationStepView : ISetUpMigrationStepView
    {
        public event TestConnectionEvent TestConnection;

        private MigrateDatabaseSettings _settings;
        public MigrateDatabaseSettings Settings
        {
            get { return _settings; }
            set {
                _settings = value;
                Bind();
            }
        }

        private void Bind()
        {
            Binder.Bind(_settings, "ConnectionString")
                .WithTargetNullValue(string.Empty)
                .ToTextBox(ConnectionStringBox);
            Binder.Bind(_settings, "MigrationAssemblyPath")
                .WithTargetNullValue(string.Empty)
                .ToTextBox(AssemblyNameBox);
            Binder.Bind(_settings, "Skip")
                .ToCheckBox(SkipBox);
            DatabaseTypeBox.SelectedItem = _settings.DatabaseType;
        }

        public void ShowConnectionStatus(string status)
        {
            ConnectionStatusBlock.Text = status;
        }

        public WpfSetUpMigrationStepView(IEnumerable<string> databaseTypes)
        {
            InitializeComponent();
            DatabaseTypeBox.ItemsSource = databaseTypes;
            DatabaseTypeBox.SelectedIndex = 0;
            DatabaseTypeBox.SelectionChanged += DataBaseTypeChanged;
        }

        private void DataBaseTypeChanged(object sender, SelectionChangedEventArgs e)
        {
            _settings.DatabaseType = (string) DatabaseTypeBox.SelectedItem;
        }

        public void ValidateAll()
        {
            
        }

        private void TestConnectionButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (TestConnection != null)
            {
                TestConnection.Invoke(sender, new ConnectionEventArgs(ConnectionStringBox.Text));
            }
        }
    }
}
