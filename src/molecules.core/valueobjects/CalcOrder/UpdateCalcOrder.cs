namespace molecules.core.valueobjects.CalcOrder
{
    public class UpdateCalcOrder : CalcOrderDetails
    {
        public UpdateCalcOrder() : base()
        {
        }


        public UpdateCalcOrder(string name, string description): this()
        {
            Name = name;
            Description = description;
        }
    }
}
