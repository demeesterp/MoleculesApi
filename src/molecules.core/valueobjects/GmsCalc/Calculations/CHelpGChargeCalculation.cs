using molecules.core.valueobjects.AtomProperty;
using molecules.core.valueobjects.BasisSet;
using System.Text;

namespace molecules.core.valueobjects.GmsCalc.Calculations
{
    public class CHelpGChargeCalculation : ICalculation
    {
        public GmsCalculationKind Kind => GmsCalculationKind.CHelpGCharge;

        public string GenerateInputFile(CalcDetails details)
        {
            StringBuilder retval = new StringBuilder();
            var basisSet = CalcBasisSetTable.GetCalcBasisSet(details.BasisSetCode);
            retval.AppendLine($" {basisSet?.GmsInput}");
            retval.AppendLine($" $CONTRL SCFTYP=RHF DFTTYP=B3LYP MAXIT=60 MULT=1 ICHARG={details.Charge} $END");
            retval.AppendLine(" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            retval.AppendLine(" $SCF DIRSCF=.TRUE. $END");
            retval.AppendLine(" $ELPOT  IEPOT=1 WHERE=PDC $END");
            retval.AppendLine(" $PDC PTSEL=CHELPG CONSTR=CHARGE $END");
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
