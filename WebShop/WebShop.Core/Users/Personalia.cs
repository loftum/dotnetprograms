using DotNetPrograms.Common.Validation;

namespace WebShop.Core.Users
{
    public class Personalia
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public Address Address { get; set; }

        public Personalia()
        {
            Address = new Address();
        }

        public ModelValidator<Personalia> Validate()
        {
            return new ModelValidator<Personalia>(this)
                .Require(m => m.FirstName)
                .Require(m => m.LastName)
                .Require(m => m.PhoneNumber)
                .Require(m => m.Email)
                .Append(Address.Validate());
        }
    }
}