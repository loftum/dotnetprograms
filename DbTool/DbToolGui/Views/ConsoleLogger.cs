using System;
using System.Windows.Controls;

namespace DbToolGui.Views
{
    public class ConsoleLogger
    {
        private static ConsoleLogger _instance;
        public static ConsoleLogger Instance
        {
            get { return _instance ?? (_instance = new ConsoleLogger()); }
        }

        public TextBox TextBox { get; set; }

        public void Log(string text, params object[] args)
        {
            if (TextBox != null)
            {
                TextBox.AppendText(string.Format("{0}{1}", string.Format(text, args), Environment.NewLine));
            }
        }
    }
}