using molecules.core.valueobjects;

namespace molecules.core.aggregates
{
    public class CalcOrderItem
    {
        public int Id { get; set; }

        public string MoleculeName { get; set; }

        public CalcDetails Details { get; set; }

        public CalcOrderItem()
        {
            Id = 0;
            MoleculeName = string.Empty;
            Details = new CalcDetails();
        }

        public CalcOrderItem(string moleculeName):this()
        {
            if ( string.IsNullOrWhiteSpace(moleculeName))
            {
                throw new ArgumentException("Molecule name cannot be null or empty", nameof(moleculeName));
            }
            MoleculeName = moleculeName;
        }

        public void UpdateDetails(CalcDetails details)
        {
            if ( details == null)
            {
                throw new ArgumentNullException(nameof(details));
            }
            Details = details;
        }

    }
}
