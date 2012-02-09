using DbTool.Lib.Data;

namespace DbTool.Lib.Ui.Syntax
{
    public class SchemaObjectProvider : ISchemaObjectProvider
    {
        private readonly object _lock = new object();

        private Schema _schema;
        public Schema Schema
        {
            get
            {
                lock(_lock)
                {
                    return _schema;
                }
            }
            set
            {
                lock (_lock)
                {
                    _schema = value;
                }
            }
        }

        public TagType GetTypeOf(string word)
        {
            if (Schema == null)
            {
                return TagType.Nothing;
            }
            var lowercase = word.ToLowerInvariant();
            if (Schema.ContainsTable(lowercase))
            {
                return TagType.Object;
            }
            if (Schema.ContainsColumn(lowercase))
            {
                return TagType.Property;
            }
            return TagType.Nothing;
        }
    }
}