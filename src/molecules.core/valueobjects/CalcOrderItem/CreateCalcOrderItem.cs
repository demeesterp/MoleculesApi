namespace molecules.core.valueobjects.CalcOrderItem
{
    public class CreateCalcOrderItem
    {
        public string MoleculeName { get; set; } = "";

        public CalcDetails CalcDetails { get; set; } = new CalcDetails();
    }
}
