using System;
using System.Collections.Generic;
using System.Linq;
using DotNetPrograms.Common.ExtensionMethods;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Data;
using MasterData.Core.Domain.Products;
using MasterData.Core.Domain.Stores;
using MasterData.Core.Model;
using MasterData.Core.Model.Common;
using MasterData.Core.Model.Suppliers;
using WebShop.Common.ExtensionMethods;

namespace MasterData.Core.Facade
{
    public class SupplierFacade : ISupplierFacade
    {
        private readonly IMasterDataRepository _repo;

        public SupplierFacade(IMasterDataRepository repo)
        {
            _repo = repo;
        }

        public IPagedList<SupplierModel> GetSuppliers(string searchText, int pageNumber, int pageSize)
        {
            return _repo.GetAll<Supplier>()
                .Where(s => string.IsNullOrEmpty(searchText) || s.Name.Contains(searchText))
                .Paged(pageNumber, pageSize, s => s.MapTo<SupplierModel>());
        }

        public EditSupplierModel Edit(Guid id)
        {
            var supplier = id.IsDefault() ? new Supplier() : _repo.GetOrThrow<Supplier>(id);
            var model = supplier.MapTo<EditSupplierModel>();
            model.Products = GetProductsForSupplier(supplier);
            return model;
        }

        private IList<CheckItemModel> GetProductsForSupplier(Supplier supplier)
        {
            var existing = supplier.Products.ToList();
            return _repo.GetAll<ProductVariant>()
                .ToList()
                .Select(v => new CheckItemModel{Id = v.Id, Name = v.Name, Checked = existing.Any(p => p.Variant == v)})
                .ToList();
        }

        public IObjectIdentifier Save(EditSupplierModel input)
        {
            var supplier = input.IsNew ? new Supplier() : _repo.GetOrThrow<Supplier>(input.Id);
            supplier.Name = input.Name;
            var existing = supplier.Products.ToList();
            var productsToAdd = input.Products.Where(p => p.Checked && existing.All(sp => sp.Variant.Id != p.Id));
            var productsToRemove = existing.Where(sp => input.Products.Any(p => !p.Checked && p.Id == sp.Variant.Id));

            foreach (var toRemove in productsToRemove)
            {
                supplier.Remove(toRemove);
                _repo.Delete(toRemove);
            }
            foreach (var toAdd in productsToAdd)
            {
                supplier.Add(new SupplierProduct(_repo.Get<ProductVariant>(toAdd.Id)));
            }

            _repo.Save(supplier);
            _repo.Commit();
            return new ObjectIdentifier(supplier.Name);
        }
    }
}