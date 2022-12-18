using Entities.Concrete;
using FluentValidation;
using System;

namespace Business.ValidationRules.FluentValidation
{
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(c => c.FirstName).MinimumLength(1);
            RuleFor(c => c.LastName).MinimumLength(1);
            RuleFor(c => c.City).MinimumLength(3);
            RuleFor(c => c.Country).MinimumLength(3);
            RuleFor(c => c.BirthDate).LessThanOrEqualTo(DateTime.Now.AddYears(-18));
        }
    }
}
