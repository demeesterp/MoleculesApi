namespace molecules.core.valueobjects.CalcOrder
{
    public class CalcOrderDetails
    {
        public CalcOrderDetails(string name, string description)
        {
            Name = name;
            Description = description;
        }   

        public string Name { get; }

        public string Description { get;  }
    }
}
