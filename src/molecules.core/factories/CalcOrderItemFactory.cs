using molecule.infrastructure.data.interfaces.DbEntities;
using molecules.core.aggregates;
using molecules.core.valueobjects.BasisSet;
using molecules.core.valueobjects.CalcOrderItem;

namespace molecules.core.Factories
{
    /// <summary>
    /// Factory Service for a CalcOrderItem
    /// </summary>
    public class CalcOrderItemFactory : ICalcOrderItemFactory
    {
        /// <summary>
        /// Create a CalcOrderItem from db data
        /// </summary>
        /// <param name="dbEntity">Data comming from Db</param>
        /// <returns>CalcOrderItem entity</returns>
        public CalcOrderItem CreateCalcOrderItem(CalcOrderItemDbEntity dbEntity)
        {
            return new CalcOrderItem(dbEntity.Id, dbEntity.MoleculeName,
                    new CalcOrderItemDetails(dbEntity.Charge, dbEntity.XYZ,                    
                    Enum.TryParse(dbEntity.BasissetCode, out CalcBasisSetCode calcBasisSetCode)? calcBasisSetCode : CalcBasisSetCode.BSTO3G,
                    Enum.TryParse(dbEntity.CalcType, out CalcOrderItemType calcType) ? calcType : CalcOrderItemType.GeoOpt));
        }
    }
}
