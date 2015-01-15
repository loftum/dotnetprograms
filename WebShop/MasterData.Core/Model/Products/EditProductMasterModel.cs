using System;
using System.Collections.Generic;
using System.Web.Mvc;
using DotNetPrograms.Common.Validation;

namespace MasterData.Core.Model.Products
{
    public class EditProductMasterModel : MasterDataObjectModel
    {
        public string DisplayName { get { return IsNew ? "New Product Master" : Name; } }
        public string Name { get; set; }
        public string Description { get; set; }

        public Guid ProductTypeId { get; set; }
        public IEnumerable<SelectListItem> AvailableProductTypes { get; set; }
        public Guid ProducerId { get; set; }
        public IEnumerable<SelectListItem> AvailableProducers { get; set; }

        public ModelValidator<EditProductMasterModel> Validate()
        {
            return new ModelValidator<EditProductMasterModel>(this)
                .Require(m => m.Name)
                .Require(m => m.Description)
                .Require(m => m.ProductTypeId, "Product type must be set")
                .Require(m => m.ProducerId, "Producer must be set");
        }

        public void UpdateFrom(EditProductMasterModel input)
        {
            Name = input.Name;
            Description = input.Description;
            ProductTypeId = input.ProductTypeId;
            ProducerId = input.ProducerId;
        }
    }
}