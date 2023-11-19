namespace molecules.core.valueobjects.CalcOrder
{
    public record UpdateCalcOrder(string Name, string Description) : CalcOrderDetails(Name, Description);
}
