using System;
using System.Collections.Generic;
using System.Windows.Media;
using Deploy.Lib.Deployment.Profiles;
using Deploy.Lib.Validation;
using DeployWizard.Lib.AutoComplete;
using DeployWizard.Lib.Events.FileSystem;
using DeployWizard.Lib.Steps.Views;

namespace DeployWizard.Wpf.Steps.Views
{
    public partial class WpfSetUpBackupStepView : ISetUpBackupStepView
    {
        public event CreateDirectoryEvent CreateDirectory;
        private readonly AutoCompleteSuggestions _suggestions;

        private BackupSettings _settings;
        private readonly IValidator<string> _validator = new DirectoryPathValidator();
        private readonly IAutoCompleteProvider _autoCompleteProvider;


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

        public WpfSetUpBackupStepView(IAutoCompleteProvider autoCompleteProvider)
        {
            _suggestions = new AutoCompleteSuggestions();
            DataContext = _suggestions;
            InitializeComponent();
            _autoCompleteProvider = autoCompleteProvider;
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
            UpdateSuggestions(BackupFolderInput.Text);
        }

        private void UpdateSuggestions(string input)
        {
            _suggestions.Suggestions = _autoCompleteProvider.GetSuggestionsFor(input);
            PrintSuggestions();
        }

        private void PrintSuggestions()
        {
            Console.WriteLine("New suggestions:");
            foreach (var suggestion in _suggestions.Suggestions)
            {
                Console.WriteLine(suggestion);
            }
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

        private void FolderComboBox_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ValidateAll();
            UpdateSuggestions(FolderComboBox.Text);
        }
    }
}
