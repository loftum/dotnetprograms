using System.IO;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MovieBase.Data.Dao;
using MovieBase.Data.Mappings;
using MovieBase.Domain;
using MovieBase.Domain.Builders;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using NUnit.Framework;

namespace MovieBase.IntegrationTesting.Data
{
    [TestFixture]
    public class MovieBaseRepositoryTest
    {
        private MovieBaseRepository _repository;
        private const string DbFileName = "database.db";

        [SetUp]
        public void Setup()
        {
            var sessionFactory = CreateSessionFactory();
            _repository = new MovieBaseRepository(sessionFactory);
        }

        [Test]
        public void ShouldSaveMovie()
        {
            var movie = Build.Movie().WithTitle("Title").Item;
            var savedMovie = _repository.Save(movie);
            Assert.That(savedMovie.IsPersisted);
        }

        [Test]
        public void ShouldGetMovie()
        {
            var movie = Build.Movie().WithTitle("Title").Item;
            var savedMovie = _repository.Save(movie);
            var gottenMovie = _repository.Get<Movie>(movie.Id);
            Assert.That(gottenMovie.Id, Is.EqualTo(savedMovie.Id));
        }

        [Test]
        public void ShouldSaveMovieWithCategory()
        {
            var movie = Build.Movie().WithTitle("Title")
                .WithCategory(Build.Category().WithName("Category"))
                .Item;
            var saved = _repository.Save(movie);

            var gottenMovie = _repository.Get<Movie>(saved.Id);
            Assert.That(gottenMovie.Categories.Count, Is.EqualTo(1));
        }

        private static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(
                    SQLiteConfiguration.Standard.ShowSql().UsingFile(DbFileName)
                )
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<MovieMap>())
                .ExposeConfiguration(BuildSchema)
                .BuildSessionFactory();
        }

        private static void BuildSchema(Configuration configuration)
        {
            if (File.Exists(DbFileName))
            {
                File.Delete(DbFileName);
            }
            new SchemaExport(configuration).Create(false, true);
        }
    }
}
