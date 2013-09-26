using System;
using MasterData.Core.Domain.Pricing;

namespace MasterData.Core.Domain.MasterData
{
    public class SaleProduct : Product
    {
        public virtual ProductVariant Variant { get; set; }
        public override Product Parent { get { return Variant; } }
        public virtual Price BasePrice { get; set; }
        public virtual Price CalculatedPrice { get { return Calculator.Calculate(BasePrice); } }
        public virtual PriceCalculation Calculator { get; set; }
        public virtual Color Color { get { return FromVariantOrDefault(v => v.Color); } }

        private T FromVariantOrDefault<T>(Func<ProductVariant, T> property, T defaultValue = default(T))
        {
            return Variant == null ? defaultValue : property(Variant);
        }
        
        public SaleProduct()
        {
            BasePrice = new Price();
            Calculator = new PriceCalculation();
        }
    }
}