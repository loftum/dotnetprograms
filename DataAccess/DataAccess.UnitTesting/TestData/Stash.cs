using System;

namespace DataAccess.UnitTesting.TestData
{
    public class Stash
    {
        public Guid Id { get; set; }
        public Guid PersonId { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
    }
}