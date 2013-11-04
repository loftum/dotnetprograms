using System;
using System.Collections.Generic;
using System.Linq;
using MasterData.Core.Domain.Pricing;
using MasterData.Core.Domain.Products;

namespace MasterData.Core.Domain.Stores
{
    public abstract class Store : MasterDataObject
    {
        public abstract Store Parent { get; }
        public virtual bool HasParent { get { return Parent != null; } }
        public virtual string Name { get; set; }
        public virtual PriceCalculation PriceCalculation { get; set; }
        public virtual PriceCalculation GetPriceCalculation()
        {
            if (PriceCalculation != null)
            {
                return PriceCalculation;
            }
            if (HasParent)
            {
                return Parent.GetPriceCalculation();
            }
            return new PriceCalculation(new MultiplyCalculator((decimal)Math.PI));
        }
        
        public virtual MasterDataObjectFilter<StoreProduct> SaleProductFilter { get; protected set; }

        protected Store()
        {
            SaleProductFilter = new MasterDataObjectFilter<StoreProduct> { DefaultInclude = true };
        }
        public abstract IEnumerable<StoreProduct> GetAvailableStoreProducts();
        public virtual Assortment GetAssortment()
        {
            return new Assortment(GetAvailableStoreProducts().ToList());
        }
    }
}