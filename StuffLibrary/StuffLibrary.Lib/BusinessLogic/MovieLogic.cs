using System.Collections.Generic;
using StuffLibrary.Common.Logging;
using StuffLibrary.Domain;
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
            _repo.Add(movie);
            _repo.SaveChanges();
            return movie.Id;
        }
    }
}