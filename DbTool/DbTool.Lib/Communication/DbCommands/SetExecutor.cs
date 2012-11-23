using System;
using DbTool.Lib.Communication.DbCommands.Results;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
using DotNetPrograms.Common.ExtensionMethods;

namespace DbTool.Lib.Communication.DbCommands
{
    public class SetExecutor : IDbCommandExecutor
    {
        private readonly IDbToolConfig _config;

        public SetExecutor(IDbToolConfig config)
        {
            _config = config;
        }

        public IDbCommandResult Execute(string command)
        {
            var parts = command.SplitByWhitespace();
            if (parts.Length < 2)
            {
                return new MessageResult(_config.Settings.AsJson());
            }
            return Set(parts[1]);
        }

        private IDbCommandResult Set(string parts)
        {
            var split = parts.Split(new[] {'='});
            var propertyName = split[0].Trim();
            var value = split[1];

            try
            {
                _config.Settings.Set(propertyName, value);
                return new MessageResult(string.Format("{0} = {1}", propertyName, value));
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex);
            }
        }
    }
}