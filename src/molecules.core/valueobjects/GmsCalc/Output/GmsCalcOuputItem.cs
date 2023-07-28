using System.Text;

namespace molecules.core.valueobjects.GmsCalc.Output
{
    public class GmsCalcOuputItem
    {
        private string _orderName { get; }
        private int _orderItemId { get; }
        private GmsCalcInfo _gmsCalcInfo { get; }

        public GmsCalcOuputItem(string orderName, int orderItemId, GmsCalcInfo info) 
        {
            _orderName = orderName;
            _orderItemId = orderItemId;
            _gmsCalcInfo = info;
        }

        public GmsCalcOuputItem(string displayName, StringBuilder content)
        {
            var entries = displayName.Split("_");
            if (entries.Length != 4 
                    || !Enum.IsDefined(typeof(GmsCalculationKind), entries[3])) 
            { 
                throw new ArgumentException($"Invalid display name {displayName}"); 
            }
            _orderName = entries[0];
            _orderItemId = int.Parse(entries[1]);
            _gmsCalcInfo = new GmsCalcInfo(entries[2], 
                                            (GmsCalculationKind)Enum.Parse(typeof(GmsCalculationKind), entries[3]), 
                                                content);
        }


    }
}
