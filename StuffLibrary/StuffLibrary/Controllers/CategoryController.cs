using System.Linq;
using System.Web.Mvc;
using StuffLibrary.Domain;
using StuffLibrary.Lib.BusinessLogic;
using StuffLibrary.Models.Categories;
using StuffLibrary.Models.Grids;

namespace StuffLibrary.Controllers
{
    public class CategoryController : StuffLibraryControllerBase
    {
        private readonly ICategoryLogic _categoryLogic;

        public CategoryController(ICategoryLogic categoryLogic)
        {
            _categoryLogic = categoryLogic;
        }

        public ActionResult Index()
        {
            var model = new CategoryIndexViewModel();
            return View(model);
        }

        public ActionResult JsonCategories()
        {
            var categories = from category 
                                 in _categoryLogic.GetCategories()
                                 select new CategoryGridRowViewModel(category);
            return Json(new GridViewModel(categories), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Edit(long id)
        {
            var categeory = _categoryLogic.GetCategory(id);
            var model = new CategoryViewModel(categeory);
            return View(model);
        }

        public ActionResult RegisterNew()
        {
            var model = new CategoryViewModel(new Category());
            return View("Edit", model);
        }

        [HttpPost]
        public ActionResult Save(CategoryViewModel model)
        {
            _categoryLogic.Save(model.Category);
            return RedirectToAction("Index");
        }
    }
}