using DotNetPrograms.Common.Validation;

namespace WebShop.Core.Users
{
    public class AddressModel
    {
        public string FirstLine { get; set; }
        public string SecondLine { get; set; }
        public string PostalCode { get; set; }
        public string PostalPlace { get; set; }

        public ModelValidator<AddressModel> Validate()
        {
            return new ModelValidator<AddressModel>(this)
                .Require(m => m.FirstLine)
                .Require(m => m.PostalCode)
                .Require(m => m.PostalPlace);
        }
    }
}