using StuffLibrary.Domain;

namespace StuffLibrary.Repository.Mappings
{
    public class CategoryMap : DomainObjectMap<Category>
    {
        public CategoryMap()
        {
            Map(x => x.Name);
        }
    }
}