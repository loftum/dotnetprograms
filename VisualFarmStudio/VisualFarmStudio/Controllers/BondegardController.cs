using System.Linq;
using System.Web.Mvc;
using VisualFarmStudio.Attributes;
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
            AddUserMessage(UserMessage.Error("Blah", "bleh"));
            var model = new BondegardIndexViewModel(_bondegardFacade.GetAllBondegards().ToList());
            return View(model);
        }
    }
}