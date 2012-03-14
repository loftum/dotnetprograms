using System.Linq;
using NHibernate;
using NHibernate.Linq;
using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Core.Repository
{
    public class VisualFarmRepo : IVisualFarmRepo
    {
        private readonly ISession _session;

        public VisualFarmRepo(ISession session)
        {
            _session = session;
        }

        public TEntity Get<TEntity>(long id) where TEntity : DomainObject
        {
            return _session.Get<TEntity>(id);
        }

        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : DomainObject
        {
            return _session.Query<TEntity>();
        }

        public TEntity Save<TEntity>(TEntity entity) where TEntity : DomainObject
        {
            _session.Save(entity);
            return entity;
        }
    }
}