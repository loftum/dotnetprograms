using System.Collections.Generic;
using MovieBase.Domain;

namespace MovieBase.Data.Dao
{
    public interface IMovieBaseRepository
    {
        T Save<T>(T item) where T : DomainObject;
        T Get<T>(long id) where T : DomainObject;
        IList<T> GetAll<T>() where T: DomainObject;
    }
}