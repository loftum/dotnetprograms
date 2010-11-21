using FluentNHibernate.Mapping;
using MovieBase.Domain;

namespace MovieBase.Data.Mappings
{
    public abstract class DomainObjectMap<T> : ClassMap<T> where T : DomainObject
    {
        protected DomainObjectMap()
        {
            Id(x => x.Id);
        }
    }
}