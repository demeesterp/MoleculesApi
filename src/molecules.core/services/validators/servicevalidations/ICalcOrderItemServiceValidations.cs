using molecules.core.valueobjects.CalcOrderItem;

namespace molecules.core.services.validators.servicehelpers
{
    public interface ICalcOrderItemServiceValidations
    {
        /// <summary>
        /// Validated a CreateCalcOrderItem
        /// </summary>
        /// <param name="createCalcOrderItem">Create calcorderitem</param>
        void Validate(CreateCalcOrderItem createCalcOrderItem);

        /// <summary>
        /// Validated a CreateCalcOrderItem
        /// </summary>
        /// <param name="updateCalcOrderItem">Update calcorderitem</param>
        void Validate(UpdateCalcOrderItem updateCalcOrderItem); 
    }
}
