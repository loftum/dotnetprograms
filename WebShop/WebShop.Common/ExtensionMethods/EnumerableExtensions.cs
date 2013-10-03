using System;
using System.Collections.Generic;
using System.Linq;
using WebShop.Common.Models.Pricing;

namespace WebShop.Common.ExtensionMethods
{
    public static class EnumerableExtensions
    {
        public static PriceModel Sum<T>(this IEnumerable<T> items, Func<T, PriceModel> price)
        {
            var total = PriceModel.Zero();
            return items.Aggregate(total, (current, item) => current + price(item));
        }
    }
}