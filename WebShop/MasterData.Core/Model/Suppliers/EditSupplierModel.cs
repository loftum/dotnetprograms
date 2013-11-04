using System.Collections.Generic;
using System.Linq;
using MasterData.Core.Model.Common;

namespace MasterData.Core.Model.Suppliers
{
    public class EditSupplierModel : MasterDataObjectModel
    {
        public string DisplayName { get { return IsNew ? "New supplier" : Name; } }
        public string Name { get; set; }
        public IList<CheckItemModel> Products { get; set; }

        public EditSupplierModel()
        {
            Products = new List<CheckItemModel>();
        }

        public void UpdateFrom(EditSupplierModel input)
        {
            Name = input.Name;
            foreach (var product in Products)
            {
                product.Checked = input.Products.Single(p => p.Id == product.Id).Checked;
            }
        }
    }
}