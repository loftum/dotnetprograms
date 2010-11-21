using System.Collections.Generic;

namespace MovieBase.Domain
{
    public class Category : DomainObject
    {
        public virtual string Name { get; set; }
        public virtual IList<Movie> Movies { get; set; }
        
        public Category()
        {
            Movies = new List<Movie>();
        }

        public virtual void AddMovie(Movie movie)
        {
            Movies.Add(movie);
        }
    }
}