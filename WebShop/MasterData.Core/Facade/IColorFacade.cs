using System;
using DotNetPrograms.Common.Paging;
using MasterData.Core.Model;
using MasterData.Core.Model.Common;
using MasterData.Core.Model.Misc;
using MasterData.Core.Model.Products;

namespace MasterData.Core.Facade
{
    public interface IColorFacade
    {
        IPagedList<ColorModel> SearchColors(SearchInput searchInput);
        EditColorModel Edit(Guid id);
        IObjectIdentifier Save(EditColorModel input);
    }
}