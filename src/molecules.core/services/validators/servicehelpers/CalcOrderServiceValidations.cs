using FluentValidation;
using molecules.core.valueobjects.CalcOrder;

namespace molecules.core.services.validators.servicehelpers
{
    /// <summary>
    /// Helpers service to group all validations for the CalcOrderService
    /// </summary>
    public class CalcOrderServiceValidations: ICalcOrderServiceValidations
    {
        #region dependencies

        private readonly IValidator<CreateCalcOrder> _createCalcOrderValidator;

        private readonly IValidator<UpdateCalcOrder> _updateCalcOrderValidator;

        #endregion

        /// <summary>
        /// Constructor for CalcOrderServiceValidations
        /// </summary>
        /// <param name="createCalcOrderValidator">CreateCalcOrder validation logic</param>
        /// <param name="updateCalcOrderValidator">UpdateCalcOrder validation logic</param>
        public CalcOrderServiceValidations(IValidator<CreateCalcOrder> createCalcOrderValidator,
                                                IValidator<UpdateCalcOrder> updateCalcOrderValidator)
        {
            _createCalcOrderValidator = createCalcOrderValidator;
            _updateCalcOrderValidator = updateCalcOrderValidator;
        }

        /// <summary>
        /// Validate CreateCalcOrder
        /// </summary>
        /// <param name="createCalcOrder">Object to be validated</param>
        /// <exception cref="ValidationException">Throws when validation fails</exception>"
        public void Validate(CreateCalcOrder createCalcOrder)
        {
            _createCalcOrderValidator.ValidateAndThrow(createCalcOrder);
        }

        /// <summary>
        /// Validate UpdateCalcOrder
        /// </summary>
        /// <param name="updateCalcOrder">Object to be validated</param>
        /// <exception cref="ValidationException">Throws when validation fails</exception>"
        public void Validate(UpdateCalcOrder updateCalcOrder)
        {
            _updateCalcOrderValidator.ValidateAndThrow(updateCalcOrder);
        }


    }
}
