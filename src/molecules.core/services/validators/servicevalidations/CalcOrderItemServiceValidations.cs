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

        private readonly IValidator<UpdateCalcOrderItem> _updateCalcOrderItemValidator;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="createValidator">Injected validator from fluent validations</param>
        /// <param name="updateValidator">Injected validator from fluent validations</param>
        /// <exception cref="ArgumentNullException">validator should not be null</exception>
        public CalcOrderItemServiceValidations(IValidator<CreateCalcOrderItem> createValidator,
                                                    IValidator<UpdateCalcOrderItem> updateValidator)
        {
            _createCalcOrderItemValidator = createValidator ?? throw new ArgumentNullException(nameof(createValidator));
            _updateCalcOrderItemValidator = updateValidator ?? throw new ArgumentNullException(nameof(updateValidator));
        }


        /// <inheritdoc/>
        public void Validate(CreateCalcOrderItem createCalcOrderItem)
        {
            _createCalcOrderItemValidator.ValidateAndThrow(createCalcOrderItem);
        }

        /// <inheritdoc/>
        public void Validate(UpdateCalcOrderItem updateCalcOrderItem)
        {
            _updateCalcOrderItemValidator.ValidateAndThrow(updateCalcOrderItem);
        }
    }
}
