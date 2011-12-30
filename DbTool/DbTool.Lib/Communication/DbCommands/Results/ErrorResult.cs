using System;
using DbTool.Lib.Exceptions;

namespace DbTool.Lib.Communication.DbCommands.Results
{
    public class ErrorResult : DbCommandResultBase
    {
        public string Message { get; private set; }

        public ErrorResult(Exception exception)
        {
            if (exception is DbToolException)
            {
                Message = exception.Message;    
            }
            else
            {
                Message = exception.ToString();
            }
        }

        protected override string ConvertToString()
        {
            return Message;
        }
    }
}