using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using WebShop.Common.Configuration;
using WebShop.Core.Data.Mapping;
using WebShop.Core.Domain.OrderDb;
using WebShop.Core.Users;

namespace WebShop.Core.Data
{
    public class OrderRepository : DbContext, IOrderRepository
    {
        private readonly IUserSession _userSession;
        private readonly IOrderNumberGenerator _orderNumberGenerator;

        public OrderRepository(IConfigSettings settings,
            IUserSession userSession,
            IOrderNumberGenerator orderNumberGenerator)
            : base(settings.OrderDbConnectionString)
        {
            _userSession = userSession;
            _orderNumberGenerator = orderNumberGenerator;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            new OrderDbMapper(modelBuilder).Map();
        }

        public T Get<T>(Guid id) where T : OrderDbObject
        {
            return Set<T>().FirstOrDefault(e => e.Id == id);
        }

        public IQueryable<T> GetAll<T>() where T : OrderDbObject
        {
            return Set<T>();
        }

        public T Save<T>(T item) where T : OrderDbObject
        {
            if (item.IsNew)
            {
                Set<T>().Add(item);
            }
            return item;
        }

        public void Commit()
        {
            foreach (var added in GetAddedEntries().OfType<OrderDbObjectWithChangeStamp>())
            {
                added.Id = Guid.NewGuid();
                added.ChangeStamp.YouWereCreatedNowBy(_userSession.User.FullName);
                AddOrderNumberIfOrder(added as OrderHead);
            }
            foreach (var modified in GetModifiedEntries().OfType<IHaveChangeStamp>())
            {
                modified.ChangeStamp.YouWereModifiedNowBy(_userSession.User.FullName);
            }
            SaveChanges();
        }

        private void AddOrderNumberIfOrder(OrderHead order)
        {
            if (order == null)
            {
                return;
            }
            order.OrderNumber = _orderNumberGenerator.GetNextOrderNumber();
        }

        public IEnumerable<OrderDbObject> GetModifiedEntries()
        {
            return ChangeTracker.Entries().Where(e => e.State == EntityState.Modified).Select(e => e.Entity).Cast<OrderDbObject>();
        }

        public IEnumerable<OrderDbObject> GetAddedEntries()
        {
            return ChangeTracker.Entries().Where(e => e.State == EntityState.Added).Select(e => e.Entity).Cast<OrderDbObject>();
        }
    }
}