namespace DotNetPrograms.Common.UnitTests.Common.Mapping
{
    public class Price
    {
        public decimal IncVat { get; private set; }
        public decimal ExVat { get; private set; }

        public Price(decimal incVat, decimal exVat)
        {
            IncVat = incVat;
            ExVat = exVat;
        }
    }
}