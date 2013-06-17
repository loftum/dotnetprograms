using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.Communication.DbCommands.Dynamic;
using DbTool.Lib.Communication.DbCommands.Results;
using DotNetPrograms.Common.ExtensionMethods;

namespace DbTool.Lib.Communication.DbCommands.CSharp
{
    public class CollectionConverter
    {
        public IDbCommandResult Convert(IEnumerable values)
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
            if (type == typeof(DynamicDataRow))
            {
                return QueryResultOfDynamicDataRow(values.Cast<DynamicDataRow>());
            }
            if (type.IsAssignableFrom(typeof(IDictionary)))
            {
                return QueryResultOfDictionary((IEnumerable<IDictionary>)values);
            }

            return QueryResultOfType(values, type);
        }

        private IDbCommandResult QueryResultOfDynamicDataRow(IEnumerable<DynamicDataRow> values)
        {
            var result = new QueryResult();
            if (values.IsEmpty())
            {
                return result;
            }

            var columns = values.First().Columns;
            columns.Each(c => result.AddColumn(c, typeof(string)));
            foreach (var record in values)
            {
                var theRecord = record;
                result.AddRow(columns.Select(c => theRecord[c]));
            }
            return result;
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
                var theValue = value;
                var memberValues = properties.Select(p => p.GetValue(theValue, new object[0]));
                result.AddRow(memberValues);
            }
            return result;
        }

        private IDbCommandResult QueryResultOfValues(IEnumerable values, Type type)
        {
            var result = new QueryResult();
            result.AddColumn(type.Name, type);
            foreach (var value in values)
            {
                result.AddRow(value.AsArray());
            }
            return result;
        }
    }
}