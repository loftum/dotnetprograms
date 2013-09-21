using System;
using System.Data.Entity;
using System.Linq;
using WebShop.Core.Domain.Transactions;

namespace WebShop.Core.Data
{
    public class TransactionsRepository : DbContext, ITransactionsRepository
    {
        public T Get<T>(Guid id) where T : TransactionsObject
        {
            return Set<T>().FirstOrDefault(e => e.Id == id);
        }

        public IQueryable<T> GetAll<T>() where T : TransactionsObject
        {
            return Set<T>();
        }

        public T Save<T>(T item) where T : TransactionsObject
        {
            if (item.IsNew)
            {
                Set<T>().Add(item);
            }
            return item;
        }

        public void Commit()
        {
            SaveChanges();
        }
    }
}