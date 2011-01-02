using System.Collections.Generic;
using HourGlass.Lib.Domain;

namespace HourGlass.Lib.Data
{
    public interface IHourGlassRepo
    {
        T Get<T>(long id) where T : DomainObject;
        T Save<T>(T item) where T : DomainObject;
        T Delete<T>(T item) where T : DomainObject;
        IEnumerable<T> GetAll<T>() where T : DomainObject;
    }
}