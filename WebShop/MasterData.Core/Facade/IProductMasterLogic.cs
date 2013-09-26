using MasterData.Core.Model;

namespace MasterData.Core.Facade
{
    public interface IProductMasterLogic
    {
        PagedProductMasterList GetProducts();
    }
}