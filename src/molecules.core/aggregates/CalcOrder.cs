using molecules.core.valueobjects.CalcOrder;

namespace molecules.core.aggregates
{
    public class CalcOrder
    {
        public int Id { get; private set; }
        public CalcOrderDetails Details { get; private set; }
        public string CustomerName { get; }
        public List<CalcOrderItem> Items { get; private set; }

        public CalcOrder(int id, string name, string description = "")
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("name should have a value", nameof(name));
            }
            Id = id;
            Details = new CalcOrderDetails(name, description);
            CustomerName = "Default";
            Items = new List<CalcOrderItem>();
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
