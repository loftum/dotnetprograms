using System;
using System.Collections.Generic;
using System.Linq;
using MovieBase.Common.Comparison;
using MovieBase.Data.Dao;
using MovieBase.Domain;

namespace MovieBase.Data.Services
{
    public class MovieBaseService : IMovieBaseService
    {
        private readonly IMovieBaseRepository _repo;

        public MovieBaseService(IMovieBaseRepository repo)
        {
            _repo = repo;
        }

        public T Save<T>(T item) where T : DomainObject
        {
            return _repo.Save(item);
        }

        public Movie MovieByTitle(string title)
        {
            var movies = _repo.GetAll<Movie>()
                .Where(movie => StringValue.Of(movie.Title).EqualsIgnoreCase(title));
            return movies.Count() > 0 ?
                movies.First() :
                CreateNewMovie(title);
        }

        private Movie CreateNewMovie(string title)
        {
            var movie = new Movie {Title = title};
            return Save(movie);
        }

        public Category CategoryByName(string name)
        {
            var categories = _repo.GetAll<Category>()
                .Where(c => StringValue.Of(c.Name).EqualsIgnoreCase(name));
            return categories.Count() > 0 ?
                categories.First() :
                CreateNewCategory(name);
        }

        public Category CreateNewCategory(string name)
        {
            var category = new Category {Name = name};
            return Save(category);
        }

        public IEnumerable<Movie> SearchMovies(string searchText)
        {
            if (string.IsNullOrWhiteSpace(searchText))
            {
                return _repo.GetAll<Movie>();
            }
            return _repo.GetAll<Movie>()
                .Where(movie => StringValue.Of(movie.Title).Like(searchText));
        }
    }
}