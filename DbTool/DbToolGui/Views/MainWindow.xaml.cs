using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using DbToolGui.ExtensionMethods;
using DbToolGui.Highlighting;
using DbToolGui.Modules;
using DbToolGui.ViewModels;
using Ninject;

namespace DbToolGui.Views
{
    public partial class MainWindow
    {
        private readonly DbToolGuiViewModel _viewModel;
        private readonly ISyntaxHighlighter _highlighter;

        public MainWindow()
        {
            InitializeComponent();
            var kernel = new StandardKernel(new ViewModelModule(), new DatabaseModule());

            _viewModel = kernel.Get<DbToolGuiViewModel>();
            DataContext = _viewModel;
            _highlighter = new SyntaxHighlighter(EditorBox.Document);
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

        private void EditorBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (_highlighter != null)
            {
                EditorBox.TextChanged -= EditorBox_TextChanged;
                _highlighter.Highlight();
                EditorBox.TextChanged += EditorBox_TextChanged;
            }
        }
    }
}
