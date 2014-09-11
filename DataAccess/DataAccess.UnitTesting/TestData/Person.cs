using System;

namespace DataAccess.UnitTesting.TestData
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public Person()
        {
            BirthDate = new DateTime(1900, 01, 01);
        }

        public override string ToString()
        {
            return Describe.Object(this);
        }
    }
}