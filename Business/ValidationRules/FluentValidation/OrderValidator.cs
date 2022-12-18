using Entities.Concrete;
using FluentValidation;
using System;

namespace Business.ValidationRules.FluentValidation
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(o => o.CustomerID).MinimumLength(2);
            RuleFor(o => o.ShipCity).MinimumLength(2);
            RuleFor(o => o.ShipCountry).MinimumLength(2);
            RuleFor(o => o.OrderDate).LessThanOrEqualTo(DateTime.Now);

        }
    }
}
