using System;

namespace ZenTester.Lib.Domain
{
    public class Comment : ZenObject
    {
        public string Text { get; set; }
        public DateTime CreateTime { get; set; }
        public User Author { get; set; }
    }
}