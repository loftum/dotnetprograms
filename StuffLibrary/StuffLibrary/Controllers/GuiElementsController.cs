using System.Web.Mvc;
using StuffLibrary.Models.GuiElements;
using StuffLibrary.Models.Messages;

namespace StuffLibrary.Controllers
{
    public class GuiElementsController : StuffLibraryControllerBase
    {
         public ActionResult Index()
         {
             var model = new GuiElementsViewModel();
             return View(model);
         }

        public JsonResult ShowSuccessMessage(GuiElementsViewModel model)
        {
            return Json(FlashMessage.Success(model.SuccessMessage));
        }

        public JsonResult ShowInfoMessage(GuiElementsViewModel model)
        {
            return Json(FlashMessage.Info(model.InfoMessage));
        }

        public JsonResult ShowWarningMessage(GuiElementsViewModel model)
        {
            return Json(FlashMessage.Warning(model.WarningMessage));
        }

        public JsonResult ShowErrorMessage(GuiElementsViewModel model)
        {
            return Json(FlashMessage.Error(model.ErrorMessage));
        }
    }
}