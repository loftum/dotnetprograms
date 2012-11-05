namespace DotNetPrograms.Common.UserInteraction
{
    public class UserMessage
    {
        public MessageType Type { get; private set; }
        public string Title { get; private set; }
        public string Message { get; private set; }

        private UserMessage(MessageType type, string title, string message)
        {
            Type = type;
            Title = title;
            Message = message;
        }

        public static UserMessage Info(string title, string message)
        {
            return new UserMessage(MessageType.Info, title, message);
        }

        public static UserMessage Success(string title, string message)
        {
            return new UserMessage(MessageType.Success, title, message);
        }

        public static UserMessage Warning(string title, string message)
        {
            return new UserMessage(MessageType.Warning, title, message);
        }

        public static UserMessage Error(string title, string message)
        {
            return new UserMessage(MessageType.Error, title, message);
        }
    }
}