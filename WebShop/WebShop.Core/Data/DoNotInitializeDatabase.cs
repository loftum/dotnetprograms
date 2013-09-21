using System.Data.Entity;

namespace WebShop.Core.Data
{
    public class DoNotInitializeDatabase<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        public void InitializeDatabase(TContext context)
        {
        }
    }
}