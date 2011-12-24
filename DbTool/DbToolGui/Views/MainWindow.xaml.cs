using System;
using System.Windows;
using System.Windows.Input;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Ui.Highlighting;
using DbTool.Lib.Ui.Worksheet;
using DbToolGui.ExtensionMethods;
using DbToolGui.Highlighting;
using DbToolGui.Modules;
using DbToolGui.ViewModels;
using Ninject;

namespace DbToolGui.Views
{
    public partial class MainWindow
    {
        private readonly IDbToolConfig _config;
        private readonly MainViewModel _viewModel;
        private readonly ISyntaxHighlighter _highlighter;
        private readonly IWorksheetManager _worksheetManager;

        public MainWindow()
        {
            InitializeComponent();
            var kernel = new StandardKernel(new SettingsModule(), new ViewModelModule(), new DatabaseModule());
            _viewModel = kernel.Get<MainViewModel>();
            DataContext = _viewModel;
            var factory = kernel.Get<ISyntaxHighlighterFactory>();
            _highlighter = factory.CreateFor(EditorBox, Dispatcher);
            _highlighter.StartHighlight();

            _config = kernel.Get<IDbToolConfig>();
            _worksheetManager = kernel.Get<IWorksheetManager>();
            EditorBox.AppendText(_worksheetManager.Read());
        }

        private void EditorBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5 || (e.Key.In(Key.Return, Key.Enter) && (Keyboard.Modifiers & ModifierKeys.Control) > 0))
            {
                _viewModel.EditorText = EditorBox.GetSelectedOrAllText();
                _viewModel.ExecuteCommand.Execute(sender);
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            _highlighter.StopHighlight();
            try
            {
                _config.SaveSettings();
                _worksheetManager.Save(EditorBox.GetAllText());
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Could not save settings: {0}", ex.Message), "Could not save settings", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
