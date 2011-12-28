using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DbTool.Lib.ExtensionMethods;
using Mono.CSharp;
using WebMatrix.Data;

namespace DbTool.Lib.Communication.DbCommands
{
    public class CSharpExecutor : IDbCommandExecutor
    {
        private readonly Evaluator _evaluator;

        public CSharpExecutor()
        {
            var report = new Report(new ConsoleReportPrinter());
            var parser = new CommandLineParser(report);
            var settings = parser.ParseArguments(new string[0]);
            settings.AssemblyReferences.Add("System.Core.dll");
            _evaluator = new Evaluator(settings, report);

            _evaluator.Run("using System;");
            _evaluator.Run("using System.Linq;");
            _evaluator.Run("using System.Collections.Generic;");
        }

        public IDbCommandResult Execute(string command)
        {
            if (command.StartsWith("using"))
            {
                _evaluator.Run(command);
                return new MessageResult("");
            }

            return GetResultOf(command);
        }

        private IDbCommandResult GetResultOf(string command)
        {
            object value;
            bool valueIsSet;
            var returnMessage = _evaluator.Evaluate(command, out value, out valueIsSet);
            var builder = new StringBuilder();
            if (valueIsSet)
            {
                if (value.ShouldBeViewedInTable())
                {
                    return QueryResultOf((IEnumerable) value);
                }
                builder.AppendLine(value.ToString());
            }
            if (!returnMessage.IsNotNullOrEmpty())
            {
                builder.AppendLine(returnMessage);
            }
            return new MessageResult(builder.ToString());
        }

        private IDbCommandResult QueryResultOf(IEnumerable values)
        {
            if (values.IsEmpty())
            {
                return new QueryResult();
            }
            var type = values.GetTypeOfValues();

            if (type.IsValueType || type == typeof(string))
            {
                return QueryResultOfValues(values, type);
            }
            if (type.IsAssignableFrom(typeof(IDictionary)))
            {
                return QueryResultOfDictionary((IEnumerable<IDictionary>) values);
            }
            
            return QueryResultOfType(values, type);
        }

        private IDbCommandResult QueryResultOfDictionary(IEnumerable<IDictionary> dictionaries)
        {
            var result = new QueryResult();

            var columnNames = new HashSet<object>();
            foreach (var dictionary in dictionaries)
            {
                foreach (var key in dictionary.Keys)
                {
                    columnNames.Add(key.ToString());
                }
            }

            foreach (var columnName in columnNames)
            {
                result.AddColumn(columnName.ToString(), typeof(string));
            }
            foreach (var dictionary in dictionaries)
            {
                var values = new List<string>();
                foreach (var columnName in columnNames)
                {
                    if (dictionary.Contains(columnName))
                    {
                        values.Add(dictionary[columnName].ToString());
                    }
                    else
                    {
                        values.Add(string.Empty);
                    }
                }
            }
            return result;
        }


        private IDbCommandResult QueryResultOfType(IEnumerable values, Type type)
        {
            var result = new QueryResult();
            var properties = type.GetProperties();
            if (properties.IsEmpty())
            {
                return QueryResultOfValues(values, type);
            }
            foreach (var property in properties)
            {
                result.AddColumn(property.Name, property.PropertyType);
            }
            foreach (var value in values)
            {
                var memberValues = properties.Select(p => p.GetValue(value, new object[0]));
                result.AddRow(memberValues);
            }
            return result;
        }

        private IDbCommandResult QueryResultOfValues(IEnumerable values, Type type)
        {
            var result = new QueryResult();
            result.AddColumn("Value", type);
            foreach (var value in values)
            {
                result.AddRow(value.AsArray());
            }
            return result;
        }
    }
}