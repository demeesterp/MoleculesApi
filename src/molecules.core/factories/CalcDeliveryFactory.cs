using molecules.core.aggregates;
using molecules.core.valueobjects;
using molecules.core.valueobjects.CalcOrderItem;
using molecules.core.valueobjects.GmsCalc;
using molecules.core.valueobjects.GmsCalc.Calculations;
using molecules.core.valueobjects.GmsCalc.Input;
using System.Text;

namespace molecules.core.factories
{
    public class CalcDeliveryFactory : ICalcDeliveryFactory
    {
        private List<ICalculation> _calculations { get; }

        public CalcDeliveryFactory()
        {
            _calculations = new List<ICalculation>()
            {
                new GeoOptCalculation(),
                new CHelpGChargeCalculation(),
                new GeoDiskChargeCalculation(),
                new FukuiNeutralCalculation(),
                new FukuiHOMOCalculation(),
                new FukuiLUMOCalculation()
            };
        }

        public GmsCalcInput BuildCalcInput(IList<CalcOrder> orders)
        {
            GmsCalcInput retval = new GmsCalcInput();
            foreach(var calcOrder in orders)
            {
                foreach(var calcOrderItem in calcOrder.Items)
                {
                    foreach (var calc in _calculations.FindAll(item => 
                                                                calcOrderItem.Details.Type == CalcOrderItemType.AllKinds
                                                                    ||
                                                                item.Kind != GmsCalculationKind.GeometryOptimization))
                    {
                        retval.AddItem(calcOrder.Details.Name,
                                            calcOrderItem.Id,
                                         new GmsCalcInfo(calcOrderItem.MoleculeName,
                                                            calc.Kind,
                                                                new StringBuilder(calc.GenerateInputFile(calcOrderItem.Details))));
                    }
                }
            }
            return retval;
        }
    }
}
