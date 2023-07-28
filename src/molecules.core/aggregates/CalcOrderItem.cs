using molecules.core.valueobjects.CalcOrderItem;

namespace molecules.core.aggregates
{
    public class CalcOrderItem
    {
        public int Id { get; set; }

        public string MoleculeName { get; set; }

        public CalcOrderItemDetails Details { get; set; }

        public CalcOrderItem()
        {
            Id = 0;
            MoleculeName = string.Empty;
            Details = new CalcOrderItemDetails();
        }

        public CalcOrderItem(string moleculeName):this()
        {
            if ( string.IsNullOrWhiteSpace(moleculeName))
            {
                throw new ArgumentException("Molecule name cannot be null or empty", nameof(moleculeName));
            }
            MoleculeName = moleculeName;
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
