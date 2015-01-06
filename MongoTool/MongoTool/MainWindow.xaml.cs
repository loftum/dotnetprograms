using System;
using System.Windows;
using System.Windows.Input;
using MongoTool.Core.Syntax;
using MongoTool.Core.WorkSheets;
using MongoTool.ExtensionMethods;
using MongoTool.Ioc;
using MongoTool.ViewModels;

namespace MongoTool
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;
        private readonly IWorkSheetManager _workSheetManager;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = Di.Container.GetInstance<MainViewModel>();
            _workSheetManager = Di.Container.GetInstance<IWorkSheetManager>();
            DataContext = _viewModel;
            EditorBox.SyntaxParser = Di.Container.GetInstance<ISyntaxParser>();
            EditorBox.Text = _workSheetManager.Load();
        }

        private void EditorBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F5)
            {
                _viewModel.Input = EditorBox.GetSelectedOrAllText();
                _viewModel.ExecuteCommand.Execute(sender);
            }
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            try
            {
                SaveWorkSheet(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Could not save settings: {0}", ex.Message), "Could not save settings", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveWorkSheet(bool showStatus)
        {
            var text = EditorBox.Text ?? "";
            _workSheetManager.Save(text.TrimEnd());
            if (showStatus)
            {
                _viewModel.StatusText = "Worksheet saved";
            }
        }

        private void EditorBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.S && (Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
            {
                SaveWorkSheet(true);
            }
        }
    }
}
