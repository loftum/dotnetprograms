using DotNetPrograms.Common.Validation;

namespace WebShop.Core.Users
{
    public class Address
    {
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string PostalCode { get; set; }
        public string PostalPlace { get; set; }

        public ModelValidator<Address> Validate()
        {
            return new ModelValidator<Address>(this)
                .Require(m => m.FirstLine)
                .Require(m => m.PostalCode)
                .Require(m => m.PostalPlace);
        }
    }
}