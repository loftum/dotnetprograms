namespace MasterData.Core.Domain.Pricing
{
    public abstract class PriceCalculator
    {
        public abstract Price Calculate(Price price);
    }
}