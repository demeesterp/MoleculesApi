namespace molecules.core.valueobjects.CalcOrder
{
    public record CreateCalcOrder(string Name, string Description) : CalcOrderDetails(Name, Description);
}
