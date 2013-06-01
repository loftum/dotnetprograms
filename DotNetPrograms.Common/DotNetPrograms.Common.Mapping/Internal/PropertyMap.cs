using System;
using DotNetPrograms.Common.Mapping.Exceptions;
using DotNetPrograms.Common.Meta;
using DotNetPrograms.Common.Validation;

namespace DotNetPrograms.Common.Mapping.Internal
{
    public class PropertyMap : PropertyMapBase
    {
        private readonly PropertyMeta _sourceProperty;
        private readonly ITypeMap _map;

        public PropertyMap(Type parentType, PropertyMeta sourceProperty, PropertyMeta targetProperty, IMapRegistry registry) : base(parentType, targetProperty)
        {
            Guard.NotNull(() => sourceProperty, () => targetProperty);
            _sourceProperty = sourceProperty;
            _map = registry.GetMap(sourceProperty.Type, targetProperty.Type);
        }

        public override void Map(object source, object target)
        {
            try
            {
                var sourceValue = _sourceProperty.GetValue(source);
                var targetValue = _map.Map(sourceValue);
                TargetProperty.SetValue(target, targetValue);
            }
            catch (Exception ex)
            {
                throw new MappingException(string.Format("Could not map {0}.{1} to {2}.{3}", source.GetType(), _sourceProperty.Name, target, TargetProperty.Name), ex);
            }
        }
    }
}