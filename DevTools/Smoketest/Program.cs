using System;
using System.Diagnostics;

namespace Smoketest
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var arguments = SmoketestArgs.Parse(args);
                Console.WriteLine("Smoketesting {0}", arguments);
                var result = SmokeTester.SmokeTest(arguments);
                Console.WriteLine(result);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                PrintUsage();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("{0} [VERB] URL", Process.GetCurrentProcess().ProcessName);
        }
    }
}
