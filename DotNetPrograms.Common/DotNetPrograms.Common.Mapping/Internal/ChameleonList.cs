using System;
using System.Collections;
using System.Collections.Generic;
using DotNetPrograms.Common.Mapping.Exceptions;
using DotNetPrograms.Common.Meta;

namespace DotNetPrograms.Common.Mapping.Internal
{
    public class ChameleonList : ArrayList
    {
        public TList As<TList>() where TList : IEnumerable
        {
            return (TList) As(typeof (TList));
        }

        public IEnumerable As(Type type)
        {
            var meta = new CollectionMeta(type);
            if (!meta.IsCollection)
            {
                throw new ArgumentException(string.Format("{0} is not a collection type", type));
            }
            var list = ConvertTo(meta);
            return Populate(list);
        }

        private IList ConvertTo(CollectionMeta meta)
        {
            return meta.Type.IsInterface ? FromInterface(meta) : FromClass(meta);
        }

        private IList FromClass(CollectionMeta meta)
        {
            if (meta.Type.IsArray)
            {
                return Array.CreateInstance(meta.Type.GetElementType(), Count);
            }
            if (meta.Is<IEnumerable>())
            {
                return (IList) meta.NewUp();
            }
            throw new Tantrum(string.Format("Don't know how to create a new {0} as collection", meta));
        }

        private IList Populate(IList list)
        {
            foreach (var value in this)
            {
                list.Add(value);
            }
            return list;
        }

        private static IList FromInterface(CollectionMeta meta)
        {
            if (meta.Is<IEnumerable>())
            {
                if (meta.Type.IsGenericType)
                {
                    var list = typeof (List<>);
                    var type = list.MakeGenericType(meta.Type.GetGenericArguments());
                    return (IList) Activator.CreateInstance(type);
                }
                return new ArrayList();
            }
            throw new Tantrum(string.Format("Don't know how to create a new {0} as collection", meta));
        }
    }
}