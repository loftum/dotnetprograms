using System.Windows.Controls;

namespace MongoTool.ExtensionMethods
{
    public static class TextBoxExtensions
    {
        public static string GetSelectedOrAllText(this TextBox textBox)
        {
            var selectedText = textBox.SelectedText;
            return string.IsNullOrEmpty(selectedText) ? textBox.Text : selectedText;
        }
    }
}