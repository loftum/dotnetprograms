using System.Web.Mvc;
using StuffLibrary.Common.Exceptions;
using StuffLibrary.Common.ExtensionMethods;
using StuffLibrary.Extensions;
using StuffLibrary.Models.Messages;

namespace StuffLibrary.Controllers
{
    public class StuffLibraryControllerBase : Controller
    {
        public void AddFlashMessageFor(UserException exception)
        {
            var message = FlashMessage.Error(exception.Type.GetDescription());
            TempData.AddFlashMessage(message);
        }
    }
}