using DotNetPrograms.Common.Validation;

namespace WebShop.Core.Users
{
    public class PersonaliaModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AddressModel Address { get; set; }

        public PersonaliaModel()
        {
            Address = new AddressModel();
        }

        public ModelValidator<PersonaliaModel> Validate()
        {
            return new ModelValidator<PersonaliaModel>(this)
                .Require(m => m.FirstName)
                .Require(m => m.LastName)
                .Require(m => m.PhoneNumber)
                .Require(m => m.Email)
                .Append(Address.Validate());
        }
    }
}