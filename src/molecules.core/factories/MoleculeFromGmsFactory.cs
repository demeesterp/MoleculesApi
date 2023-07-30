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
                GeoOptParser.Parse(fileLines,molecule);
                GeoOptDftEnergyParser.Parse(fileLines,molecule);
            }
            else if ( fileName.Contains(GmsCalculationKind.GeoDiskCharge.ToString()) ) {
                ChargeParser.Parse(fileLines,molecule);
            }
            else if ( fileName.Contains(GmsCalculationKind.CHelpGCharge.ToString()) ) {
                ChargeParser.Parse(fileLines, molecule);
            }
            else if ( fileName.Contains(GmsCalculationKind.FukuiHOMO.ToString()) ) {
                LewisHOMOPopulationAnalysisParser.GetPopulation(fileLines, molecule);
                molecule.HFEnergyHOMO = FukuiEnergyLewisHOMOParser.GetEnergy(fileLines);
            }
            else if ( fileName.Contains(GmsCalculationKind.FukuiLUMO.ToString()) ) {
                LewisLUMOPopulationAnalysisParser.GetPopulation(fileLines, molecule);
                molecule.HFEnergyLUMO = FukuiEnergyLewisLUMOParser.GetEnergy(fileLines);
            }
            else if ( fileName.Contains(GmsCalculationKind.FukuiNeutral.ToString()) ) {
                NeutralPopulationAnalysisParser.GetPopulation(fileLines, molecule);
                molecule.HFEnergy = FukuiEnergyNeutralParser.GetEnergy(fileLines);
            } 
            else {
                return false;
            }
            return true;
        }

    }
}
