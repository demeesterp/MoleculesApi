namespace molecules.core.valueobjects.GmsCalc
{
    public interface ICalculation
    {
        string GenerateInputFile(CalcDetails details);

        object ParseOutputFile();

        GmsCalculationKind Kind { get; }
    }
}
