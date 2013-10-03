using System;
using System.Linq;
using MasterData.Core.Domain.MasterData;

namespace MasterData.Core.Data
{
    public interface IMasterDataRepository
    {
        T Get<T>(Guid id) where T : MasterDataObject;
        T GetOrThrow<T>(Guid id) where T : MasterDataObject;
        IQueryable<T> GetAll<T>() where T : MasterDataObject;
        T Save<T>(T item) where T : MasterDataObject;
        void Commit();
        IQueryable<T> Linq<T>() where T : MasterDataObject;
    }
}