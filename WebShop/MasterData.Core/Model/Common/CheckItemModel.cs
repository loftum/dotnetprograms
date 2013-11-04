using System;

namespace MasterData.Core.Model.Common
{
    public class CheckItemModel
    {
        public Guid Id { get; set; }
        public bool Checked { get; set; }
        public string Name { get; set; }
    }
}