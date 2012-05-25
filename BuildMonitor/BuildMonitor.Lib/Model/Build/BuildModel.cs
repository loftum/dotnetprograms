using System;
using BuildMonitor.Common.ExtensionMethods;

namespace BuildMonitor.Lib.Model.Build
{
    public class BuildModel
    {
        public string Id { get; set; }
        public string Number { get; set; }
        private string _status;
        public string Status
        {
            get { return _status.ToLowerInvariant(); }
            set { _status = value.IsNullOrWhiteSpace() ? "unknown" : value.ToLowerInvariant(); }
        }
        public string BuildTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }

        public DateTime TriggeredDate { get; set; }
        public BuildUser TriggeredBy { get; set; }
    }
}