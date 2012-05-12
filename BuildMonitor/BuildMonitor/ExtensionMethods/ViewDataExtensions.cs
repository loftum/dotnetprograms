using System.Web.Mvc;
using BuildMonitor.Models.Shared;

namespace BuildMonitor.ExtensionMethods
{
    public static class ViewDataExtensions
    {
        public static TopModel GetTopModel(this ViewDataDictionary viewData)
        {
            return (TopModel) viewData["TopModel"] ?? new TopModel();
        }

        public static void SetTopModel(this ViewDataDictionary viewData, TopModel model)
        {
            viewData["TopModel"] = model;
        }
    }
}