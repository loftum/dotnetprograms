namespace MasterData.Core.Model.Products
{
    public class ProducerModel : MasterDataObjectModel
    {
        public string Name { get; set; }

        public string DisplayName
        {
            get { return IsNew ? "New Producer" : Name; }
        }
    }
}