namespace MasterData.Core.Domain.Pricing
{
    public class MultiplyCalculator : PriceCalculator
    {
        private readonly decimal _factor;

        public MultiplyCalculator(decimal factor)
        {
            _factor = factor;
        }

        public override Price Calculate(Price price)
        {
            return price * _factor;
        }
    }
}