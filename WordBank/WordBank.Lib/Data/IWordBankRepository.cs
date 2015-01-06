using System.Linq;
using Wordbank.Lib.Domain;

namespace Wordbank.Lib.Data
{
    public interface IWordBankRepository
    {
        void Save<T>(T item) where T : DomainObject;
        T Get<T>(long id) where T : DomainObject;
        IQueryable<T> GetAll<T>() where T : DomainObject;
    }
}