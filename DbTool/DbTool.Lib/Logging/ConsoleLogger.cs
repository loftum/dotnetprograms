using System;

namespace DbTool.Lib.Logging
{
    public class ConsoleLogger : IDbToolLogger
    {
        public void WriteLine(string text, params object[] args)
        {
            Console.WriteLine(text, args);
        }

        public void WriteLine(object text)
        {
            Console.WriteLine(text);
        }

        public void Write(string text, params object[] args)
        {
            Console.Write(text, args);
        }
    }
}