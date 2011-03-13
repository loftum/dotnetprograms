using System.Collections.Generic;

namespace ZenTester.Lib.Domain
{
    public class Story : ZenObject
    {
        public string Text { get; set; }
        public string Details { get; set; }
        public int Size { get; set; }
        public string Color { get; set; }
        public string Status { get; set; }
        public Project Project { get; set; }
        public User Creator { get; set; }
        public User Owner { get; set; }
        public Phase Phase { get; set; }
        public IEnumerable<Step> Steps { get; set; }
        public IEnumerable<MileStone> MileStones { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}