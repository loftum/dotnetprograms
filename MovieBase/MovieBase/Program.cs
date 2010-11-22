using System;
using System.Windows;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MovieBase.Controllers;
using MovieBase.Data.Dao;
using MovieBase.Views;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace MovieBase
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            Console.WriteLine("Creating view");
            var view = CreateView();
            Console.WriteLine("Creating controller");
            var controller = CreateController(view);
            Console.WriteLine("starting app");
            new Application().Run(view);
        }

        private static MovieBaseWindow CreateView()
        {
            return new MovieBaseWindow();
        }

        private static IMovieBaseController CreateController(IMovieBaseView view)
        {
            var repository = CreateRepository();
            return new MovieBaseController(repository, view);
        }

        private static IMovieBaseRepository CreateRepository()
        {
            return new MovieBaseRepository(CreateSessionFactory());
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(c => c.FromAppSetting("MovieBase")))
                .ExposeConfiguration(CreateSchema)
                .BuildSessionFactory();
        }

        private static void CreateSchema(Configuration configuration)
        {
            new SchemaExport(configuration).Create(false, true);
        }
    }
}
