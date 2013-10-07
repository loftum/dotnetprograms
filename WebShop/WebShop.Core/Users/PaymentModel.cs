using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DotNetPrograms.Common.ExtensionMethods;
using DotNetPrograms.Common.Meta;
using DotNetPrograms.Common.Validation;

namespace WebShop.Core.Users
{
    public class PaymentModel
    {
        public PaymentType Type { get; set; }
        public IEnumerable<SelectListItem> AvailablePaymentTypes { get; private set; }
        public string CardNumber { get; set; }
        public string CardHolder { get; set; }
        public int ExpireMonth { get; set; }
        public int ExpireYear { get; set; }
        public int Cvc { get; set; }

        public PaymentModel()
        {
            AvailablePaymentTypes = new EnumMeta<PaymentType>()
                .GetValues()
                .Select(v => new SelectListItem {Text = v.ToString(), Value = ((int) v).ToInvariantString()});
        }

        public ModelValidator<PaymentModel> Validate()
        {
            return new ModelValidator<PaymentModel>(this)
                .Require(m => m.CardNumber)
                .Require(m => m.CardHolder)
                .Require(m => m.ExpireMonth, To.BeAtLeast(1))
                .Require(m => m.ExpireMonth, To.BeAtMost(12))
                .Require(m => m.ExpireYear, To.BeAtLeast(1))
                .Require(m => m.ExpireYear, To.BeAtMost(99))
                .Require(m => m.Cvc);
        }

        public void UpdateFrom(PaymentModel input)
        {
            Type = input.Type;
            CardNumber = input.CardNumber;
            CardHolder = input.CardHolder;
            ExpireMonth = input.ExpireMonth;
            ExpireYear = input.ExpireYear;
            Cvc = input.Cvc;
        }
    }
}