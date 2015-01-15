using System;
using AutoMapper;

namespace WebShop.Common.ExtensionMethods
{
    public static class ObjectExtensions
    {
        public static T If<T>(this T obj, bool condition, Action<T> action)
        {
            if (condition)
            {
                action(obj);
            }
            return obj;
        }

        public static TDest MapTo<TDest>(this object source)
        {
            return Mapper.Map<TDest>(source);
        }

        public static TDest MapTo<TDest>(this object source, TDest dest)
        {
            return Mapper.Map(source, dest);
        }
    }
}