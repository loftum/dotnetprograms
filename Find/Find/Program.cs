using System;
using System.Diagnostics;
using System.Text;
using Find.Parameters;
using Find.Searching;

namespace Find
{
    public class Program
    {
        static void Main(string[] args)
        {
            PrintArgs(args);
            if (args.Length < 1)
            {
                PrintUsage();
                Environment.Exit(0);
            }
            try
            {
                new Finder(FindParameters.Parse(args)).Find();
            }
            catch (FindException e)
            {
                PrintError(e);
            }
        }

        private static void PrintArgs(string[] args)
        {
            var builder = new StringBuilder();
            for(var ii=0; ii<args.Length; ii++)
            {
                builder.Append("args[").Append(ii).Append("]=").AppendLine(args[ii]);
            }
            Console.WriteLine(builder.ToString());
        }

        private static void PrintError(Exception exception)
        {
            Console.WriteLine(GenerateMessageFor(exception));
        }

        private static string GenerateMessageFor(Exception exception)
        {
            return new StringBuilder()
                .AppendLine("Could not run command: ")
                .Append(exception.Message)
                .ToString();
        }

        private static void PrintUsage()
        {
            Console.WriteLine(GetUsage());
        }

        private static string GetUsage()
        {
            return new StringBuilder(Process.GetCurrentProcess().ProcessName)
                .Append(" [path] [pattern]")
                .ToString();
        }
    }
}
