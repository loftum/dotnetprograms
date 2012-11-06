namespace StuffLibrary.Lib.RottenTomatoes
{
    public class SearchParamters
    {
        public bool HasQuery
        {
            get { return !string.IsNullOrEmpty(Query); }
        }
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

        public string GetCacheKey()
        {
            return ToString();
        }

        public override string ToString()
        {
            return string.Format("{0}-{1}-{2}-{3}", GetType().Name, Query, PageNumber, PageSize);
        }
    }
}