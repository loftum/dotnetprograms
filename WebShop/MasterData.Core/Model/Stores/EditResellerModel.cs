using System.Collections.Generic;
using System.Linq;
using DotNetPrograms.Common.Validation;
using MasterData.Core.Model.Common;

namespace MasterData.Core.Model.Stores
{
    public class EditResellerModel : MasterDataObjectModel
    {
        public string Name { get; set; }

        public string DisplayName
        {
            get { return IsNew ? "New Reseller" : Name; }
        }

        public IList<CheckItemModel> Suppliers { get; set; }

        public EditResellerModel()
        {
            Suppliers = new List<CheckItemModel>();
        }

        public void UpdateFrom(EditResellerModel input)
        {
            Name = input.Name;
            foreach (var supplier in input.Suppliers)
            {
                Suppliers.Single(s => s.Id == supplier.Id).Checked = supplier.Checked;
            }
        }

        public ModelValidator<EditResellerModel> Validate()
        {
            return new ModelValidator<EditResellerModel>(this)
                .Require(m => m.Name);
        }
    }
}