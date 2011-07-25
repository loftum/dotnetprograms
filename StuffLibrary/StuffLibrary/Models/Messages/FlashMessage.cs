using StuffLibrary.Common.ExtensionMethods;

namespace StuffLibrary.Models.Messages
{
    public class FlashMessage
    {
        public MessageType Type { get; set; }
        public string TypeDisplay { get { return Type.GetDescription(); } }
        public string Message { get; set; }
        public string Details { get; set; }

        public static FlashMessage Success(string message, string details = null)
        {
            return new FlashMessage { Type = MessageType.Success, Message = message, Details = details };
        }

        public static FlashMessage Info(string message, string details = null)
        {
            return new FlashMessage { Type = MessageType.Info, Message = message, Details = details };
        }

        public static FlashMessage Warning(string message, string details = null)
        {
            return new FlashMessage { Type = MessageType.Warning, Message = message, Details = details };
        }

        public static FlashMessage Error(string message, string details = null)
        {
            return new FlashMessage{Type = MessageType.Error, Message = message, Details = details};
        }
    }
}