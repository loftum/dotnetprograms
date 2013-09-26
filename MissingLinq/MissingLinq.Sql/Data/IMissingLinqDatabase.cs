using System.Linq;

namespace MissingLinq.Sql.Data
{
    public interface IMissingLinqDatabase
    {
        IQueryable<T> GetAll<T>();
    }
}