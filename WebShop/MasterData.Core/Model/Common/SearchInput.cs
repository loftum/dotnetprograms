namespace MasterData.Core.Model.Common
{
    public class SearchInput
    {
        public string SearchText { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        public SearchInput() : this(null, 1, 20)
        {
        }

        public SearchInput(string searchText, int pageNumber, int pageSize)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
            SearchText = searchText;
        }
    }
}