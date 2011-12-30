using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using DbTool.Lib.CSharp.WebMatrix;
using DbTool.Lib.Communication.DbCommands.Results;
using DbTool.Lib.Configuration;
using DbTool.Lib.ExtensionMethods;
using Mono.CSharp;

namespace DbTool.Lib.Communication.DbCommands.CSharp
{
    public class CSharpExecutor : IDbCommandExecutor
    {
        private readonly Evaluator _evaluator;
        private readonly CollectionConverter _collectionConverter;
        private WebMatrixQuery _query = new WebMatrixQuery();
        private DbToolDatabase _db;
        public DbToolDatabase Db
        {
            get { return _db; }
            set
            {
                _db = value;
                object result;
                bool resultSet;
                _evaluator.Evaluate(string.Format("_query.ConnectionString = \"{0}\";", _db.GetConnectionData().GetConnectionString()), out result, out resultSet);
                _evaluator.Evaluate(string.Format("_query.ProviderName = \"{0}\";", _db.GetConnectionData().ProviderName),out result,out resultSet);
            }
        }

        public CSharpExecutor()
        {
            _collectionConverter = new CollectionConverter();
            var report = new Report(new ConsoleReportPrinter());
            var parser = new CommandLineParser(report);
            var settings = parser.ParseArguments(new string[0]);
            settings.AssemblyReferences.Add("System.Core.dll");
            settings.AssemblyReferences.Add("WebMatrix.Data.dll");
            settings.AssemblyReferences.Add("DbTool.Lib.CSharp.dll");
            object result;
            bool resultSet;
            _evaluator = new Evaluator(settings, report);
            
            _evaluator.Run("using System;");
            _evaluator.Run("using System.Linq;");
            _evaluator.Run("using System.Collections.Generic;");
            _evaluator.Run("using WebMatrix.Data;");
            _evaluator.Run("using DbTool.Lib.CSharp.WebMatrix;");
            _evaluator.Evaluate("var _query=new WebMatrixQuery();", out result, out resultSet);
        }

        public IDbCommandResult Execute(string command)
        {
            if (command.StartsWith("using "))
            {
                _evaluator.Run(command);
                return new MessageResult("");
            }
            
            return GetResultOf(command);
        }

        private IDbCommandResult GetResultOf(string command)
        {
            if (command.Equals("vars"))
            {
                var vars = _evaluator.GetVars();
                return new MessageResult(string.Format("Vars:\n{0}",vars));
            }
            if (command.Equals("usings"))
            {
                var usings = _evaluator.GetUsing();
                return new MessageResult(string.Format("Usings:\n{0}", usings));
            }

            command = ModifySql(command);
            var builder = new StringBuilder();
            builder.AppendFormat("Running: {0}", command).AppendLine();

            object value;
            bool valueIsSet;
            var returnMessage = _evaluator.Evaluate(command, out value, out valueIsSet);
            
            if (valueIsSet)
            {
                if (value.ShouldBeViewedInTable())
                {
                    return _collectionConverter.Convert((IEnumerable)value);
                }
                builder.AppendLine(value.ToString());
            }
            if (!returnMessage.IsNotNullOrEmpty())
            {
                builder.AppendLine(returnMessage);
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