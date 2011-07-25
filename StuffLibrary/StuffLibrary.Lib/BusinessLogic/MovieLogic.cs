using System.Collections.Generic;
using StuffLibrary.Common.Logging;
using StuffLibrary.Domain;
using StuffLibrary.Domain.ExtensionMethods;
using StuffLibrary.Repository;

namespace StuffLibrary.Lib.BusinessLogic
{
    public class MovieLogic : IMovieLogic
    {
        private readonly IStuffLibraryRepo _repo;
        private readonly IStuffLibraryLogger _logger;

        public MovieLogic(IStuffLibraryRepo repo, IStuffLibraryLogger logger)
        {
            _logger = logger;
            _repo = repo;
        }

        public Movie GetMovie(long id)
        {
            return _repo.Get<Movie>(id);
        }

        public IEnumerable<Movie> GetAllMovies()
        {
            return _repo.GetAll<Movie>().List();
        }

        public long Save(Movie movie)
        {
            if (movie.IsNew())
            {
                CreateNew(movie);
            }
            else
            {
                Update(movie);
            }
            _repo.SaveChanges();
            return movie.Id;
        }

        private void Update(Movie movie)
        {
            var existing = _repo.Get<Movie>(movie.Id);
            existing.Title = movie.Title;
        }

        private void CreateNew(Movie movie)
        {
            _repo.Add(movie);
        }
    }
}