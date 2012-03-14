using System.Linq;
using System.Web.Mvc;
using VisualFarmStudio.Lib.Facades;
using VisualFarmStudio.Models;

namespace VisualFarmStudio.Controllers
{
    public class BondegardController : VFSControllerBase
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
    }
}