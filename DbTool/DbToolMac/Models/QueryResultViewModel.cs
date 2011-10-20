using System;
using DbTool.Lib.Ui.Models;
using MonoMac.Foundation;

namespace DbToolMac.Models
{
    public class QueryResultViewModel : NSObject, IQueryResultViewModel
    {
        [Export("hideResultText")]
        public bool HideResultText { get; set; }
        [Export("hideResultTable")]
        public bool HideReulsTable { get; set; }

    }
}

