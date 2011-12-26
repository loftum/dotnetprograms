using System;
using System.Collections.Generic;
using System.Linq;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Communication.DbCommands.DbSchema
{
    public class SchemaQuery
    {
        public bool HasNoCollectionName
        {
            get { return CollectionName.IsNullOrEmpty(); }
        }
        public string CollectionName { get; private set; }
        public IEnumerable<string> ColumnNames { get; private set; }
        public string[] RestrictionValues { get; private set; }

        public SchemaQuery(string query)
        {
            Validate(query);
            CollectionName = GetCollectionNameFrom(query);
            RestrictionValues = GetRestrictionValuesFrom(query);
        }

        private static string[] GetRestrictionValuesFrom(string query)
        {
            var split = query.Split(new[] { ' ' }).ToList();
            if (split.Count < 3)
            {
                return new string[0];
            }
            return split.Skip(2).Select(v => v.Replace("'", "")).ToArray();
        }

        private static string GetCollectionNameFrom(string query)
        {
            var split = query.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return split.Length < 2
                ? null
                : split[1];
        }
    }
}