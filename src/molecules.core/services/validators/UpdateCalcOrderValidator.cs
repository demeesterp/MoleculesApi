using FluentValidation;
using molecules.core.valueobjects.CalcOrder;

namespace molecules.core.services.validators
{
    public class UpdateCalcOrderValidator : AbstractValidator<UpdateCalcOrder>
    {
        public UpdateCalcOrderValidator()
        {
            RuleFor(item => item.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(item => item.Name).MaximumLength(250).WithMessage("Name cannot be longer than 250 characters");
        }
    }
}
