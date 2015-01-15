using System;
using System.Web.Mvc;
using MasterData.Core.Facade;
using MasterData.Core.Model;
using MasterData.Core.Model.Products;
using MasterData.Web.Menus;

namespace MasterData.Web.Controllers
{
    [Menu(2)]
    public class ProducerController : MasterDataControllerBase
    {
        private readonly IProducerFacade _producerFacade;

        public ProducerController(IProducerFacade producerFacade)
        {
            _producerFacade = producerFacade;
        }

        [MenuItem]
        public ActionResult New()
        {
            return RedirectToAction("Edit", new { id = Guid.Empty });
        }

        [MenuItem]
        public ActionResult List()
        {
            var producers = _producerFacade.GetProducers(null, 1, 20);
            return View(producers);
        }

        public ActionResult Edit(Guid id)
        {
            try
            {
                var producer = _producerFacade.Get(id);
                return View(producer);
            }
            catch (Exception ex)
            {
                AddError(ex);
                return RedirectToReferrer();
            }
        }

        [HttpPost]
        public ActionResult Edit(ProducerModel input)
        {
            try
            {
                var saved = _producerFacade.Save(input);
                AddSaveMessage(saved);
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                AddError(ex);
                return View(input);
            }
        }
    }
}