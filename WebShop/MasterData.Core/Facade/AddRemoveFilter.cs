using System.Collections.Generic;
using System.Linq;
using MasterData.Core.Domain;
using MasterData.Core.Model.Common;

namespace MasterData.Core.Facade
{
    public class AddRemoveFilter<T> where T : MasterDataObject
    {
        private readonly IList<T> _existing;
        private readonly IList<CheckItemModel> _inputItems; 

        public IEnumerable<CheckItemModel> ToAdd
        {
            get { return _inputItems.Where(i => i.Checked && _existing.All(e => e.Id != i.Id)); }
        }

        public IEnumerable<T> ToRemove
        {
            get { return _existing.Where(e => _inputItems.Any(i => !i.Checked && i.Id == e.Id)); }
        } 

        public AddRemoveFilter(IEnumerable<T> exising, IEnumerable<CheckItemModel> inputItems)
        {
            _existing = exising.ToList();
            _inputItems = inputItems.ToList();
        } 
    }
}