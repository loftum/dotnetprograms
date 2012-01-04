using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using DbTool.Lib.CSharp;
using DbTool.Lib.CSharp.WebMatrix;
using DbTool.Lib.Communication.DbCommands.Results;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Communication.DbCommands.CSharp
{
    public class CSharpExecutor : IDbCommandExecutor
    {
        private readonly ICSharpEvaluator _cSharpEvaluator;
        private readonly CollectionConverter _collectionConverter;
        private WebMatrixQuery _query = new WebMatrixQuery();
        private DbToolDatabase _db;
        public DbToolDatabase Db
        {
            get { return _db; }
            set
            {
                _db = value;
                _cSharpEvaluator.Run(string.Format("_query.ConnectionString = \"{0}\";", _db.GetConnectionData().GetConnectionString()));
                _cSharpEvaluator.Run(string.Format("_query.ProviderName = \"{0}\";", _db.GetConnectionData().ProviderName));
            }
        }

        public CSharpExecutor()
        {
            _collectionConverter = new CollectionConverter();
            _cSharpEvaluator = new MonoCSharpEvaluator();
        }

        public IDbCommandResult Execute(string command)
        {
            if (command.StartsWith("using "))
            {
                _cSharpEvaluator.Run(command);
                return new MessageResult("");
            }
            
            return GetResultOf(command);
        }

        private IDbCommandResult GetResultOf(string command)
        {
            if (command.Equals("vars"))
            {
                return new MessageResult(string.Format("Vars:\n{0}", string.Join(Environment.NewLine, _cSharpEvaluator.Vars)));
            }
            if (command.Equals("usings"))
            {
                return new MessageResult(string.Format("Usings:\n{0}", string.Join(Environment.NewLine, _cSharpEvaluator.Usings)));
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
            return new MessageResult(builder.ToString());
        }

        private string ModifySql(string command)
        {
            if (command.Matches(@"\${1}\({1}[\s\S]+\)"))
            {
                var rawSql = Regex.Match(command, @"\${1}\({1}[\s\S]+\)").Value;
                var query = rawSql.Replace("$", "_query.Query").Replace("(", "(\"").Replace(")", "\")");
                command = command.Replace(rawSql, query);
            }
            return command;
        }
    }
}