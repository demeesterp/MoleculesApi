using molecule.infrastructure.data.interfaces.DbEntities;
using molecules.core.aggregates;
using molecules.core.valueobjects;
using molecules.core.valueobjects.BasisSet;

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
            CalcOrderItem retval = new CalcOrderItem(dbEntity.MoleculeName);
            retval.Id = dbEntity.Id;
            retval.Details.Charge = dbEntity.Charge;
            retval.Details.XYZ = dbEntity.XYZ;
            
            if (Enum.TryParse(dbEntity.CalcType, out CalcType calcType))
            {
                retval.Details.CalcType = calcType;
            }

            if (Enum.TryParse(dbEntity.BasissetCode, out CalcBasisSetCode calcBasisSetCode))
            {
                retval.Details.BasisSetCode = calcBasisSetCode;
            }

            return retval;
        }
    }
}
