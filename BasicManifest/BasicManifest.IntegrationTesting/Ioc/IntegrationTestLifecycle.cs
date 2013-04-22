using StructureMap.Pipeline;

namespace BasicManifest.IntegrationTesting.Ioc
{
    public class IntegrationTestLifecycle : ILifecycle
    {
        public string Scope { get { return "IntegrationTest"; } }

        private readonly IObjectCache _cache = new MainObjectCache();

        public void EjectAll()
        {
            FindCache().DisposeAndClear();
        }

        public IObjectCache FindCache()
        {
            return _cache;
        }
    }
}