using System.Collections.Generic;
using System.Linq;
using NHibernate;

namespace BasicManifest.Data.Transactions
{
    public class NHibernateUnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;
        private ITransaction _transaction;
        private readonly IList<WorkUnit> _workUnits = new List<WorkUnit>();

        public NHibernateUnitOfWork(ISession session)
        {
            _session = session;
        }

        protected void HandleEnded(WorkUnit sender, WorkEventArgs e)
        {
            sender.Ended -= HandleEnded;
            if (_workUnits.Any(w => w != sender && !w.HasEnded))
            {
                return;
            }
            EndTransaction(_workUnits.All(w => w.IsComplete));
        }

        public WorkUnit Begin()
        {
            if (_transaction == null || !_transaction.IsActive)
            {
                _transaction = _session.BeginTransaction();
            }
            var work = new WorkUnit();
            work.Ended += HandleEnded;
            _workUnits.Add(work);
            return work;
        }

        protected void EndTransaction(bool commit)
        {
            _workUnits.Clear();
            if (_transaction == null || !_transaction.IsActive)
            {
                return;
            }

            try
            {
                if (commit)
                {
                    TryCommitOrRollback();
                }
                else
                {
                    _transaction.Rollback();
                }
            }
            finally
            {
                if (_transaction != null)
                {
                    _transaction.Dispose();
                    _transaction = null;
                }
            }
        }

        private void TryCommitOrRollback()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
        }

        public void Dispose()
        {
            EndTransaction(false);
        }
    }
}