using System.Windows.Controls;
using System.Windows.Documents;
using DbTool.Lib.ExtensionMethods;

namespace DbToolGui.ExtensionMethods
{
    public static class RichTextBoxExtensions
    {
        public static string GetAllText(this RichTextBox textBox)
        {
            textBox.ShouldNotBeNull("textBox");
            var document = textBox.Document;
            var textRange = new TextRange(document.ContentStart, document.ContentEnd);
            return textRange.Text;
        }

        public static string GetSelectedOrAllText(this RichTextBox textBox)
        {
            textBox.ShouldNotBeNull("textBox");
            return textBox.Selection.IsEmpty
                ? textBox.GetAllText()
                : textBox.Selection.Text;
        }

        public static void ClearAllText(this RichTextBox textBox)
        {
            textBox.SetText(string.Empty);
        }

        public static void SetText(this RichTextBox textBox, string value)
        {
            textBox.ShouldNotBeNull("textBox");
            var document = textBox.Document;
            var textRange = new TextRange(document.ContentStart, document.ContentEnd);
            textRange.Text = value;
        }
    }
}