using System;

namespace DotNetPrograms.Common.Mapping.Internal
{
    public class SimpleTypeMap : TypeMapBase
    {
        public SimpleTypeMap(Type sourceType, Type targetType, IMapRegistry registry) : base(sourceType, targetType, registry)
        {
            if (!SourceType.IsSimple)
            {
                throw new ArgumentException(string.Format("Source type must be a simple type"), "sourceType");
            }
            if (!TargetType.IsSimple)
            {
                throw new ArgumentException(string.Format("Target type must be a simple type"), "targetType");
            }
        }

        public override void Map(object source, object target)
        {
            
        }

        public override object Map(object source)
        {
            try
            {
                return source == null ? null : Convert.ChangeType(source, TargetType.Type);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(string.Format("Could not convert {0} to {1}", SourceType.Type, TargetType.Type), ex);
            }
        }
    }
}