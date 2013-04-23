using System;
using System.Linq.Expressions;
using AutoMapper;

namespace BasicManifest.Lib.ExtensionMethods
{
    public static class AutoMapperExtensions
    {
        public static TDestination MapTo<TDestination>(this object item)
        {
            return Mapper.Map<TDestination>(item);
        }

        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> mapping, Expression<Func<TDestination, object>> property)
        {
            return mapping.ForMember(property, o => o.Ignore());
        }
    }
}