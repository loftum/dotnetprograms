using System;
using System.Collections.Generic;
using System.Linq;
using DotNetPrograms.Common.Mapping.Exceptions;
using DotNetPrograms.Common.Meta;

namespace DotNetPrograms.Common.Mapping.Internal
{
    public class TypeMap : TypeMapBase
    {
        private readonly IList<IPropertyMap> _propertyMaps = new List<IPropertyMap>();

        public TypeMap(Type sourceType, Type targetType, IMapRegistry registry) : base(sourceType, targetType, registry)
        {
            Init();
        }

        private void Init()
        {
            foreach (var targetProperty in TargetType.Properties.Where(p => p.HasSetter))
            {
                var sourceProperty = SourceType.Properties.SingleOrDefault(p => p.Name == targetProperty.Name);
                _propertyMaps.Add(PropertyMapFor(sourceProperty, targetProperty));
            }
        }

        private IPropertyMap PropertyMapFor(PropertyMeta sourceProperty, PropertyMeta targetProperty)
        {
            if (sourceProperty == null)
            {
                return new NullPropertyMap(TargetType.Type, targetProperty);
            }
            return new PropertyMap(TargetType.Type, sourceProperty, targetProperty, Registry);
        }

        public override void Map(object source, object target)
        {
            foreach (var propertyMap in _propertyMaps)
            {
                propertyMap.Map(source, target);
            }
        }

        public override object Map(object source)
        {
            if (source == null)
            {
                return null;
            }
            var target = TypeMeta.New(TargetType.Type);
            foreach (var propertyMap in _propertyMaps)
            {
                try
                {
                    propertyMap.Map(source, target);
                }
                catch (Exception ex)
                {
                    throw new MappingException(string.Format("Could not map property {0}", propertyMap.FullName), ex);
                }
            }
            return target;
        }
    }
}