using System;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DbTool.Lib.Tasks;

namespace DbTool
{
    public class Program
    {
        static int Main(string[] args)
        {
            var config = new DbToolConfig();
            try
            {
                var provider = new TaskProvider(config);
                provider.GetTask(args).Execute(args);
            }
            catch(DbToolException e)
            {
                PrintExceptionMessage(e);
            }
            catch (Exception e)
            {
                PrintException(e);
                return -1;
            }
            finally
            {
                config.SaveSettings();
            }
            return 0;
        }

        private static void PrintExceptionMessage(DbToolException exception)
        {
            Console.WriteLine(exception.Message);
        }

        private static void PrintException(Exception exception)
        {
            Console.WriteLine(exception);
        }
    }
}
