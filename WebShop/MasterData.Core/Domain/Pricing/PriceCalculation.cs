using System.Collections.Generic;
using System.Linq;

namespace MasterData.Core.Domain.Pricing
{
    public class PriceCalculation
    {
        public virtual IList<PriceCalculator> Calculators { get; set; }

        public PriceCalculation()
        {
            Calculators = new List<PriceCalculator>();
        }

        public Price Calculate(Price price)
        {
            return Calculators.Aggregate(price, (current, calculation) => calculation.Calculate(current));
        }

        public void Add(PriceCalculator calculator)
        {
            Calculators.Add(calculator);
        }
    }
}