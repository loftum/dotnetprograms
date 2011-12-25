using System;
using DbTool.Commands;
using DbTool.Lib.Configuration;
using DbTool.Lib.Exceptions;
using DbTool.Lib.Modules;
using DbTool.Modules;
using Ninject;

namespace DbTool
{
    public class Program
    {
        static int Main(string[] args)
        {
            var kernel = CreateKernel();
            var config = kernel.Get<IDbToolConfig>();
            try
            {
                var provider = kernel.Get<ICommandProvider>();
                provider.GetCommand(args).Execute(args);
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

        private static IKernel CreateKernel()
        {
            return new StandardKernel(new ConfigModule(),
                new LogModule(),
                new TaskModule(),
                new CommandModule());
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
