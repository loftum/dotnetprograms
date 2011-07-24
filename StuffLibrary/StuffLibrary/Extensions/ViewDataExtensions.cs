using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StuffLibrary.Models.Messages;

namespace StuffLibrary.Extensions
{
    public static class ViewDataExtensions
    {
        private const string FlashMessageKey = "FlashMessages";

        public static void AddFlashMessage(this TempDataDictionary dictionary, FlashMessage flashMessage)
        {
            var messages = (ICollection<FlashMessage>) dictionary[FlashMessageKey] ?? new List<FlashMessage>();
            messages.Add(flashMessage);
        }

        public static bool HasFlashMessages(this TempDataDictionary dictionary)
        {
            var messages = dictionary.GetFlashMessages();
            return messages != null && messages.Count() > 0;
        }

        public static IEnumerable<FlashMessage> GetFlashMessages(this TempDataDictionary dictionary)
        {
            return (IEnumerable<FlashMessage>) dictionary[FlashMessageKey];
        }
    }
}