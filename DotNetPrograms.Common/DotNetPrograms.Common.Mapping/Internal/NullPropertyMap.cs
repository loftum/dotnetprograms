using System;
using DotNetPrograms.Common.Meta;

namespace DotNetPrograms.Common.Mapping.Internal
{
    public class NullPropertyMap : PropertyMapBase
    {
        public NullPropertyMap(Type parentType, PropertyMeta targetProperty) : base(parentType, targetProperty)
        {
        }

        public override void Map(object source, object target)
        {
        }
    }
}