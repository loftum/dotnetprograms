using System;
using MasterData.Core.Domain;

namespace MasterData.UnitTesting.TestData.Builders
{
    public abstract class MasterDataObjectBuilderBase<TItem, TBuilder> : BuilderBase<TItem, TBuilder>
        where TBuilder : MasterDataObjectBuilderBase<TItem, TBuilder>
        where TItem : MasterDataObject
    {
        protected MasterDataObjectBuilderBase(TItem item, bool generateId = true) : base(item)
        {
            if (generateId)
            {
                item.Id = NewId();
            }
        }

        private static Guid NewId()
        {
            return Guid.NewGuid();
        }

        public TBuilder WithId(Guid id)
        {
            Item.Id = id;
            return MySelf;
        }
    }
}