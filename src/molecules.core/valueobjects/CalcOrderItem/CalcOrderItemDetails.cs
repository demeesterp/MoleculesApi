using molecules.core.valueobjects.BasisSet;

namespace molecules.core.valueobjects.CalcOrderItem
{
    public class CalcOrderItemDetails : CalcDetails
    {
        public CalcOrderItemDetails(int charge, string xyz, CalcBasisSetCode calcBasisSetCode, CalcOrderItemType type) 
            : base(charge, xyz, calcBasisSetCode)
        {
            Type = type;
        }

        public CalcOrderItemType Type { get; set; }

    }
}
