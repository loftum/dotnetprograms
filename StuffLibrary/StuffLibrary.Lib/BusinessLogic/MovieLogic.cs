using System.Collections.Generic;
using System.Linq;
using StuffLibrary.Common.ExtensionMethods;
using StuffLibrary.Common.Logging;
using StuffLibrary.Domain;
using StuffLibrary.Lib.UnitOfWork;
using StuffLibrary.Repository;

namespace StuffLibrary.Lib.BusinessLogic
{
    public class MovieLogic : IMovieLogic
    {
        private readonly IStuffLibraryRepo _repo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStuffLibraryLogger _logger;

        public MovieLogic(IStuffLibraryRepo repo, IStuffLibraryLogger logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _repo = repo;
        }

        public Movie GetMovie(long id)
        {
            return _repo.Get<Movie>(id);
        }

        public IEnumerable<Movie> GetAllMovies(string query)
        {
            var movies = _repo.GetAll<Movie>();
            if (!query.IsNullOrEmpty())
            {
                movies = movies.Where(movie => movie.Title.Contains(query));
            }
            return movies;
        }

        public long Save(Movie movie)
        {
            using (var work = _unitOfWork.Begin())
            {
                if (movie.IsNew())
                {
                    CreateNew(movie);
                }
                else
                {
                    Update(movie);
                }
                work.Complete();
                return movie.Id;    
            }
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