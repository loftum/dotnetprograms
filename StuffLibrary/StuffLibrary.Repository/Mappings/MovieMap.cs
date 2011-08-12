using StuffLibrary.Domain;

namespace StuffLibrary.Repository.Mappings
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