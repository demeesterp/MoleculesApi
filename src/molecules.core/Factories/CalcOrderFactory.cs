using molecule.infrastructure.data.interfaces.DbEntities;
using molecules.core.aggregates;

namespace molecules.core.Factories
{
    /// <summary>
    /// Factory service to create CalcOrder entity
    /// </summary>
    public class CalcOrderFactory : ICalcOrderFactory
    {

        #region dependencies

        private readonly ICalcOrderItemFactory _calcOrderItemFactory;

        #endregion

        /// <summary>
        /// Constrcutor
        /// </summary>
        /// <param name="calcOrderItemFactory">Factory for the items</param>
        public CalcOrderFactory(ICalcOrderItemFactory calcOrderItemFactory)
        {
            this._calcOrderItemFactory = calcOrderItemFactory;
        }

        /// <summary>
        /// Create a new CalcOrder entity from a CalcOrderDbEntity
        /// </summary>
        /// <param name="dbEntity">Data comming from DB</param>
        /// <returns>Completd calcorder</returns>
        public CalcOrder CreateCalcOrder(CalcOrderDbEntity dbEntity)
        {
            CalcOrder retval = new CalcOrder(dbEntity.Id, dbEntity.Name, dbEntity.Description);
            retval.Items.AddRange(dbEntity.CalcOrderItems.Select(_calcOrderItemFactory.CreateCalcOrderItem));
            return retval;
        }
    }
}
