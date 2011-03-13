using System;

namespace ZenTester.Lib.Domain
{
    public class Step : ZenObject
    {
        public string Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long Duration { get; set; }
    }
}