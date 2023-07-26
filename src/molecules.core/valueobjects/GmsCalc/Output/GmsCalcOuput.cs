namespace molecules.core.valueobjects.GmsCalc.Output
{
    public class GmsCalcOuput
    {
        public string OrderName { get; }

        public List<GmsCalcOuputItem> Items { get; }

        public GmsCalcOuput(string orderName)
        {
            OrderName = orderName;
            Items = new List<GmsCalcOuputItem>();
        }

        public void AddItem(GmsCalcOuputItem item)
        {
            Items.Add(item);
        }

        public GmsCalcOuputItem? GetItem(string moleculeName, GmsCalculationKind kind)
        {
            return Items.FirstOrDefault(x => x.Details.MoleculeName == moleculeName && x.Details.Kind == kind);
        }

    }
}
