using System.Text;

namespace molecules.core.valueobjects.GmsCalc.Input
{
    public class GmsCalcInputItem
    {
        private int _orderItemId { get; }

        private string _orderName { get; }

        public string MoleculeName { get; }

        public GmsCalculationKind Kind { get; }

        public string Content { get; }

        public GmsCalcInputItem(string orderName, 
                                    int orderItemId,
                                     string moleculeName,
                                        GmsCalculationKind kind,
                                            string content) {
            _orderItemId = orderItemId;
            _orderName = orderName;
            MoleculeName = moleculeName;
            Kind = kind;
            Content = content;
        }

        public string DisplayName => $"{_orderName}_{_orderItemId}_{MoleculeName}_{Kind}";

    }
}
