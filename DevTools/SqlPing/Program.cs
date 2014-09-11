using System;
using System.Diagnostics;
using System.Linq;

namespace SqlPing
{
    class Program
    {
        static void Main(string[] argz)
        {
            if (!argz.Any())
            {
                PrintUsage();
                return;
            }

            var arguments = new PingArgs(argz);
            try
            {
                Pinger.Ping(arguments.What);
                Console.WriteLine("Pong!");
            }
            catch (StupidUserException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("PANG!");
                if (arguments.Verbose)
                {
                    Console.WriteLine(ex);    
                }
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("{0} <host or connectionString> [-v(erbose)]", Process.GetCurrentProcess().ProcessName);
        }
    }
}
