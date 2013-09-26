using WebShop.Common.ExtensionMethods;

namespace WebShop.Common.Models.Pricing
{
    public class PriceModel
    {
        public decimal IncVat { get; private set; }
        public decimal ExVat { get; private set; }

        public PriceModel(decimal incVat, decimal exVat)
        {
            IncVat = incVat;
            ExVat = exVat;
        }

        public override string ToString()
        {
            return IncVat.ToFriendlyPrice();
        }
    }
}