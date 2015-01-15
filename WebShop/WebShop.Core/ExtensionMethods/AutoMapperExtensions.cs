using AutoMapper;
using WebShop.Common.ExtensionMethods;
using WebShop.Core.Domain.OrderDb;

namespace WebShop.Core.ExtensionMethods
{
    public static class AutoMapperExtensions
    {
        public static IMappingExpression<TSource, TDest> IgnoreChangeStamp<TSource, TDest>(
            this IMappingExpression<TSource, TDest> expression) where TDest : IHaveChangeStamp
        {
            return expression
                .Ignore(d => d.ChangeStamp);
        }

        public static IMappingExpression<TSource, TDest> IgnoreOrderDbObjectProperties<TSource, TDest>(
            this IMappingExpression<TSource, TDest> expression) where TDest : OrderDbObjectWithChangeStamp
        {
            return expression.Ignore(d => d.Id).Ignore(d => d.ChangeStamp);
        }
    }
}