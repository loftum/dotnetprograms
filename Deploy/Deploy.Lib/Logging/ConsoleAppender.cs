using System;

namespace Deploy.Lib.Logging
{
    public class ConsoleAppender : IAppender
    {
        public void Append(object sender, LogMessageEventArgs args)
        {
            Console.WriteLine(args.Message);
        }
    }
}