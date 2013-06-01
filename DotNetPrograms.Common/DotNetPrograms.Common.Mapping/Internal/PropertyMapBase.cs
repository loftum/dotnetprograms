using System;
using DotNetPrograms.Common.Meta;

namespace DotNetPrograms.Common.Mapping.Internal
{
    public abstract class PropertyMapBase : IPropertyMap
    {
        public string PropertyName { get; private set; }
        public string FullName { get; private set; }
        protected readonly PropertyMeta TargetProperty;

        protected PropertyMapBase(Type parentType, PropertyMeta targetProperty)
        {
            TargetProperty = targetProperty;
            PropertyName = targetProperty.Name;
            FullName = string.Format("{0}.{1}", parentType.Name, targetProperty.Name);
        }

        public abstract void Map(object source, object target);

    }
}