using BasicManifest.Core.Domain;
using FluentNHibernate.Mapping;

namespace BasicManifest.Data.Mappings
{
    public class DomainObjectMap<TDomainObject> : ClassMap<TDomainObject> where TDomainObject : DomainObject
    {
        public DomainObjectMap()
        {
            Id(o => o.Id).GeneratedBy.Guid();
            Map(o => o.CreatedBy);
            Map(o => o.CreatedDate);
            Map(o => o.ModifiedBy);
            Map(o => o.ModifiedDate);
         }
    }
}