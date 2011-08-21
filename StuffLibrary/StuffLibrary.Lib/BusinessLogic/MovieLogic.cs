using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NHibernate.Criterion;
using NHibernate.Linq;
using StuffLibrary.Common.ExtensionMethods;
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