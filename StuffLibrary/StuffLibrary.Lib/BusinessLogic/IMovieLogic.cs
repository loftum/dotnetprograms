using System.Collections.Generic;
using StuffLibrary.Domain;

namespace StuffLibrary.Lib.BusinessLogic
{
    public interface IMovieLogic
    {
        IEnumerable<Movie> GetAllMovies(string query);
        long Save(Movie movie);
        Movie GetMovie(long id);
    }
}