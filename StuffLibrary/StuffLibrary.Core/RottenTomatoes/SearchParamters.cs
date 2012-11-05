namespace StuffLibrary.Core.RottenTomatoes
{
    public class SearchParamters
    {
        public string Query { get; private set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }

        public SearchParamters(string query, int pageNumber, int pageSize)
        {
            Query = query;
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        public SearchParamters(string query) : this(query, 1, 20)
        {
        }
    }
}