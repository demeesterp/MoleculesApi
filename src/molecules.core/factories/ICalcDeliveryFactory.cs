using molecules.core.aggregates;
using molecules.core.valueobjects.GmsCalc.Input;

namespace molecules.core.factories
{
    public interface ICalcDeliveryFactory
    {
        GmsCalcInput BuildCalcInput(IList<CalcOrder> orders);
    }
}
