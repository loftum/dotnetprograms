namespace WebShop.Common.ExtensionMethods
{
    public static class DecimalExtensions
    {
        public static string ToFriendlyPrice(this decimal value)
        {
            return value.ToString("C");
        }
    }
}