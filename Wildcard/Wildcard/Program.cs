using System;
using System.Diagnostics;
using System.Text;
using Wildcard.Replacement;

namespace Wildcard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                PrintUsage();
            }
            var parameters = ReplacementParameters.Parse(args);

        }

        private static void PrintUsage()
        {
            Console.WriteLine(GetUsage());
        }

        private static string GetUsage()
        {
            return new StringBuilder(Process.GetCurrentProcess().ProcessName)
                .Append(" <wildcardfile> <file>")
                .ToString();
        }
    }
}
