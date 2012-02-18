using DbTool.Lib.Objects;

namespace DbTool.Lib.Ui.Syntax
{
    public class MetaInfoProvider : IMetaInfoProvider
    {
        private readonly IObjectCache _objectCache;

        public MetaInfoProvider(IObjectCache objectCache)
        {
            _objectCache = objectCache;
        }

        public TagType GetTypeOf(string word)
        {
            var o = _objectCache.GetObjectType(word);
            return o == null ? TagType.Nothing : TagType.Object;
        }

        public DbToolObject GetObject(string word)
        {
            return _objectCache.GetObject(word);
        }
    }
}