using System;
using System.Linq;
using DotNetPrograms.Common.ExtensionMethods;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Data;
using MasterData.Core.Domain.Products;
using MasterData.Core.Model;
using MasterData.Core.Model.Common;
using MasterData.Core.Model.Misc;
using MasterData.Core.Model.Products;
using WebShop.Common.ExtensionMethods;

namespace MasterData.Core.Facade
{
    public class ColorFacade : IColorFacade
    {
        private readonly IMasterDataRepository _repo;

        public ColorFacade(IMasterDataRepository repo)
        {
            _repo = repo;
        }

        public IPagedList<ColorModel> SearchColors(SearchInput searchInput)
        {
            return _repo.GetAll<Color>()
                .Where(c => string.IsNullOrEmpty(searchInput.SearchText) || c.Name.Contains(searchInput.SearchText))
                .Paged(searchInput.PageNumber, searchInput.PageSize, c => c.MapTo<ColorModel>());
        }

        public EditColorModel Edit(Guid id)
        {
            var color = id.IsDefault() ? new Color() : _repo.GetOrThrow<Color>(id);
            var model = color.MapTo<EditColorModel>();
            return model;
        }

        public IObjectIdentifier Save(EditColorModel input)
        {
            input.Validate().OrThrowPropertyError();
            var color = input.IsNew ? new Color() : _repo.GetOrThrow<Color>(input.Id);
            color.Name = input.Name;
            color.Red = input.Red;
            color.Green = input.Green;
            color.Blue = input.Blue;
            color.Alpha = input.Alpha;
            _repo.Save(color);
            _repo.Commit();
            return new ObjectIdentifier(color.Name);
        }
    }
}