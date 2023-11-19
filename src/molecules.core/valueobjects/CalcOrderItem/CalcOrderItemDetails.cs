using molecules.core.valueobjects.BasisSet;

namespace molecules.core.valueobjects.CalcOrderItem
{
    public class CalcOrderItemDetails(int charge, string xyz, CalcBasisSetCode calcBasisSetCode, CalcOrderItemType type): CalcDetails(charge, xyz, calcBasisSetCode)
    {
        public CalcOrderItemType Type { get; init; } = type;
    };
}
