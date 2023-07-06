namespace molecules.core.aggregates
{
    public class CalcOrder
    {
        public int Id { get; set; } = 0;
        public string Name { get; }
        public string Description { get; private set; }
        public List<CalcOrderItem> Items { get; private set; }

        public CalcOrder()
        {
            Name = string.Empty;
            Description = string.Empty;
            Items = new List<CalcOrderItem>();
        }

        public CalcOrder(string name, string description = ""):this()
        {
            if ( string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name should have a value", nameof(name));
            }
            Name = name; 
            Description = description;
        }

        public void AddItem(CalcOrderItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }
            Items.Add(item);
        }

        public void RemoveItem(int itemId)
        {
            var item = Items.Find(i => i.Id == itemId);
            if (item != null)
            {
                Items.Remove(item);
            }
        }
        
    }
}
