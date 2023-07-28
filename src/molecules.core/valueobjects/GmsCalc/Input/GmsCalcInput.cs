namespace molecules.core.valueobjects.GmsCalc.Input
{
    public class GmsCalcInput
    { 
        public List<GmsCalcInputItem> Items { get; }

        public GmsCalcInput()
        {
            Items = new List<GmsCalcInputItem>();
        }

        public void AddItem(string orderName, int orderItemId, GmsCalcInfo info)
        {
            Items.Add(new GmsCalcInputItem(orderName, orderItemId, info));
        }
    }
}
