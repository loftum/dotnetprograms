using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Demo.Web.Models
{
    public abstract class OrderSearchResult
    {
        private static readonly IList<SelectListItem> AvailableOrderBys;

        static OrderSearchResult()
        {
            AvailableOrderBys = typeof (OrderModel)
                .GetProperties()
                .Select(p => new SelectListItem {Text = p.Name, Value = p.Name})
                .ToList();
        }

        public IEnumerable<SelectListItem> OrderBys { get { return AvailableOrderBys; } }
        public string OrderBy { get; set; }
        public OrderDirection Direction { get; set; }

        public IList<OrderModel> Orders { get; set; }

        protected OrderSearchResult()
        {
            Orders = new List<OrderModel>();
        }
    }
}