namespace molecules.core.valueobjects.CalcOrder
{
    public class UpdateCalcOrder : CalcOrderDetails
    {
        public UpdateCalcOrder(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
