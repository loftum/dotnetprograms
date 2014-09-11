using System;

namespace Demo.Web.Models
{
    public class CombinedSearchInput : OrderSearchResult
    {
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        

        public CombinedSearchInput()
        {
            DateFrom = DateTime.Now.AddMonths(-1);
            DateTo = DateTime.Now;
        }
    }
}