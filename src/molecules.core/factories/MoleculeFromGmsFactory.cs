using molecules.core.factories.GmsParsers;
using molecules.core.valueobjects.GmsCalc;
using molecules.core.valueobjects.Molecules;

namespace molecules.core.factories
{
    public class MoleculeFromGmsFactory : IMoleculeFromGmsFactory
    {
        public bool TryCompleteMolecule(string fileName, List<string> fileLines, Molecule molecule)
        {
            if ( fileName.Contains(GmsCalculationKind.GeometryOptimization.ToString()) ) {
                if (GmsCalcValidityParser.TryParse(GmsCalculationKind.GeometryOptimization, fileLines, molecule))
                {
                    GeoOptParser.Parse(fileLines, molecule);
                    GeoOptDftEnergyParser.Parse(fileLines, molecule);
                }
            }
            else if ( fileName.Contains(GmsCalculationKind.GeoDiskCharge.ToString()) ) {
                if (GmsCalcValidityParser.TryParse(GmsCalculationKind.GeoDiskCharge, fileLines, molecule)){
                    ChargeParser.Parse(fileLines, molecule);
                }
            }
            else if ( fileName.Contains(GmsCalculationKind.CHelpGCharge.ToString()) ) 
            {
                if (GmsCalcValidityParser.TryParse(GmsCalculationKind.CHelpGCharge,fileLines, molecule))
                {
                    ChargeParser.Parse(fileLines, molecule);
                }
            }
            else if ( fileName.Contains(GmsCalculationKind.FukuiHOMO.ToString()) ) {
                if (GmsCalcValidityParser.TryParse(GmsCalculationKind.FukuiHOMO, fileLines, molecule))
                {
                    LewisHOMOPopulationAnalysisParser.GetPopulation(fileLines, molecule);
                    molecule.HFEnergyHOMO = FukuiEnergyLewisHOMOParser.GetEnergy(fileLines);
                }
            }
            else if ( fileName.Contains(GmsCalculationKind.FukuiLUMO.ToString()) ) {
                if ( GmsCalcValidityParser.TryParse(GmsCalculationKind.FukuiLUMO, fileLines, molecule))
                {
                    LewisLUMOPopulationAnalysisParser.GetPopulation(fileLines, molecule);
                    molecule.HFEnergyLUMO = FukuiEnergyLewisLUMOParser.GetEnergy(fileLines);
                }
            }
            else if ( fileName.Contains(GmsCalculationKind.FukuiNeutral.ToString()) ) {
                if ( GmsCalcValidityParser.TryParse(GmsCalculationKind.FukuiNeutral, fileLines, molecule))
                {
                    NeutralPopulationAnalysisParser.GetPopulation(fileLines, molecule);
                    molecule.HFEnergy = FukuiEnergyNeutralParser.GetEnergy(fileLines);
                }
            } 
            else {
                return false;
            }
            return true;
        }

    }
}
