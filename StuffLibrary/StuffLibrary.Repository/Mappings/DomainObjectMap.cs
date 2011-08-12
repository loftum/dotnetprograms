using FluentNHibernate.Mapping;
using StuffLibrary.Domain;

namespace StuffLibrary.Repository.Mappings
{
    public abstract class DomainObjectMap<TDomainObject> : ClassMap<TDomainObject> where TDomainObject : DomainObject
    {
         protected DomainObjectMap()
         {
            // ReSharper disable DoNotCallOverridableMethodsInConstructor
             Id(x => x.Id);
             OptimisticLock.Version();
             Version(x => x.Version).Column("Version");
             Map(x => x.CreatedBy);
             Map(x => x.CreatedAt);
             Map(x => x.ModifiedBy);
             Map(x => x.ModifiedAt);
             // ReSharper restore DoNotCallOverridableMethodsInConstructor
         } 
    }
}