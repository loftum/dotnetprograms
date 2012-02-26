using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace StuffLibrary.Lib.UnitOfWork
{
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;
        private ITransaction _transaction;
        private readonly IList<Work> _workUnits = new List<Work>();

        public NHibernateUnitOfWork(ISession session)
        {
            _session = session;
        }

        public IWork Begin()
        {
            _transaction = _session.BeginTransaction();
            var work = new Work();
            work.Ended += HandleEnded;
            _workUnits.Add(work);
            return work;
        }

        private void HandleEnded(IWork sender, WorkEventArgs e)
        {
            if (_workUnits.Any(w => !w.Equals(sender) && !w.HasEnded))
            {
                return;
            }
            EndTransaction(_workUnits.All(w => w.IsComplete));
        }

        private void EndTransaction(bool commit)
        {
            if (_transaction != null && _transaction.IsActive)
            {
                if (commit)
                {
                    _transaction.Commit();
                }
                else
                {
                    _transaction.Rollback();
                }
            }
        }

        public void Dispose()
        {
            EndTransaction(false);
        }
    }
}