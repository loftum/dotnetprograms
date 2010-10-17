namespace Deploy.Lib.Logging
{
    public class LogMessageEventArgs
    {
        public string Message { get; private set; }

        public LogMessageEventArgs(string message)
        {
            Message = message;
        }
    }
}