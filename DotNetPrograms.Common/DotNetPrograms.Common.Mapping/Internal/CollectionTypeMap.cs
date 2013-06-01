using System;
using System.Collections;
using DotNetPrograms.Common.Meta;

namespace DotNetPrograms.Common.Mapping.Internal
{
    public class CollectionTypeMap : TypeMapBase
    {
        public CollectionTypeMap(Type sourceType, Type targetType, IMapRegistry registry) : base(sourceType, targetType, registry)
        {
            if (!SourceType.IsCollection)
            {
                throw new InvalidOperationException(string.Format("{0} is not a collection.", SourceType.Type));
            }
            if (!TargetType.IsCollection)
            {
                throw new InvalidOperationException(string.Format("{0} is not a collection.", TargetType.Type));
            }
        }

        public override void Map(object source, object target)
        {

        }

        public override object Map(object source)
        {
            if (source == null)
            {
                return null;
            }
            var list = new ChameleonList();
            
            var collectionMeta = new CollectionMeta(TargetType.Type);
            var targetType = collectionMeta.GetItemType();

            foreach (var value in (IEnumerable) source)
            {
                list.Add(MapValue(value, targetType));
            }
            return list.As(TargetType.Type);
        }

        private object MapValue(object source, Type targetType)
        {
            if (source == null)
            {
                return null;
            }
            var map = Registry.GetMap(source.GetType(), targetType);
            return map.Map(source);
        }
    }
}