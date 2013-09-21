namespace WebShop.Core.Model
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
    }
}