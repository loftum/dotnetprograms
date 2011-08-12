using System;
using NHibernate.Event;
using NHibernate.Persister.Entity;
using StuffLibrary.Common.DateAndTime;
using StuffLibrary.Domain;

namespace StuffLibrary.Repository.Configuration
{
    public class ChangeStampUpdater : IChangeStampUpdater, IPreInsertEventListener, IPreUpdateEventListener
    {
        private readonly IDateProvider _dateProvider;

        public ChangeStampUpdater(IDateProvider dateProvider)
        {
            _dateProvider = dateProvider;
        }

        public bool OnPreInsert(PreInsertEvent evt)
        {
            var entity = evt.Entity as DomainObject;
            SetChangeStampOn(entity, evt.Persister, evt.State);
            return false;
        }

        public bool OnPreUpdate(PreUpdateEvent evt)
        {
            var entity = evt.Entity as DomainObject;
            SetChangeStampOn(entity, evt.Persister, evt.State);
            return false;
        }

        private void SetChangeStampOn(DomainObject entity, IEntityPersister persister, object[] state)
        {
            if (entity == null)
            {
                return;
            }
            var now = _dateProvider.Now;

            if (entity.IsNew())
            {
                Set(persister, state, "CreatedAt", now);
                entity.CreatedAt = _dateProvider.Now;
            }
            Set(persister, state, "ModifiedAt", now);
            entity.ModifiedAt = _dateProvider.Now;
        }

        private static void Set(IEntityPersister persister, object[] state, string propertyName, object value)
        {
            var index = Array.IndexOf(persister.PropertyNames, propertyName);
            if (index == -1)
            {
                return;
            }
            state[index] = value;
        }
    }
}