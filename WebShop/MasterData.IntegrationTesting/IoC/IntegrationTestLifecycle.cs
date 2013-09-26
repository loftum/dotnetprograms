using StructureMap.Pipeline;

namespace MasterData.IntegrationTesting.IoC
{
    public class IntegrationTestLifecycle : ILifecycle
    {
        private readonly IObjectCache _objectCache = new MainObjectCache();

        public void EjectAll()
        {
            _objectCache.DisposeAndClear();
        }

        public IObjectCache FindCache()
        {
            return _objectCache;
        }

        public string Scope { get { return "Integration test"; } }
    }
}