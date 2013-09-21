using System.Linq;

namespace WebShop.Core.Data
{
    public interface ISqlDatabase
    {
        IQueryable<T> GetAll<T>();
    }
}