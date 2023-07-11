using molecules.core.valueobjects.CalcOrderItem;

namespace molecules.core.services.validators.servicehelpers
{
    public interface ICalcOrderItemServiceValidations
    {
        /// <summary>
        /// Validated a CreateCalcOrderItem
        /// </summary>
        /// <param name="createCalcOrderItem">Create calc order item</param>
        void Validate(CreateCalcOrderItem createCalcOrderItem);
    }
}
