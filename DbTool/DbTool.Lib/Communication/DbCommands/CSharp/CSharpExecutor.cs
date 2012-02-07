using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using DbTool.Lib.CSharp;
using DbTool.Lib.CSharp.Mono;
using DbTool.Lib.Communication.DbCommands.Results;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Communication.DbCommands.CSharp
{
    public class CSharpExecutor : IDbCommandExecutor
    {
        private readonly ICSharpEvaluator _cSharpEvaluator;
        private readonly CollectionConverter _collectionConverter;
        private DbToolDatabase _db;
        public DbToolDatabase Db
        {
            get { return _db; }
            set
            {
                _db = value;
                DbToolInteractive.SetDb(value);
            }
        }

        public CSharpExecutor()
        {
            _collectionConverter = new CollectionConverter();
            _cSharpEvaluator = new MonoCSharpEvaluator();
        }

        public IDbCommandResult Execute(string command)
        {
            return GetResultOf(command);
        }

        private IDbCommandResult GetResultOf(string command)
        {
            if (command.Equals("reset"))
            {
                _cSharpEvaluator.Init();
                return new MessageResult(string.Format("C# Evaluator is reset"));
            }

            command = ModifySql(command);
            var builder = new StringBuilder();
            builder.AppendFormat("Running: {0}", command).AppendLine();

            var result = _cSharpEvaluator.Run(command);

            if (result.ResultSet)
            {
                if (result.Result.ShouldBeViewedInTable())
                {
                    return _collectionConverter.Convert((IEnumerable)result.Result);
                }
                builder.AppendLine(result.Result.ToString());
            }
            if (result.HasMessage)
            {
                builder.AppendLine(result.Message);
            }
            if (result.HasReport)
            {
                builder.AppendLine(result.Report);
            }
            return new MessageResult(builder.ToString());
        }

        private string ModifySql(string command)
        {
            const string pattern = @"\${1}(<[^\s]+>)?\({1}[^\(^\)]+\){1}";
            if (command.Matches(pattern))
            {
                var rawSql = Regex.Match(command, pattern).Value;
                var query = rawSql.Replace("$", "Query")
                    .Replace("(", "(\"")
                    .Replace(")", "\")");
                command = command.Replace(rawSql, query);
            }
            return command;
        }
    }
}