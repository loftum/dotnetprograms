namespace MasterData.Core.Model.Products
{
    public class ProductTypeModel : MasterDataObjectModel
    {
        public string Name { get; set; }
        public string DisplayName { get { return IsNew ? "New product type" : Name; } }

        public decimal VatRate { get; set; }

        public void UpdateFrom(ProductTypeModel input)
        {
            Name = input.Name;
            VatRate = input.VatRate;
        }
    }
}