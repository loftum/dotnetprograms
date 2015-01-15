using System.Collections.Generic;

namespace MasterData.Core.Domain.Products
{
    public class ProductType : MasterDataObject, IHaveName
    {
        public virtual string Name { get; set; }
        public virtual decimal VatRate { get; set; }

        public virtual IList<ProductMaster> ProductMasters { get; set; }

        public ProductType()
        {
            ProductMasters = new List<ProductMaster>();
            VatRate = 0.25m;
        }
    }
}