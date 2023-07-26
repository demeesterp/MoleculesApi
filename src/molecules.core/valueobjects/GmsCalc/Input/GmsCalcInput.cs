namespace molecules.core.valueobjects.GmsCalc.Input
{
    public class GmsCalcInput
    {
        public string OrderName { get; }

        public List<GmsCalcInputItem> Items { get; }

        public GmsCalcInput(string orderName)
        {
            OrderName = orderName;
            Items = new List<GmsCalcInputItem>();
        }

        public void AddItem(GmsCalcInputItem item)
        {
            Items.Add(item);
        }
    }
}
