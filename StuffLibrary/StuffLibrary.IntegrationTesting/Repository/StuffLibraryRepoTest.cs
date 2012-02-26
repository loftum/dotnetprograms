using NUnit.Framework;
using Ninject;
using StuffLibrary.Common.Scoping;
using StuffLibrary.Domain;
using StuffLibrary.IntegrationTesting.NinjectModules;
using StuffLibrary.IntegrationTesting.UnitOfWork;
using StuffLibrary.NinjectModules;
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
        private RollbackUnitOfWork _unitOfWork;
        private DisposableObject _scope;

        [TestFixtureSetUp]
        public void SetUpTestFixture()
        {
            _kernel = new StandardKernel(new CommonModule(), new ConfigModule(), new RepoModule(), new UnitOfWorkModule());
        }

        [TestFixtureTearDown]
        public void TearDownTestFixture()
        {
            _kernel.Dispose();
        }

        [SetUp]
        public void Setup()
        {
            _scope = new DisposableObject();
            InjectionContext.Current = _scope;
            _repo = _kernel.Get<StuffLibraryRepo>();
            _unitOfWork = _kernel.Get<RollbackUnitOfWork>();
        }

        [TearDown]
        public void TearDown()
        {
            _unitOfWork.Rollback();
            _scope.Dispose();
            _scope = null;
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

            var gottenMovies = _repo.GetAll<Movie>();
            CustomAssert.ThatCollection(gottenMovies).HasCount(5);
        }
    }
}