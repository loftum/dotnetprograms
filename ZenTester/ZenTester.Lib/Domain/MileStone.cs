using System;

namespace ZenTester.Lib.Domain
{
    public class MileStone : ZenObject
    {
        public Phase Phase { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long Duration { get; set; }
    }
}