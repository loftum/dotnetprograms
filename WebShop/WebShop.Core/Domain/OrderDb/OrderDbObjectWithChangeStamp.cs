namespace WebShop.Core.Domain.OrderDb
{
    public abstract class OrderDbObjectWithChangeStamp : OrderDbObject, IHaveChangeStamp
    {
        public ChangeStamp ChangeStamp { get; set; }

        protected OrderDbObjectWithChangeStamp()
        {
            ChangeStamp = new ChangeStamp();
        }
    }
}