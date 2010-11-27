using System.Collections.Generic;
using MovieBase.Domain;

namespace MovieBase.Data.Services
{
    public interface IMovieBaseService
    {
        Movie MovieByTitle(string title);
        Category CategoryByName(string name);
        Category CreateNewCategory(string name);
        T Save<T>(T item) where T : DomainObject;
        IEnumerable<Movie> SearchMovies(string searchText);
    }
}