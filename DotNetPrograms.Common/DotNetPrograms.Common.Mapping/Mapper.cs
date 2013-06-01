using DotNetPrograms.Common.Mapping.Internal;

namespace DotNetPrograms.Common.Mapping
{
    public class Mapper<TSource, TTarget>
    {
        private readonly IMapRegistry _registry = new MapRegistry();
        private readonly ITypeMap _map;

        public Mapper()
        {
            _map = _registry.GetMap<TSource, TTarget>();
        }

        public string Mappings
        {
            get { return _registry.ToString(); }
        }

        public TTarget Map(TSource source)
        {
            var target = _map.Map(source);
            return (TTarget) target;
        }

        public void ValidateProperties(TSource source, TTarget target)
        {
        }
    }
}