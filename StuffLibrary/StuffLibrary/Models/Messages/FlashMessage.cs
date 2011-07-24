using StuffLibrary.Common.ExtensionMethods;

namespace StuffLibrary.Models.Messages
{
    public class FlashMessage
    {
        public MessageType Type { get; set; }
        public string TypeDisplay { get { return Type.GetDescription(); } }
        public string Title { get; set; }
        public string Details { get; set; }

        public static FlashMessage Success(string title, string details = null)
        {
            return new FlashMessage { Type = MessageType.Success, Title = title, Details = details };
        }

        public static FlashMessage Info(string title, string details = null)
        {
            return new FlashMessage { Type = MessageType.Info, Title = title, Details = details };
        }

        public static FlashMessage Warning(string title, string details = null)
        {
            return new FlashMessage { Type = MessageType.Warning, Title = title, Details = details };
        }

        public static FlashMessage Error(string title, string details = null)
        {
            return new FlashMessage{Type = MessageType.Error, Title = title, Details = details};
        }
    }
}