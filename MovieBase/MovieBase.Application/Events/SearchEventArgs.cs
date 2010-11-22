namespace MovieBase.AppLib.Events
{
    public class SearchEventArgs
    {
        public string SearchText { get; private set; }

        public SearchEventArgs(string searchText)
        {
            SearchText = searchText;
        }
    }
}
