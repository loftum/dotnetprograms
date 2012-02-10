using FluentNHibernate.Mapping;
using Wordbank.Lib.Domain;

namespace Wordbank.Lib.DomainMapping
{
    public abstract class DomainObjectMap<TDomainObject> : ClassMap<TDomainObject>
        where TDomainObject : DomainObject
    {
        protected DomainObjectMap()
        {
            Id(o => o.Id);
        }
    }
}