using System;
using System.Collections.Generic;
using System.Text;
using DotNetPrograms.Common.Mapping.Exceptions;
using DotNetPrograms.Common.Meta;

namespace DotNetPrograms.Common.Mapping.Internal
{
    public class MapRegistry : IMapRegistry
    {
        public Guid Id { get; private set; }
        private readonly IDictionary<TypePair,ITypeMap> _maps = new Dictionary<TypePair, ITypeMap>();

        public MapRegistry()
        {
            Id = Guid.NewGuid();
        }

        public ITypeMap GetMap<TSource, TTarget>()
        {
            return GetMap(typeof (TSource), typeof (TTarget));
        }

        public ITypeMap GetMap(Type sourceType, Type targetType)
        {
            var pair = new TypePair(sourceType, targetType);
            if (!_maps.ContainsKey(pair))
            {
                _maps[pair] = CreateMap(sourceType, targetType);
            }
            return _maps[pair];
        }

        private ITypeMap CreateMap(Type sourceType, Type targetType)
        {
            var target = new TypeMeta(targetType);
            if (target.IsSimple)
            {
                return new SimpleTypeMap(sourceType, targetType, this);
            }
            if (target.IsDictionary)
            {
                
            }
            if (target.IsCollection)
            {
                return new CollectionTypeMap(sourceType, targetType, this);
            }
            if (target.IsComplex)
            {
                return new TypeMap(sourceType, targetType, this);
            }
            throw new Tantrum(string.Format("Don't know how to map {0}", targetType));
        }

        public void Reset()
        {
            _maps.Clear();
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            foreach (var typeMap in _maps)
            {
                builder.AppendFormat("{0}", typeMap.Key).AppendLine();
            }
            return builder.ToString();
        }
    }
}