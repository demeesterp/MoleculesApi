namespace molecules.core.valueobjects.CalcOrderItem
{
    public class CreateCalcOrderItem
    {
        public string MoleculeName { get; set; } = "";

        public CalcOrderItemDetails Details { get; set; } = new CalcOrderItemDetails();
    }
}
