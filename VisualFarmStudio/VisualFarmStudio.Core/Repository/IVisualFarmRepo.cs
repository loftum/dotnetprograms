using System.Linq;
using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Core.Repository
{
    public interface IVisualFarmRepo
    {
        TEntity Get<TEntity>(long id) where TEntity : DomainObject;
        IQueryable<TEntity> GetAll<TEntity>() where TEntity : DomainObject;
        TEntity Save<TEntity>(TEntity entity) where TEntity : DomainObject;
    }
}