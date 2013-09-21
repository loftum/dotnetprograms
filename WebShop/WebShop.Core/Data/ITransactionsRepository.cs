using System;
using System.Linq;
using WebShop.Core.Domain.Transactions;

namespace WebShop.Core.Data
{
    public interface ITransactionsRepository
    {
        T Get<T>(Guid id) where T : TransactionsObject;
        IQueryable<T> GetAll<T>() where T : TransactionsObject;
        T Save<T>(T item) where T : TransactionsObject;
        void Commit();
    }
}