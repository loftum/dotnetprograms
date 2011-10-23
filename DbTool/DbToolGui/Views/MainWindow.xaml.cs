using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
using DbTool.Lib.Ui.Highlighting;
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
        }

        private void EditorBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5 || (e.Key.In(Key.Return, Key.Enter) && (Keyboard.Modifiers & ModifierKeys.Control) > 0))
            {
                _viewModel.EditorText = GetEditorText();
                _viewModel.ExecuteCommand.Execute(sender);
            }
        }

        private string GetEditorText()
        {
            if (EditorBox.Selection.IsEmpty)
            {
                var document = EditorBox.Document;
                var textRange = new TextRange(document.ContentStart, document.ContentEnd);
                return textRange.Text;    
            }
            return EditorBox.Selection.Text;
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            _highlighter.StopHighlight();
            try
            {
                _config.SaveSettings();
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Could not save settings: {0}", ex.Message), "Could not save settings", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
