using System;

namespace DbTool.Lib.Communication.Commands
{
    public class ErrorResult : DbCommandResultBase
    {
        public string Message { get; private set; }

        public ErrorResult(Exception exception)
        {
            Message = exception.Message;
        }

        protected override string ConvertToString()
        {
            return Message;
        }
    }
}