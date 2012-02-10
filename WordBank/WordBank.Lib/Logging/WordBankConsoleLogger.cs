using System;
using System.Text;

namespace Wordbank.Lib.Logging
{
    public class WordBankConsoleLogger : IWordBankLogger
    {
        public void Info(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public void Warn(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public void Error(string message, params object[] args)
        {
            Console.WriteLine(message, args);
        }

        public void Error(Exception exception, string message, params object[] args)
        {
            Console.WriteLine(
                new StringBuilder()
                .AppendFormat(message, args).AppendLine()
                .AppendLine(exception.ToString())
                );
        }
    }
}