using FluentValidation;
using molecules.core.valueobjects.CalcOrder;

namespace molecules.core.services.validators
{
    public class CreateCalcOrderValidator : AbstractValidator<CreateCalcOrder>
    {
        public CreateCalcOrderValidator()
        {
            RuleFor(item => item.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(item => item.Name).MaximumLength(250).WithMessage("Name cannot be longer than 250 characters");
        }
    }
}
