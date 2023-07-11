using FluentValidation;
using molecules.core.valueobjects.CalcOrderItem;

namespace molecules.core.services.validators.servicehelpers
{
    /// <summary>
    /// Helper service to group all validations for the CalcOrderItemService
    /// </summary>
    public class CalcOrderItemServiceValidations : ICalcOrderItemServiceValidations
    {
        #region dependencies

        private readonly IValidator<CreateCalcOrderItem> _createCalcOrderItemValidator;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="validator">Injected validator from fluent validations</param>
        /// <exception cref="ArgumentNullException">validator should not be null</exception>
        public CalcOrderItemServiceValidations(IValidator<CreateCalcOrderItem> validator)
        {
            _createCalcOrderItemValidator = validator ?? throw new ArgumentNullException(nameof(validator));
        }


        /// <inheritdoc/>
        public void Validate(CreateCalcOrderItem calcOrderItem)
        {
            _createCalcOrderItemValidator.ValidateAndThrow(calcOrderItem);
        }
    }
}
