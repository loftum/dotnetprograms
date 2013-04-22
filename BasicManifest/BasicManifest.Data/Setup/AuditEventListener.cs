using System;
using System.Collections.Generic;
using BasicManifest.Core.Domain;
using DotNetPrograms.Common.DateAndTime;
using NHibernate.Event;
using NHibernate.Persister.Entity;

namespace BasicManifest.Data.Setup
{
    public class AuditEventListener : IAuditEventListener
    {
        private const string CreatedDate = "CreatedDate";
        private const string ModifiedDate = "ModifiedDate";
        private const string CreatedBy = "CreatedBy";
        private const string ModifiedBy = "ModifiedBy";

        public bool OnPreInsert(PreInsertEvent e)
        {
            var audit = e.Entity as IAuditable;
            if (audit == null)
            {
                return false;
            }

            var createdDate = DateTimeProvider.Now;

            Store(e.Persister, e.State, CreatedDate, createdDate);
            Store(e.Persister, e.State, ModifiedDate, createdDate);
            Store(e.Persister, e.State, CreatedBy, CurrentUser.Name);
            Store(e.Persister, e.State, ModifiedBy, CurrentUser.Name);
            audit.CreatedDate = createdDate;
            audit.ModifiedDate = createdDate;
            audit.CreatedBy = CurrentUser.Name;
            audit.ModifiedBy = CurrentUser.Name;

            return false;
        }

        public bool OnPreUpdate(PreUpdateEvent e)
        {
            var audit = e.Entity as IAuditable;
            if (audit == null)
            {
                return false;
            }

            var date = DateTimeProvider.Now;

            Store(e.Persister, e.State, ModifiedDate, date);
            Store(e.Persister, e.State, ModifiedBy, CurrentUser.Name);
            audit.ModifiedDate = date;
            audit.ModifiedBy = CurrentUser.Name;

            return false;
        }

        private static void Store(IEntityPersister persister, IList<object> state, string propertyName, object value)
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