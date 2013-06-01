using System;
using DotNetPrograms.Common.Meta;

namespace DotNetPrograms.Common.Mapping.Internal
{
    public abstract class TypeMapBase : ITypeMap
    {
        protected readonly IMapRegistry Registry;
        protected readonly TypeMeta SourceType;
        protected readonly TypeMeta TargetType;

        protected TypeMapBase(Type sourceType, Type targetType, IMapRegistry registry)
        {
            SourceType = new TypeMeta(sourceType);
            TargetType = new TypeMeta(targetType);
            Registry = registry;
        }

        public abstract void Map(object source, object target);
        public abstract object Map(object source);
    }
}