using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using VisualFarmStudio.Common.ExtensionMethods;
using VisualFarmStudio.Lib.UserInteraction;

namespace VisualFarmStudio.ExtensionMethods
{
    public static class TempDataExtensions
    {
        private const string FlashMessageKey = "FlashMessages";

        public static void AddFlashMessage(this TempDataDictionary dictionary, UserMessage flashMessage)
        {
            var messages = (ICollection<UserMessage>)dictionary[FlashMessageKey];
            if (messages == null)
            {
                messages = new List<UserMessage>();
                dictionary[FlashMessageKey] = messages;
            }
            messages.Add(flashMessage);
        }

        public static bool HasErrorMessage(this TempDataDictionary dictionary)
        {
            return dictionary.GetFlashMessages().Any(m => m.Type == MessageType.Error);
        }

        public static bool HasFlashMessages(this TempDataDictionary dictionary)
        {
            var messages = dictionary.GetFlashMessages();
            return !messages.IsNullOrEmpty();
        }

        public static IEnumerable<UserMessage> GetFlashMessages(this TempDataDictionary dictionary)
        {
            var messages = dictionary == null
                ? Enumerable.Empty<UserMessage>()
                : (IEnumerable<UserMessage>)dictionary[FlashMessageKey];
            return messages.IsNullOrEmpty()
                ? Enumerable.Empty<UserMessage>()
                : messages;
        }
    }
}