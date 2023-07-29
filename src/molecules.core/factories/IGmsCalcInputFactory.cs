using molecules.core.aggregates;
using molecules.core.valueobjects.GmsCalc.Input;

namespace molecules.core.factories
{
    public interface IGmsCalcInputFactory
    {
        List<GmsCalcInputItem> BuildCalcInput(IList<CalcOrder> orders);
    }
}
