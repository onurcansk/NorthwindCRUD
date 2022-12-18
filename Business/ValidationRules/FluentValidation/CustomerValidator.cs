using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.CompanyName).MinimumLength(2);
            RuleFor(c => c.ContactName).MinimumLength(2);
            RuleFor(c => c.City).MinimumLength(2);
            RuleFor(c => c.Country).MinimumLength(2);
            RuleFor(c => c.CustomerID).MinimumLength(4);
        }
    }
}
