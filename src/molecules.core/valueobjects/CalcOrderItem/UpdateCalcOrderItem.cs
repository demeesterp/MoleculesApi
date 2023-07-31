namespace molecules.core.valueobjects.CalcOrderItem
{
    public class UpdateCalcOrderItem
    {
        public string MoleculeName { get; set; } = string.Empty;
        
        public CalcOrderItemDetails Details { get; set; } = new CalcOrderItemDetails();
    }
}
