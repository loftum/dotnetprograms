using System.Web.Mvc;
using StuffLibrary.Lib.BusinessLogic;

namespace StuffLibrary.Controllers
{
    public class CategoryController : StuffLibraryControllerBase
    {
        private readonly ICategoryLogic _categoryLogic;

        public CategoryController(ICategoryLogic categoryLogic)
        {
            _categoryLogic = categoryLogic;
        }

        public ActionResult JsonCategories()
        {
            var categories = _categoryLogic.GetCategories();
            return Json(categories, JsonRequestBehavior.AllowGet);
        }
    }
}