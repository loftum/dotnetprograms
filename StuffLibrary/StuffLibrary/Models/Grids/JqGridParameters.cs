using StuffLibrary.Common.ExtensionMethods;

namespace StuffLibrary.Models.Grids
{
    public class JqGridParameters
    {
        public string Query { get; set; }
        // ReSharper disable InconsistentNaming
        public int page { get; set; }
        public int rows { get; set; }
        public string sidx { get; set; }
        public string sord { get; set; }
        public int rowNum { get; set; }
        // ReSharper restore InconsistentNaming

        public OrderType OrderType
        {
            get { return sord.Equals("desc") ? OrderType.Descending : OrderType.Ascending; }
        }
    }
}