using System.Web.Mvc;
using MasterData.Core.Facade;

namespace MasterData.Web.Controllers
{
    public class ProductMasterController : MasterDataControllerBase
    {
        private readonly IProductMasterLogic _productMasterLogic;

        public ProductMasterController(IProductMasterLogic productMasterLogic)
        {
            _productMasterLogic = productMasterLogic;
        }

        public ActionResult Index()
        {
            var products = _productMasterLogic.GetProducts();
            return View(products);
        }
    }
}