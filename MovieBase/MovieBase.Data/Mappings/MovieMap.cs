using MovieBase.Domain;

namespace MovieBase.Data.Mappings
{
    public class MovieMap : DomainObjectMap<Movie>
    {
        public MovieMap()
        {
            Map(x => x.Title);
            HasManyToMany(x => x.Categories)
                .Cascade.All()
                .Table("MovieCategory");
        }
    }
}