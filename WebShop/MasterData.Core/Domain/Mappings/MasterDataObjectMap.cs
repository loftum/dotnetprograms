using FluentNHibernate.Mapping;
using MasterData.Core.Domain.MasterData;

namespace MasterData.Core.Domain.Mappings
{
    public abstract class MasterDataObjectMap<T> : ClassMap<T> where T : MasterDataObject
    {
        protected MasterDataObjectMap()
        {
            Id(o => o.Id).GeneratedBy.Guid();
        }
    }
}