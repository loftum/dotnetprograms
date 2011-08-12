using NUnit.Framework;
using Ninject;
using StuffLibrary.Domain;
using StuffLibrary.IntegrationTesting.NinjectModules;
using StuffLibrary.Repository;
using StuffLibrary.UnitTesting;
using StuffLibrary.UnitTesting.Asserting;

namespace StuffLibrary.IntegrationTesting.Repository
{
    [TestFixture]
    public class StuffLibraryRepoTest
    {
        private IKernel _kernel;
        private StuffLibraryRepo _repo;

        [TestFixtureSetUp]
        public void SetUpTestFixture()
        {
            _kernel = new StandardKernel(new CommonSingletonModule(), new ConfigSingletonModule(), new RepoSingletonModule());
        }

        [TestFixtureTearDown]
        public void TearDownTestFixture()
        {
            _kernel.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            _repo = _kernel.Get<StuffLibraryRepo>();
        }

        [TearDown]
        public void TearDown()
        {
            _repo.Rollback();
        }

        [Test]
        public void ShouldAddMovie()
        {
            var movie = new Movie {Title = Some.Title};
            _repo.Add(movie);
            var gottenMovie = _repo.Get<Movie>(movie.Id);
            Assert.That(gottenMovie.IsNew(), Is.False);
            Assert.That(gottenMovie.Title, Is.EqualTo(Some.Title));
        }

        [Test]
        public void ShouldDeleteMovie()
        {
            var movie = new Movie { Title = Some.Title };
            _repo.Add(movie);
            var id = movie.Id;
            _repo.Delete(movie);
            var gottenMovie = _repo.Get<Movie>(id);
            Assert.That(gottenMovie, Is.Null);
        }

        [Test]
        public void ShouldGetAllMovies()
        {
            for (var ii = 0; ii < 5; ii++)
            {
                _repo.Add(new Movie{Title = string.Format("Title {0}", ii)});
            }

            var gottenMovies = _repo.GetAll<Movie>().List();
            CustomAssert.ThatCollection(gottenMovies).HasCount(5);
        }
    }
}