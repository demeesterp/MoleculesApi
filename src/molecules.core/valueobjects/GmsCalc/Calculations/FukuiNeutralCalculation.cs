using molecules.core.valueobjects.AtomProperty;
using molecules.core.valueobjects.BasisSet;
using System.Text;

namespace molecules.core.valueobjects.GmsCalc.Calculations
{
    public class FukuiNeutralCalculation : ICalculation
    {
        public GmsCalculationKind Kind => GmsCalculationKind.FukuiNeutral;

        public string GenerateInputFile(CalcDetails details)
        {
            var input = new StringBuilder();
            var basisSet = CalcBasisSetTable.GetCalcBasisSet(details.BasisSetCode);
            input.AppendLine($" {basisSet?.GmsInput}");
            input.AppendLine($" $CONTRL SCFTYP=RHF MAXIT=60 MULT=1 ICHARG={details.Charge} $END");
            input.AppendLine($" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            input.AppendLine($" $SCF DIRSCF=.TRUE. $END");
            input.AppendLine(" $DATA");
            input.AppendLine();
            input.AppendLine("C1");
            foreach (var atom in details.ParseXyz())
            {
                var current = AtomPropertiesTable.GetAtomProperties(atom.symbol);
                input.AppendLine($"{atom.symbol} {current?.AtomNumber:0.0} {atom.x} {atom.y} {atom.z}".Replace(",", "."));
            }
            input.AppendLine(" $END");
            return input.ToString();
        }

        public object ParseOutputFile()
        {
            throw new NotImplementedException();
        }
    }
}
