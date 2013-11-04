namespace MasterData.Core.Model.Suppliers
{
    public class SupplierModel : MasterDataObjectModel
    {
        public string DisplayName { get { return IsNew ? "New supplier" : Name; } }
        public string Name { get; set; }
    }
}