using System;
using System.Linq.Expressions;
using AutoMapper;

namespace WebShop.Common.ExtensionMethods
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDest> Ignore<TSource, TDest>(this IMappingExpression<TSource, TDest> expression, Expression<Func<TDest, object>> member)
        {
            return expression.ForMember(member, o => o.Ignore());
        }
    }
}