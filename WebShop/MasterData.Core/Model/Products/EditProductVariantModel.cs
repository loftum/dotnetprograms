using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DotNetPrograms.Common.Validation;

namespace MasterData.Core.Model.Products
{
    public class EditProductVariantModel : MasterDataObjectModel
    {
        public Guid MasterId { get; set; }
        public string Name { get; set; }
        public InheritableModel<string> Description { get; set; } 
        public string ProductNumber { get; set; }
        public Guid ColorId { get; set; }
        public IEnumerable<SelectListItem> AvailableColors { get; set; }

        public string DisplayName
        {
            get { return IsNew ? "New variant" : Name; }
        }

        public EditProductVariantModel() : this(Enumerable.Empty<SelectListItem>())
        {
        }

        public EditProductVariantModel(IEnumerable<SelectListItem> availableColors)
        {
            AvailableColors = availableColors;
            Description = new InheritableModel<string>();
        }

        public void UpdateFrom(EditProductVariantModel input)
        {
            Name = input.Name;
            Description = input.Description;
            ProductNumber = input.ProductNumber;
            ColorId = input.ColorId;
        }

        public ModelValidator<EditProductVariantModel> Validate()
        {
            return new ModelValidator<EditProductVariantModel>(this)
                .Require(m => m.ProductNumber)
                .Require(m => m.ColorId);
        }
    }
}