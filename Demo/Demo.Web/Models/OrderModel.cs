using System;
using System.Reflection;

namespace Demo.Web.Models
{
    public class OrderModel
    {
        public static readonly PropertyInfo[] Properties = typeof (OrderModel).GetProperties();

        public string OrderId { get; set; }
        public DateTime Date { get; set; }
        public string Buyer { get; set; }
        public string Imei { get; set; }
        public string TrackingNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string DeliveryNoteNumber { get; set; }
    }
}