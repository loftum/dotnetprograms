using System.Collections.Generic;
using System.Linq;
using MasterData.Core.Domain.Inheritance;

namespace MasterData.Core.Model
{
    public class InheritableModel<TValue>
    {
        public TValue OwnValue { get; set; }
        public TValue InheritedValue { get; set; }
        public IList<InheritableModel<TValue>> Heritage { get; private set; }

        public InheritableModel()
        {
            Heritage = new List<InheritableModel<TValue>>();
        }

        public InheritableModel(Inheritable<TValue> inheritable)
        {
            OwnValue = inheritable.OwnValue;
            InheritedValue = inheritable.Value;
            Heritage = inheritable.Heritage
                .Select(h => new InheritableModel<TValue>(h))
                .ToList();
        } 
    }
}