using System.Text;

namespace Deploy.Lib.Logging
{
    public class Logger : ILogger
    {
        public event LogMessageEventHandler InfoMessageLogged;
        public event ProgressEventHandler ProgressChanged;
        
        public void Info(string name, string message)
        {
            if (InfoMessageLogged != null)
            {
                var toLog = new StringBuilder(name).Append(": ").Append(message).ToString();
                InfoMessageLogged(this, new LogMessageEventArgs(toLog));    
            }
        }

        public void ReportProgress(int current, int total)
        {
            if (ProgressChanged != null)
            {
                ProgressChanged(this, new ProgressEventArgs(total, current));    
            }
        }
    }
}