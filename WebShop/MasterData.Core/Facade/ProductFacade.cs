using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DotNetPrograms.Common.ExtensionMethods;
using DotNetPrograms.Common.Meta;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Data;
using MasterData.Core.Domain.Products;
using MasterData.Core.Domain.Stores;
using MasterData.Core.Model;
using MasterData.Core.Model.Products;
using WebShop.Common.ExtensionMethods;

namespace MasterData.Core.Facade
{
    public class ProductFacade : IProductFacade
    {
        private readonly IMasterDataRepository _repo;

        public ProductFacade(IMasterDataRepository repo)
        {
            _repo = repo;
        }

        public IPagedList<ProductMasterModel> GetProducts(string searchText, int pageNumber, int pageSize)
        {
            return _repo.GetAll<ProductMaster>()
                        .Where(p => string.IsNullOrEmpty(searchText) || p.Name.Contains(searchText))
                        .Paged(pageNumber, pageSize, p => p.MapTo<ProductMasterModel>());
        }

        public EditProductMasterModel EditMaster(Guid id)
        {
            var master = id.IsDefault() ? new ProductMaster() : _repo.GetOrThrow<ProductMaster>(id);
            var model = master.MapTo<EditProductMasterModel>();
            model.AvailableProducers = new []{new SelectListItem{Text = "(Select producer)", Selected = true, Value = Guid.Empty.ToString()} }
                .Concat(_repo.GetAll<Producer>().OrderBy(p => p.Name).Select(p => p.MapTo<SelectListItem>()));
            model.AvailableProductTypes = new[]{new SelectListItem {Text = "(Select product type)", Selected = true, Value = Guid.Empty.ToString()}}
                .Concat(_repo.GetAll<ProductType>().OrderBy(t => t.Name).Select(t => t.MapTo<SelectListItem>()));

            return model;
        }

        public IObjectIdentifier Save(EditProductMasterModel input)
        {
            input.Validate().OrThrowPropertyError();
            var producer = _repo.GetOrThrow<Producer>(input.ProducerId);
            var productType = _repo.GetOrThrow<ProductType>(input.ProductTypeId);
            var master = input.IsNew ? new ProductMaster() : _repo.GetOrThrow<ProductMaster>(input.Id);
            master.Name = input.Name;
            master.Description = input.Description;
            master.ProductType = productType;

            producer.Add(master);

            _repo.Commit();
            return new ObjectIdentifier(master.Name);
        }

        public EditProductVariantModel EditVariant(Guid id)
        {
            var variant = _repo.GetOrThrow<ProductVariant>(id);
            var model = variant.MapTo<EditProductVariantModel>();
            model.AvailableColors = GetAvailableColors();
            return model;
        }

        private IEnumerable<SelectListItem> GetAvailableColors()
        {
            return new[] { new SelectListItem { Text = "(Select color)", Value = Guid.Empty.ToString(), Selected = true } }
                .Concat(_repo.GetAll<Color>().OrderBy(c => c.Name).Select(c => new SelectListItem { Text = c.Name, Value = c.Id.ToString() }));
        }

        public IObjectIdentifier Save(EditProductVariantModel input)
        {
            input.Validate().OrThrowPropertyError();
            var master = _repo.GetOrThrow<ProductMaster>(input.MasterId);
            var variant = input.IsNew ? new ProductVariant() : _repo.GetOrThrow<ProductVariant>(input.Id);
            if (variant.IsNew)
            {
                master.Add(variant);
            }
            variant.Name = input.Name;
            variant.Description = input.Description.OwnValue;
            variant.ProductNumber = input.ProductNumber;
            variant.Color = _repo.GetOrThrow<Color>(input.ColorId);
            _repo.Commit();

            return new ObjectIdentifier(variant.GetName());
        }

        public EditProductVariantModel NewVariantForMaster(Guid id)
        {
            var master = _repo.GetOrThrow<ProductMaster>(id);
            var model = new EditProductVariantModel
                {
                    MasterId = master.Id,
                    AvailableColors = GetAvailableColors(),
                    Name = master.Name
                };
            return model;
        }
    }
}