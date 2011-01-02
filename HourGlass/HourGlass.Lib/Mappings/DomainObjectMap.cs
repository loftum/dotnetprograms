using FluentNHibernate.Mapping;
using HourGlass.Lib.Domain;

namespace HourGlass.Lib.Mappings
{
    public abstract class DomainObjectMap<T> : ClassMap<T>
        where T : DomainObject
    {
        protected DomainObjectMap()
        {
            Id(x => x.Id);
        }
    }
}