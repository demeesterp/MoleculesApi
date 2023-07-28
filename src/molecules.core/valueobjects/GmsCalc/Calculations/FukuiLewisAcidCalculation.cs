using molecules.core.valueobjects.AtomProperty;
using molecules.core.valueobjects.BasisSet;
using System.Text;

namespace molecules.core.valueobjects.GmsCalc.Calculations
{
    // Molecule accepting an electron , so add one electron to the charge
    public class FukuiLewisAcidCalculation : ICalculation
    {
        public GmsCalculationKind Kind => GmsCalculationKind.FukuiLewisAcid;

        public string GenerateInputFile(CalcDetails details)
        {
            StringBuilder retval = new StringBuilder();
            var basisSet = CalcBasisSetTable.GetCalcBasisSet(details.BasisSetCode);
            retval.AppendLine($" {basisSet?.GmsInput}");
            retval.AppendLine($" $CONTRL SCFTYP=UHF MAXIT=60 MULT=2 ICHARG={details.Charge + 1} $END");
            retval.AppendLine($" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            retval.AppendLine($" $SCF DIRSCF=.TRUE. $END");
            retval.AppendLine(" $STATPT OPTTOL=0.0001 NSTEP=999 $END");
            retval.AppendLine(" $DATA");
            retval.AppendLine();
            retval.AppendLine("C1");
            foreach (var atom in details.ParseXyz())
            {
                var current = AtomPropertiesTable.GetAtomProperties(atom.symbol);
                retval.AppendLine($"{atom.symbol} {current?.AtomNumber:0.0} {atom.x} {atom.y} {atom.z}".Replace(",", "."));
            }
            retval.AppendLine(" $END");
            return retval.ToString();
        }

        public object ParseOutputFile()
        {
            throw new NotImplementedException();
        }
    }
}
