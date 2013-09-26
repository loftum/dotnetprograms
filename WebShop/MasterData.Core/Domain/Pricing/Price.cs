namespace MasterData.Core.Domain.Pricing
{
    public class Price
    {
        public decimal IncVat { get; private set; }
        public decimal ExVat { get; private set; }

        public static Price Zero
        {
            get { return new Price(0m, 0m); }
        }

        public Price() : this(0m, 0m)
        {
            
        }

        public Price(decimal incVat, decimal exVat)
        {
            IncVat = incVat;
            ExVat = exVat;
        }

        public static Price operator +(Price first, Price second)
        {
            return new Price(first.IncVat + second.IncVat, first.ExVat + second.ExVat);
        }

        public static Price operator *(Price price, decimal number)
        {
            return new Price(price.IncVat * number, price.ExVat * number);
        }

        public static Price operator *(decimal number, Price price)
        {
            return price * number;
        }

        public static Price operator /(Price price, decimal number)
        {
            return new Price(price.IncVat / number, price.ExVat / number);
        }

        public static Price FromExVat(decimal exVat, decimal vatRate)
        {
            return new Price(exVat * (1 + vatRate), exVat);
        }
    }
}