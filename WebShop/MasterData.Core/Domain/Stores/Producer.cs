using System.Collections.Generic;
using MasterData.Core.Domain.Products;

namespace MasterData.Core.Domain.Stores
{
    public class Producer : MasterDataObject, IHaveName
    {
        public virtual string Name { get; set; }
        public virtual IList<ProductMaster> Products { get; set; }

        public Producer()
        {
            Products = new List<ProductMaster>();
        }

        public virtual void Add(ProductMaster master)
        {
            Products.Add(master);
            master.Producer = this;
        }
    }
}