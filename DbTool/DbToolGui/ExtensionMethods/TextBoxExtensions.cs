using System.Windows.Controls;
using DotNetPrograms.Common.ExtensionMethods;

namespace DbToolGui.ExtensionMethods
{
    public static class TextBoxExtensions
    {
        public static string GetSelectedOrAllText(this TextBox textBox)
        {
            var selectedText = textBox.SelectedText;
            return selectedText.IsNullOrEmpty() ? textBox.Text : selectedText;
        }
    }
}