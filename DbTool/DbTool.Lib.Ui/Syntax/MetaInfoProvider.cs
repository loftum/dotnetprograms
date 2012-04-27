using DbTool.Lib.Meta;
using DbTool.Lib.Meta.Types;

namespace DbTool.Lib.Ui.Syntax
{
    public class MetaInfoProvider : IMetaInfoProvider
    {
        private readonly ITypeCache _typeCache;

        public MetaInfoProvider(ITypeCache typeCache)
        {
            _typeCache = typeCache;
        }

        public TagType GetTypeOf(string word)
        {
            var type = _typeCache.GetType(word);
            return type == null ? TagType.Nothing : TagType.Object;
        }

        public TypeMeta GetType(string word)
        {
            return _typeCache.GetType(word);
        }
    }
}