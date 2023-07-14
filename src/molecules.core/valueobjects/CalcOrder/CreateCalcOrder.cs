namespace molecules.core.valueobjects.CalcOrder
{
    public class CreateCalcOrder : CalcOrderDetails
    {

        public CreateCalcOrder(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
