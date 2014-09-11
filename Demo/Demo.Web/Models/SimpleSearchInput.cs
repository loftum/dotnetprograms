namespace Demo.Web.Models
{
    public class SimpleSearchInput : OrderSearchResult
    {
        public OrderProperty OrderProperty { get; set; }
        public string Value { get; set; }

        public bool Wants(OrderModel order)
        {
            switch (OrderProperty)
            {
                case(OrderProperty.DeliveryNoteNumber):
                    return order.DeliveryNoteNumber == Value;
                case(OrderProperty.Imei):
                    return order.Imei == Value;
                case(OrderProperty.InvoiceNumber) :
                    return order.InvoiceNumber == Value;
                case (OrderProperty.OrderId):
                    return order.OrderId == Value;
                case (OrderProperty.TrackingNumber):
                    return order.TrackingNumber == Value;
                default:
                    return false;
            }
        }
    }
}