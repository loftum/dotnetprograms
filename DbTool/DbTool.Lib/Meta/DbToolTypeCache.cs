using DbTool.Lib.CSharp;
using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Meta
{
    public class DbToolTypeCache : ITypeCache
    {
        private DatabaseSchema _schema;
        public DatabaseSchema Schema
        {
            get { return _schema; }
            set
            {
                _schema = value;
                GenerateAssembly();
            }
        }

        private readonly ICSharpEvaluator _evaluator;
        private readonly IDatabaseToAssemblyConverter _converter;

        private void GenerateAssembly()
        {
            var assembly = _converter.CreateFor(Schema);
            assembly.Save();
            _evaluator.ReferenceAssemblies(assembly.Assembly);
            _evaluator.Using(Schema.FullName);
        }

        public DbToolTypeCache(IDatabaseToAssemblyConverter converter,
            ICSharpEvaluator evaluator)
        {
            _converter = converter;
            _evaluator = evaluator;
        }

        public TableMeta GetType(string name)
        {
            return Schema == null ? null : Schema.GetTable(name);
        }
    }
}