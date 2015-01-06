using System;
using Ninject;
using WordBank.NinjectModules;
using Wordbank.Commands;
using Wordbank.Lib.Exceptions;
using Wordbank.Lib.NinjectModules;

namespace WordBank
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = CreateKernel();
            try
            {
                kernel.Get<ICommandExecutor>().Execute(args);
            }
            catch(UserException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static IKernel CreateKernel()
        {
            return new StandardKernel(new DataModule(), new ConsoleModule());
        }
    }
}
