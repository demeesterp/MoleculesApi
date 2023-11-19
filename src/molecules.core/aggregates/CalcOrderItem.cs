using molecules.core.valueobjects.CalcOrderItem;

namespace molecules.core.aggregates
{
    public class CalcOrderItem
    {
        public int Id { get; }

        public string MoleculeName { get; }

        public CalcOrderItemDetails Details { get; set; }

        public CalcOrderItem(int id, string moleculeName, CalcOrderItemDetails details)
        {
            Id = id;
            MoleculeName = String.IsNullOrWhiteSpace(moleculeName) ?
                        throw new ArgumentException(nameof(moleculeName)) : moleculeName;
            Details = details?? throw new ArgumentException(nameof(details));
        }        

        public void UpdateDetails(CalcOrderItemDetails details)
        {
            if ( details == null)
            {
                throw new ArgumentNullException(nameof(details));
            }
            Details = details;
        }

    }
}
