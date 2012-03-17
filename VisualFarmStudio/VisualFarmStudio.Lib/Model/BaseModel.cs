using VisualFarmStudio.Core.Domain;

namespace VisualFarmStudio.Lib.Model
{
    public abstract class BaseModel<TEntity> where TEntity : DomainObject, new()
    {
        public long Id { get; private set; }

        protected BaseModel()
        {
        }

        protected BaseModel(TEntity entity)
        {
            Id = entity.Id;
        }

        public TEntity ToEntity()
        {
            return MapTo(new TEntity{Id = Id});
        }

        protected abstract TEntity MapTo(TEntity entity);
    }
}