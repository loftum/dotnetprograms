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
            return Display(true);
        }

        public decimal GetValue(bool incVat)
        {
            return incVat ? IncVat : ExVat;
        }

        public string Display(bool incVat)
        {
            return GetValue(incVat).ToFriendlyPrice();
        }

        public static PriceModel operator +(PriceModel first, PriceModel second)
        {
            return new PriceModel(first.IncVat + second.IncVat, first.ExVat + second.ExVat);
        }

        public static PriceModel Zero()
        {
            return new PriceModel(0, 0);
        }
    }
}