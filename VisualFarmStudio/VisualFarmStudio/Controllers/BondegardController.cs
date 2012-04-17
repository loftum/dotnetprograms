using System;
using System.Linq;
using System.Web.Mvc;
using VisualFarmStudio.Attributes;
using VisualFarmStudio.Common.Exceptions;
using VisualFarmStudio.Lib.Facades;
using VisualFarmStudio.Lib.UserInteraction;
using VisualFarmStudio.Lib.UserSession;
using VisualFarmStudio.Models;

namespace VisualFarmStudio.Controllers
{
    [VFSAuthorize(new[] {UserRole.Bruker})]
    public class BondegardController : VisualFarmControllerBase
    {
        private readonly IBondegardFacade _bondegardFacade;

        public BondegardController(IBondegardFacade bondegardFacade)
        {
            _bondegardFacade = bondegardFacade;
        }

        public ActionResult Index()
        {
            var model = new BondegardIndexViewModel(_bondegardFacade.GetAllBondegards().ToList());
            return View(model);
        }

        public ActionResult Edit(long id)
        {
            try
            {
                var model = _bondegardFacade.GetBondegard(id);
                return View(new BondegardViewModel(model));
            }
            catch (UserException ex)
            {
                AddUserMessageFor(ex);
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public ActionResult Edit(BondegardViewModel model)
        {
            try
            {
                _bondegardFacade.Save(model.Bondegard);
                AddUserMessage(UserMessage.Success(string.Format("{0} er lagret", model.Bondegard.Navn), string.Empty));
                return RedirectToAction("Edit", new {id = model.Bondegard.Id});
            }
            catch (UserException ex)
            {
                AddUserMessageFor(ex);
                return RedirectToAction("Edit", new {id = model.Bondegard.Id});
            }
            catch(Exception e)
            {
                AddUserMessage(UserMessage.Error(string.Format("Hey! Noen kastet {0}", e.GetType().Name), string.Empty));
                return RedirectToAction("Edit", new { id = model.Bondegard.Id });
            }
        }
    }
}