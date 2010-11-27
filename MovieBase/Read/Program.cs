using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MovieBase.Data.Dao;
using MovieBase.Data.Mappings;
using MovieBase.Data.Services;
using NHibernate;

namespace Read
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ValidateArguments(args);

            try
            {
                Console.Write("Connecting to MovieBase...");
                var repo = new MovieBaseRepository(CreateSessionFactory());
                Console.WriteLine("OK");
                var service = new MovieBaseService(repo);
                Console.WriteLine("Starting import from " + args[0]);
                new MovieFileImporter(args[0], service).ImportAll();
            }
            catch(MovieReaderException e)
            {
                PrintException(e);
                Environment.Exit(1);
            }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    MySQLConfiguration.Standard.ConnectionString(c => c.FromConnectionStringWithKey("MovieBase"))
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<MovieMap>())
                .BuildSessionFactory();
        }

        private static void ValidateArguments(ICollection<string> args)
        {
            if (args.Count == 0)
            {
                PrintUsageAndExit();
            }
        }

        private static void PrintException(Exception exception)
        {
            Console.WriteLine(exception);
        }

        private static void PrintUsageAndExit()
        {
            Console.WriteLine(GetUsage());
            Environment.Exit(0);
        }

        private static string GetUsage()
        {
            return new StringBuilder()
                .Append(Process.GetCurrentProcess().ProcessName)
                .Append(" <csv-filepath>")
                .ToString();
        }
    }
}
