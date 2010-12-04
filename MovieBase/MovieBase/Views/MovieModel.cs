using System.Collections.Generic;
using MovieBase.Domain;

namespace MovieBase.Views
{
    public class MovieModel
    {
        public IList<Movie> Movies { get; private set; }

        public MovieModel()
        {
            Movies = new List<Movie>();
        }

        public void AddMovie(Movie movie)
        {
            Movies.Add(movie);
        }

        public void AddAll(IEnumerable<Movie> movies)
        {
            foreach (var movie in movies)
            {
                Movies.Add(movie);
            }
        }

        public void Clear()
        {
            Movies.Clear();
        }
    }
}