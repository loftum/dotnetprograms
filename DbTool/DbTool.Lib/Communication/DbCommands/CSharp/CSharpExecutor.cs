using System.Collections;
using System.Data.Common;
using System.Text;
using DbTool.Lib.CSharp;
using DbTool.Lib.CSharp.Mono;
using DbTool.Lib.Communication.DbCommands.Modifiers;
using DbTool.Lib.Communication.DbCommands.Results;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Communication.DbCommands.CSharp
{
    public class CSharpExecutor : ICSharpExecutor
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

        private DbConnection _connection;
        public DbConnection DbConnection
        {
            get { return _connection; }
            set
            {
                _connection = value;
                DbToolInteractive.SetConnection(value);
            }
        }

        public CSharpExecutor(ICSharpEvaluator cSharpEvaluator)
        {
            _collectionConverter = new CollectionConverter();
            _cSharpEvaluator = cSharpEvaluator;
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

            command = CommandModifier.Modify(command);
            var builder = new StringBuilder();
            builder.AppendFormat("Running: {0}", command).AppendLine();

            var result = _cSharpEvaluator.Run(command);

            if (result.ResultSet)
            {
                if (result.Result.ShouldBeViewedInTable())
                {
                    return _collectionConverter.Convert((IEnumerable)result.Result);
                }
                builder.AppendLine(result.Result == null ? "null" : result.Result.ToString());
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
    }
}