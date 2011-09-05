using DbToolGui.Data;

namespace DbToolGui.Highlighting
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

        public bool IsObject(string word)
        {
            return Schema != null && Schema.ContainsObject(word);
        }
    }
}