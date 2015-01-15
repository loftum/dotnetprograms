namespace MasterData.Core.Model.Products
{
    public class ProductVariantModel : MasterDataObjectModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ColorModel Color { get; set; }
    }
}