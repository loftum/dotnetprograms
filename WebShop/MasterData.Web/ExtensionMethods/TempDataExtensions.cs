using System.Collections.Generic;
using System.Web.Mvc;
using DotNetPrograms.Common.UserInteraction;

namespace MasterData.Web.ExtensionMethods
{
    public static class TempDataExtensions
    {
        public static void AddMessage(this TempDataDictionary tempData, UserMessage message)
        {
            var messages = GetOrCreateMessages(tempData);
            messages.Add(message);
        }

        private static IList<UserMessage> GetOrCreateMessages(TempDataDictionary tempData)
        {
            var messages = tempData["UserMessages"] as List<UserMessage>;
            if (messages == null)
            {
                messages = new List<UserMessage>();
                tempData["UserMessages"] = messages;
            }
            return messages;
        }

        public static IEnumerable<UserMessage> GetMessages(this TempDataDictionary tempData)
        {
            return GetOrCreateMessages(tempData);
        } 
    }
}