using System;
using System.Linq;
using WebShop.Core.Domain.OrderDb;

namespace WebShop.Core.Data
{
    public interface IOrderRepository
    {
        T Get<T>(Guid id) where T : OrderDbObject;
        IQueryable<T> GetAll<T>() where T : OrderDbObject;
        T Save<T>(T item) where T : OrderDbObject;
        void Commit();
    }
}