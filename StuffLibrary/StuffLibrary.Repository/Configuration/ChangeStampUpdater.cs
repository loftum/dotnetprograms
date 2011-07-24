using NHibernate.Event;
using NHibernate.Event.Default;
using StuffLibrary.Common.DateAndTime;
using StuffLibrary.Domain;
using StuffLibrary.Domain.ExtensionMethods;

namespace StuffLibrary.Repository.Configuration
{
    public class ChangeStampUpdater : DefaultSaveEventListener, IChangeStampUpdater
    {
        private readonly IDateProvider _dateProvider;

        public ChangeStampUpdater(IDateProvider dateProvider)
        {
            _dateProvider = dateProvider;
        }

        protected override object PerformSaveOrUpdate(SaveOrUpdateEvent evt)
        {
            var entity = (DomainObject) evt.Entity;
            if (entity != null)
            {
                ProcessEntityBeforeInsert(entity);
            }
            return base.PerformSaveOrUpdate(evt);
        }

        private void ProcessEntityBeforeInsert(DomainObject entity)
        {
            if (entity.IsNew())
            {
                entity.CreatedAt = _dateProvider.Now;
            }
            entity.ModifiedAt = _dateProvider.Now;
        }
    }
}