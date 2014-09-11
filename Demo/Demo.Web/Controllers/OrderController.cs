using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Demo.Web.Models;

namespace Demo.Web.Controllers
{
    public class OrderController : Controller
    {
        private static readonly List<OrderModel> Orders = new List<OrderModel>();

        static OrderController()
        {
            var firstNames = new[] {"Morten", "Gjermund", "Pål", "Kjell", "Hans Olav", "Andreas", "Pølsemaker", "Mats", "Truls"};
            var lastNames = new[] {"Riksfjord", "Bjordal", "Fagerås", "Sæther", "Loftum", "Knudsen", "Andersen", "Oustad", "Tveøy"};

            var names = new List<string>();
            foreach (var firstName in firstNames)
            {
                names.AddRange(lastNames.Select(lastName => string.Join(" ", firstName, lastName)));
            }

            var nameIndex = 0;
            for (var ii = 1; ii < 1001; ii++)
            {
                if (nameIndex >= names.Count)
                {
                    nameIndex = 0;
                }
                Orders.Add(new OrderModel
                {
                    OrderId = ii.ToString(CultureInfo.InvariantCulture),
                    Date = DateTime.Now.AddMonths(-6).AddDays(ii),
                    DeliveryNoteNumber = (ii * 42).ToString(CultureInfo.InvariantCulture),
                    Imei = ii.ToString(CultureInfo.InvariantCulture).PadLeft(8, '0'),
                    InvoiceNumber = (ii * 3).ToString(CultureInfo.InvariantCulture),
                    TrackingNumber = "TR" + (ii * 128).ToString(CultureInfo.InvariantCulture),
                    Buyer = names[nameIndex]
                });
                nameIndex++;
            }
        }

        public ActionResult SimpleSearch()
        {
            return View(new SimpleSearchInput());
        }

        [HttpPost]
        public ActionResult SimpleSearch(SimpleSearchInput input)
        {
            var orders = from o in Orders
                where input.Wants(o)
                select o;
            input.Orders = Order(orders, input)
                .Take(20)
                .ToList();
            return View(input);
        }

        public ActionResult CombinedSearch()
        {
            return View(new CombinedSearchInput());
        }

        [HttpPost]
        public ActionResult CombinedSearch(CombinedSearchInput input)
        {
            var orders = from o in Orders
                where (o.Date >= input.DateFrom) &&
                      (o.Date < input.DateTo) &&
                      (input.Name == null || o.Buyer.ToLowerInvariant().Contains(input.Name.ToLowerInvariant()))
                      select o;
            input.Orders = Order(orders, input)
                .Take(20)
                .ToList();
            return View(input);
        }

        private static IEnumerable<OrderModel> Order(IEnumerable<OrderModel> orders, OrderSearchResult input)
        {
            var type = typeof(OrderModel);
            Func<OrderModel, object> func = o => type.GetProperty(input.OrderBy).GetValue(o);

            switch (input.Direction)
            {
                case OrderDirection.Ascending:
                    return orders.OrderBy(func);
                default:
                    return orders.OrderByDescending(func);
            }
        }

        public ActionResult See(string id)
        {
            var order = Orders.SingleOrDefault(o => o.OrderId == id);
            if (order == null)
            {
                throw new Exception("Don't be stupid, please.");
            }
            return View(order);
        }
    }
}