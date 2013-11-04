using System.Collections.Generic;

namespace MasterData.Core.Model.Products
{
    public class ProductMasterModel : MasterDataObjectModel
    {
        public string DisplayName { get { return IsNew ? "New Product Master" : Name; } }
        public string Name { get; set; }
        public string Description { get; set; }

        public IList<ProductVariantModel> Variants { get; set; }

        public ProductMasterModel()
        {
            Variants = new List<ProductVariantModel>();
        }
    }
}