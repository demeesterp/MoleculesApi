namespace molecules.core.valueobjects.CalcOrder
{
    public class CreateCalcOrder : CalcOrderDetails
    {

        public CreateCalcOrder(): base()
        {

        }

        public CreateCalcOrder(string name, string description):this()
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
