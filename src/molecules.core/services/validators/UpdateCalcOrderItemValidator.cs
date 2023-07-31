using FluentValidation;
using molecules.core.valueobjects.CalcOrderItem;

namespace molecules.core.services.validators
{
    public class UpdateCalcOrderItemValidator : AbstractValidator<UpdateCalcOrderItem>
    {
        public UpdateCalcOrderItemValidator()
        {
            RuleFor(x => x.MoleculeName).NotEmpty().WithMessage("MoleculeName is required");
            RuleFor(x => x.MoleculeName).MaximumLength(250).WithMessage("MoleculeName cannot be longer than 250 characters");
        }
    }
}
