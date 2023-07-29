using molecules.core.aggregates;
using molecules.core.valueobjects.AtomProperty;
using molecules.core.valueobjects.BasisSet;
using molecules.core.valueobjects.CalcOrderItem;
using molecules.core.valueobjects.GmsCalc;
using molecules.core.valueobjects.GmsCalc.Input;
using System.Text;

namespace molecules.core.factories
{
    public class GmsCalcInputFactory : IGmsCalcInputFactory
    {

        public List<GmsCalcInputItem> BuildCalcInput(IList<CalcOrder> orders)
        {
            List<GmsCalcInputItem> retval= new List<GmsCalcInputItem>();
            foreach(var calcOrder in orders)
            {
                foreach(var calcOrderItem in calcOrder.Items)
                { 
                    if (calcOrderItem.Details.Type == CalcOrderItemType.AllKinds)
                    {
                        retval.Add(new GmsCalcInputItem(calcOrder.Details.Name, calcOrderItem.Id, calcOrderItem.MoleculeName,
                                                         GmsCalculationKind.GeometryOptimization, BuildGeoOptGmsInput(calcOrderItem.Details)));
                    }

                    retval.Add(new GmsCalcInputItem(calcOrder.Details.Name, calcOrderItem.Id, calcOrderItem.MoleculeName,
                                                         GmsCalculationKind.FukuiNeutral, BuildFukuiNeutralInput(calcOrderItem.Details)));

                    retval.Add(new GmsCalcInputItem(calcOrder.Details.Name, calcOrderItem.Id, calcOrderItem.MoleculeName,
                                                         GmsCalculationKind.FukuiLUMO, BuildFukuiLUMOInput(calcOrderItem.Details)));

                    retval.Add(new GmsCalcInputItem(calcOrder.Details.Name, calcOrderItem.Id, calcOrderItem.MoleculeName,
                                     GmsCalculationKind.FukuiHOMO, BuildFukuiHOMOInput(calcOrderItem.Details)));


                    retval.Add(new GmsCalcInputItem(calcOrder.Details.Name, calcOrderItem.Id, calcOrderItem.MoleculeName,
                                     GmsCalculationKind.CHelpGCharge, BuildCHelpGChargeInput(calcOrderItem.Details)));

                    retval.Add(new GmsCalcInputItem(calcOrder.Details.Name, calcOrderItem.Id, calcOrderItem.MoleculeName,
                                     GmsCalculationKind.GeoDiskCharge, BuildGeoDiskChargeInput(calcOrderItem.Details)));
                }
            }
            return retval;
        }


        private string BuildFukuiLUMOInput(CalcOrderItemDetails details)
        {
            StringBuilder retval = new StringBuilder();
            var basisSet = CalcBasisSetTable.GetCalcBasisSet(details.BasisSetCode);
            retval.AppendLine($" {basisSet?.GmsInput}");
            retval.AppendLine($" $CONTRL SCFTYP=UHF MAXIT=60 MULT=2 ICHARG={details.Charge - 1} $END");
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

        private string BuildFukuiHOMOInput(CalcOrderItemDetails details)
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

        private string BuildFukuiNeutralInput(CalcOrderItemDetails details)
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

        private string BuildGeoDiskChargeInput(CalcOrderItemDetails details)
        {
            StringBuilder retval = new StringBuilder();
            var basisSet = CalcBasisSetTable.GetCalcBasisSet(details.BasisSetCode);
            retval.AppendLine($" {basisSet?.GmsInput}");
            retval.AppendLine($" $CONTRL SCFTYP=RHF DFTTYP=B3LYP MAXIT=60 MULT=1 ICHARG={details.Charge} $END");
            retval.AppendLine(" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            retval.AppendLine(" $SCF DIRSCF=.TRUE. $END");
            retval.AppendLine(" $ELPOT  IEPOT=1 WHERE=PDC $END");
            retval.AppendLine(" $PDC PTSEL=GEODESIC CONSTR=CHARGE $END");
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

        private string BuildCHelpGChargeInput(CalcOrderItemDetails details)
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

        private string BuildGeoOptGmsInput(CalcOrderItemDetails details)
        {
            StringBuilder retval = new StringBuilder();
            var basisSet = CalcBasisSetTable.GetCalcBasisSet(details.BasisSetCode);
            retval.AppendLine($" {basisSet?.GmsInput}");
            retval.AppendLine($" $CONTRL SCFTYP=RHF RUNTYP=OPTIMIZE DFTTYP=B3LYP MAXIT=60 MULT=1 ICHARG={details.Charge} $END ");
            retval.AppendLine(" $SYSTEM MEMDDI=1000 MWORDS=30 $END");
            retval.AppendLine(" $STATPT NSTEP=999 $END");
            retval.AppendLine($" $SCF DIRSCF=.TRUE. $END");
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

    }
}
