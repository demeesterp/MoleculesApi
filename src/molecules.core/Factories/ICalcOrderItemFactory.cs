using molecule.infrastructure.data.interfaces.DbEntities;
using molecules.core.aggregates;

namespace molecules.core.Factories
{
    /// <summary>
    /// Interface for Factory Service that creates a CalcOrderItem
    /// </summary>
    public interface ICalcOrderItemFactory
    {
        /// <summary>
        /// Create a CalcOrderItem from db data
        /// </summary>
        /// <param name="dbEntity">Data comming from Db</param>
        /// <returns>CalcOrderItem entity</returns>
        public CalcOrderItem CreateCalcOrderItem(CalcOrderItemDbEntity dbEntity);

    }
}
