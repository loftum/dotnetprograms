using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DbTool.Lib.Exceptions;
using DbTool.Lib.ExtensionMethods;

namespace DbTool.Lib.Communication.DbCommands.DbSchema
{
    public class SchemaQuery
    {
        private const string QueryPattern =
            @"^show[\s]+([\S\s]+)[\s]+from[\s]+([\S]+)[[\s]+where[\s]+([\S]+)[\s]*([=]{1})[\s]*([\S]+)[\s]*]?$";

        public bool HasNoCollectionName
        {
            get { return CollectionName.IsNullOrEmpty(); }
        }
        public string CollectionName { get; private set; }
        public IEnumerable<string> ColumnNames { get; private set; }
        public bool SelectAll { get; private set; }

        public SchemaQuery(string query)
        {
            Validate(query);
            Parse(query);
            SelectAll = !ColumnNames.Any() || ColumnNames.Contains("*");
        }

        private void Parse(string query)
        {
            var regex = new Regex(QueryPattern);
            var match = regex.Match(query);
            ColumnNames = match.Groups[1].Value.Split(new[] {','})
                .Select(c => c.ToLowerInvariant().Trim())
                .Where(c => !c.IsNullOrEmpty());
            CollectionName = match.Groups[2].Value.Trim();
        }

        private void Validate(string query)
        {
            if (!query.Matches(QueryPattern))
            {
                throw new UserException(ExceptionType.InvalidSchemaQuery);
            }
        }
    }
}