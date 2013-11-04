using System;
using System.Web.Mvc;

namespace MasterData.Web.Controllers
{
    public abstract class EditController<TModel> : MasterDataControllerBase where TModel : class
    {
        private readonly string _modelKey = typeof (TModel).Name;

        protected TModel Model
        {
            get { return (TModel)Session[_modelKey]; }
            set
            {
                if (value == null)
                {
                    Session.Remove(_modelKey);
                }
                else
                {
                    Session[_modelKey] = value;    
                }
            }
        }

        public abstract ActionResult Edit(Guid id);

        [HttpPost]
        public abstract ActionResult Edit(TModel input);
    }
}