namespace DbTool.Lib.Communication.DbCommands
{
    public class MessageResult : DbCommandResultBase
    {
        public string Message { get; private set; }

        public MessageResult(string message)
        {
            Message = message;
        }

        protected override string ConvertToString()
        {
            return Message;
        }
    }
}