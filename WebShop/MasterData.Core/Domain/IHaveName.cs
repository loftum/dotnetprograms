using System;

namespace MasterData.Core.Domain
{
    public interface IHaveName
    {
        Guid Id { get; }
        string Name { get; }
    }
}