using System;
using System.Windows;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MovieBase.AppLib.Controllers;
using MovieBase.AppLib.Views;
using MovieBase.Data.Dao;
using MovieBase.Data.Mappings;
using MovieBase.Data.Services;
using NHibernate;
using MovieBaseWindow = MovieBase.Views.MovieBaseWindow;

namespace MovieBase
{
    public class Program
    {
        [STAThread]
        public static void Main(string[] args)
        {
            var view = CreateView();
            CreateController(view);
            new Application().Run(view);
        }

        private static IMovieBaseController CreateController(IMovieBaseView view)
        {
            return new MovieBaseController(CreateService(), view);
        }

        private static IMovieBaseService CreateService()
        {
            return new MovieBaseService(CreateRepository());
        }

        private static IMovieBaseRepository CreateRepository()
        {
            return new MovieBaseRepository(CreateSessionFactory());
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

        private static MovieBaseWindow CreateView()
        {
            return new MovieBaseWindow();
        }
    }
}
