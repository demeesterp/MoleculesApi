using molecules.core.valueobjects.GmsCalc;
using molecules.core.valueobjects.Molecules;

namespace molecules.core.factories.GmsParsers
{
    public class GmsCalcValidityParser
    {
        private const string GamessCalcNormalExecution = "EXECUTION OF GAMESS TERMINATED NORMALLY";

        private const string SCFNoConverge = "SCF IS UNCONVERGED";

        public static bool TryParse(GmsCalculationKind kind, List<string> fileLines, Molecule molecule)
        {
            if ( !fileLines.Exists(i => i.Contains(GamessCalcNormalExecution)) )
            {
                molecule.CalcValidityRemarks += $"| GAMESS calculation failed for {kind}";
                return false;
            }

            if ( fileLines.Exists(fileLines => fileLines.Contains(SCFNoConverge)) )
            {
                molecule.CalcValidityRemarks += $"| SCF is unconverged for {kind}";
                return false;
            }

            return true;
        }


    }
}
