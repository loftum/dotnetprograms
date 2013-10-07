namespace WebShop.Core.Data
{
    public interface IOrderNumberGenerator
    {
        long GetNextOrderNumber();
    }
}