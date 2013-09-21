namespace WebShop.Core.Model
{
    public class ProductModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PriceModel Price { get; set; }

        public ProductModel()
        {
            Price = new PriceModel(0m, 0m);
        }
    }
}