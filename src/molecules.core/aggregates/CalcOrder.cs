namespace molecules.core.aggregates
{
    public class CalcOrder
    {
        public int Id { get; set; } = 0;
        public string Name { get; }
        public string? Description { get; private set; }
        public List<CalcOrderItem> Items { get; private set; }

        public CalcOrder(string name)
        {
            Name = name;
            Items = new List<CalcOrderItem>();
        }
    }
}
