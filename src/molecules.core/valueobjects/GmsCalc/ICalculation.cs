using molecules.core.valueobjects.Molecules;

namespace molecules.core.valueobjects.GmsCalc
{
    public interface ICalculation
    {
        string GenerateInputFile(CalcDetails details);

        Molecule ParseOutputFile();
    }
}
