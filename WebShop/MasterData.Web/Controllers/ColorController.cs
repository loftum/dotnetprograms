using System;
using System.Web.Mvc;
using MasterData.Core.Facade;
using MasterData.Core.Model.Common;
using MasterData.Core.Model.Misc;
using MasterData.Web.Menus;

namespace MasterData.Web.Controllers
{
    [Menu("Misc")]
    public class ColorController : EditController<EditColorModel>
    {
        private readonly IColorFacade _colorFacade;

        public ColorController(IColorFacade colorFacade)
        {
            _colorFacade = colorFacade;
        }

        [MenuItem("Colors")]
        public ActionResult List()
        {
            var colors = _colorFacade.SearchColors(new SearchInput());
            return View(colors);
        }

        [MenuItem("New color")]
        public ActionResult New()
        {
            return RedirectToAction("Edit", new {id = Guid.Empty});
        }

        public override ActionResult Edit(Guid id)
        {
            Model = _colorFacade.Edit(id);
            return View(Model);
        }

        [HttpPost]
        public override ActionResult Edit(EditColorModel input)
        {
            try
            {
                Model.UpdateFrom(input);
                var saved = _colorFacade.Save(Model);
                AddSaveMessage(saved);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                AddError(ex);
                return View(Model);
            }
        }
    }
}