namespace molecules.core.valueobjects.GmsCalc.Input
{
    public class GmsCalcInputItem
    {

        private GmsCalcInfo _info { get; }

        private int _orderItemId { get; }

        private string _orderName { get; }


        public GmsCalcInputItem(string orderName, int orderItemId, GmsCalcInfo info) {
            _orderItemId = orderItemId;
            _orderName = orderName;
            _info = info;
        }

        public string DisplayName => $"{_orderName}_{_orderItemId}_{_info.MoleculeName}_{_info.Kind}";

        public string Content => _info.Content.ToString();

    }
}
