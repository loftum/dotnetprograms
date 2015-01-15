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

        public PriceCalculation(params PriceCalculator[] calculators) : this()
        {
            foreach (var calculator in calculators)
            {
                Add(calculator);
            }
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