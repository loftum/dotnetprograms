using System;

namespace DotNetPrograms.Common.Mapping.Internal
{
    public interface IMapRegistry
    {
        Guid Id { get; }
        ITypeMap GetMap<TSource, TTarget>();
        ITypeMap GetMap(Type sourceType, Type targetType);
        void Reset();
    }
}