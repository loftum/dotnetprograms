using StuffLibrary.Domain;

namespace StuffLibrary.Models.Grids
{
    public abstract class GridRowViewModelBase<TDomainObject> : IGridRowViewModel
        where TDomainObject : DomainObject
    {
        protected TDomainObject Item;

        protected GridRowViewModelBase(TDomainObject item)
        {
            Item = item;
        }

        public string RowId
        {
            get { return Item.Id.ToString(); }
        }

        public abstract object OrderByValue(string orderBy);

        public JqGridRow ToJqGridRow()
        {
            return new JqGridRow(RowId, this);
        }
    }
}