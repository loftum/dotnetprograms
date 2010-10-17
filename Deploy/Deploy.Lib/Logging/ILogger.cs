namespace Deploy.Lib.Logging
{
    public interface ILogger
    {
        event LogMessageEventHandler InfoMessageLogged;
        event ProgressEventHandler ProgressChanged;

        void Info(string name, string message);
        void ReportProgress(int current, int total);

    }

    public delegate void ProgressEventHandler(object sender, ProgressEventArgs args);
    public delegate void LogMessageEventHandler(object sender, LogMessageEventArgs args);
}