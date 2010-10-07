using System;
using System.Diagnostics;
using System.Text;
using Deploy.Lib.Deployment;

namespace Deploy
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var parameters = DeployParameters.Parse(args);
                new Deployer(parameters).Deploy();
            }
            catch(InvalidParametersException e)
            {
                PrintInvalidParameterError(e);
                PrintUsage();
                Environment.Exit(0);
            }
            catch (Exception e)
            {
                PrintError(e);
                Environment.Exit(2);
            }
        }

        private static void PrintInvalidParameterError(InvalidParametersException e)
        {
            Console.WriteLine(GenerateInvalidParameterMessage(e));
        }

        private static string GenerateInvalidParameterMessage(InvalidParametersException e)
        {
            return new StringBuilder("Invalid parameters:").AppendLine()
                .Append(e.Message).ToString();
        }

        private static void PrintError(Exception exception)
        {
            Console.WriteLine(GenerateErrorMessage(exception));
        }

        private static string GenerateErrorMessage(Exception exception)
        {
            return new StringBuilder("Could not deploy: ").AppendLine()
                .AppendLine(exception.ToString()).ToString();
        }

        private static void PrintUsage()
        {
            Console.WriteLine(DeployParameters.GetUsage());
        }
    }
}
