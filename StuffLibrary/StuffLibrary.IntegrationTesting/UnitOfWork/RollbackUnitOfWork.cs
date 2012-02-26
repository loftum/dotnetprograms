using NHibernate;
using StuffLibrary.Lib.UnitOfWork;

namespace StuffLibrary.IntegrationTesting.UnitOfWork
{
    public class RollbackUnitOfWork : IUnitOfWork
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        public RollbackUnitOfWork(ISession session)
        {
            _session = session;
        }

        public IWork Begin()
        {
            _transaction = _session.BeginTransaction();
            return new Work();
        }

        public void Rollback()
        {
            if (_transaction == null || !_transaction.IsActive)
            {
                return;
            }
            _transaction.Rollback();
            _transaction.Dispose();
        }

        public void Dispose()
        {
            Rollback();
        }
    }
}