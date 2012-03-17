using System.Web.Mvc;
using VisualFarmStudio.Common.Exceptions;
using VisualFarmStudio.ExtensionMethods;
using VisualFarmStudio.Lib.UserInteraction;

namespace VisualFarmStudio.Controllers
{
    public abstract class VisualFarmControllerBase : Controller
    {
        public void AddUserMessageFor(UserException exception)
        {
            var message = UserMessage.Error(exception.Message, string.Empty);
            AddUserMessage(message);
        }

        protected void AddUserMessage(UserMessage message)
        {
            TempData.AddFlashMessage(message);
        }
    }
}