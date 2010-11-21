using MovieBase.Domain;

namespace MovieBase.Data.Mappings
{
    public class CategoryMap : DomainObjectMap<Category>
    {
        public CategoryMap()
        {
            Map(x => x.Name);
            HasManyToMany(x => x.Movies)
                .Inverse()
                .Cascade.All()
                .Table("MovieCategory");
        }
    }
}