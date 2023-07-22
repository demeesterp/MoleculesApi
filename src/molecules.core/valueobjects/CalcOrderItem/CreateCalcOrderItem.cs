namespace molecules.core.valueobjects.CalcOrderItem
{
    public class CreateCalcOrderItem
    {
        public string MoleculeName { get; set; } = "";

        public CalcDetails Details { get; set; } = new CalcDetails();
    }
}
