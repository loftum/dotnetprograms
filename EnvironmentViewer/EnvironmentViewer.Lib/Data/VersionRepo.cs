using System.Linq;
using EnvironmentViewer.Lib.Domain;
using NHibernate;
using NHibernate.Criterion;

namespace EnvironmentViewer.Lib.Data
{
    public class VersionRepo : IVersionRepo
    {
        private readonly ISessionFactory _sessionFactory;

        public VersionRepo(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public long GetVersion()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var schemaInfo = session.CreateCriteria<SchemaInfo>()
                    .AddOrder(Order.Desc("Version"))
                    .List<SchemaInfo>()
                    .FirstOrDefault();
                var version = VersionOf(schemaInfo);
                return version;
            }
        }

        private static long VersionOf(SchemaInfo schemaInfo)
        {
            return schemaInfo == null ? -1 : schemaInfo.Version;
        }

        public void Dispose()
        {
            if (_sessionFactory != null)
            {
                if (!_sessionFactory.IsClosed)
                {
                    _sessionFactory.Close();
                }
                _sessionFactory.Dispose();
            }
        }
    }
}