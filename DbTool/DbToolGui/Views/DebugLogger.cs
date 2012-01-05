using System;
using System.Windows.Controls;

namespace DbToolGui.Views
{
    public class DebugLogger : IDebugLogger
    {
        private static DebugLogger _instance;
        public static DebugLogger Instance
        {
            get { return _instance ?? (_instance = new DebugLogger()); }
        }

        public TextBox TextBox { get; set; }

        public void Log(string text, params object[] args)
        {
            if (TextBox != null)
            {
                TextBox.AppendText(string.Format("{0}{1}", string.Format(text, args), Environment.NewLine));
                TextBox.ScrollToEnd();
            }
        }
    }
}