using System;
using System.Linq;

namespace DotNetPrograms.Common.Meta
{
    public class CollectionMeta : TypeMeta
    {
        public CollectionMeta(Type type) : base(type)
        {
            if (!IsCollection)
            {
                throw new InvalidOperationException(string.Format("{0} is not a collection type", type));
            }
        }

        public Type GetItemType()
        {
            var type = Type.GetGenericArguments().FirstOrDefault();
            return type ?? typeof (object);
        }
    }
}