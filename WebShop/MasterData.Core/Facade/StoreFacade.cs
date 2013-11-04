using System;
using System.Collections.Generic;
using System.Linq;
using DotNetPrograms.Common.ExtensionMethods;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Data;
using MasterData.Core.Domain.Stores;
using MasterData.Core.Model;
using MasterData.Core.Model.Common;
using MasterData.Core.Model.Stores;
using WebShop.Common.ExtensionMethods;

namespace MasterData.Core.Facade
{
    public class StoreFacade : IStoreFacade
    {
        private readonly IMasterDataRepository _repo;

        public StoreFacade(IMasterDataRepository repo)
        {
            _repo = repo;
        }

        public IPagedList<ResellerModel> SearchResellers(string searchText, int pageNumber, int pageSize)
        {
            return _repo.GetAll<Reseller>()
                .Where(r => string.IsNullOrEmpty(searchText) || r.Name.Contains(searchText))
                .Paged(pageNumber, pageSize, r => r.MapTo<ResellerModel>());
        }

        public IPagedList<SalespointModel> SearchSalespoints(string searchText, int pageNumber, int pageSize)
        {
            return _repo.GetAll<Salespoint>()
                .Where(s => string.IsNullOrEmpty(searchText) || s.Name.Contains(searchText))
                .Paged(pageNumber, pageSize, s => s.MapTo<SalespointModel>());
        }

        public void GenerateSaleProductsForSalespoint(Guid id)
        {
            var salespoint = _repo.GetOrThrow<Salespoint>(id);
            salespoint.RecalculateSaleproducts();
            _repo.Commit();
        }

        public EditResellerModel EditReseller(Guid id)
        {
            var reseller = id.IsDefault() ? new Reseller() : _repo.GetOrThrow<Reseller>(id);
            var model = reseller.MapTo<EditResellerModel>();
            model.Suppliers = GetSuppliersForReseller(reseller);
            return model;
        }

        public EditSalespointModel EditSalespoint(Guid id)
        {
            var salespoint = _repo.GetOrThrow<Salespoint>(id);
            var model = salespoint.MapTo<EditSalespointModel>();
            return model;
        }

        public EditSalespointModel NewForReseller(Guid id)
        {
            var reseller = _repo.GetOrThrow<Reseller>(id);
            var model = new EditSalespointModel();
            model.ResellerId = reseller.Id;
            return model;
        }

        private IList<CheckItemModel> GetSuppliersForReseller(Reseller reseller)
        {
            var all = _repo.GetAll<Supplier>().ToList();
            var existing = reseller.Suppliers.ToList();
            return all
                .Select(s => new CheckItemModel {Id = s.Id, Name = s.Name, Checked = existing.Any(e => e.Id == s.Id)})
                .ToList();
        }

        public IObjectIdentifier Save(EditResellerModel input)
        {
            input.Validate().OrThrowPropertyError();
            var reseller = input.IsNew ? new Reseller() : _repo.GetOrThrow<Reseller>(input.Id);
            reseller.Name = input.Name;

            var filter = new AddRemoveFilter<Supplier>(reseller.Suppliers, input.Suppliers);
            foreach (var toRemove in filter.ToRemove)
            {
                reseller.RemoveSupplier(toRemove);
            }
            foreach (var item in filter.ToAdd)
            {
                var supplier = _repo.GetOrThrow<Supplier>(item.Id);
                reseller.AddSupplier(supplier);
            }
            _repo.Save(reseller);
            _repo.Commit();
            return new ObjectIdentifier(reseller.Name);
        }

        public IObjectIdentifier Save(EditSalespointModel input)
        {
            input.Validate().OrThrowPropertyError();
            var reseller = _repo.GetOrThrow<Reseller>(input.ResellerId);
            var salespoint = input.IsNew ? new Salespoint() : _repo.GetOrThrow<Salespoint>(input.Id);
            salespoint.Name = input.Name;
            salespoint.Identifier = input.Identifier;
            if (salespoint.IsNew)
            {
                reseller.AddSalespoint(salespoint);
            }
            _repo.Save(reseller);
            _repo.Commit();
            return new ObjectIdentifier(salespoint.Name);
        }
    }
}