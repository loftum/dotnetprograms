using MasterData.Core.Domain.Pricing;
using MasterData.Core.Domain.Stores;

namespace MasterData.Core.Domain.Products
{
    public class SaleProduct : MasterDataObject
    {
        public virtual Salespoint Salespoint { get; set; }
        public virtual string Name { get; set; }
        public virtual string Description { get; set; }
        public virtual Price Price { get; set; }
        public virtual string ProductNumber { get; set; }
        public virtual string SearchableText { get; set; }

        public virtual void PrepareToDie()
        {
            Salespoint = null;
        }
    }
}