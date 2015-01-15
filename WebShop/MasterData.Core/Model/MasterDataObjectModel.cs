using System;
using DotNetPrograms.Common.ExtensionMethods;

namespace MasterData.Core.Model
{
    public abstract class MasterDataObjectModel
    {
        public bool IsNew { get { return Id.IsDefault(); } }
        public Guid Id { get; set; }
    }
}