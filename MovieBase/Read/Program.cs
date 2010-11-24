using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MovieBase.Data.Dao;
using MovieBase.Data.Mappings;
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
                var movies = new MovieFileReader(args[0]).ReadAll();
                var repo = new MovieBaseRepository(CreateSessionFactory());
                foreach (var movie in movies)
                {
                    repo.Save(movie);
                    Console.WriteLine("Saved " + movie.Title);
                }
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
