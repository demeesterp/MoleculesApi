using molecules.core.valueobjects.Delivery;

namespace molecules.core.aggregates
{
    public class CalcDeliveryItem
    {
        public int Id { get;}

        public int OrderItemId { get; }
        
        public CalcDeliveryItemDetails Details { get; }

        public CalcDeliveryItem(int id, int orderItemId, CalcDeliveryItemDetails details)
        {
            Id = id;
            OrderItemId = orderItemId;
            Details = details;

        }

    }
}
