using molecules.core.valueobjects.Molecules;

namespace molecules.core.factories.GmsParsers
{
    internal class LewisLUMOPopulationAnalysisParser : UHFPopulationAnalysisParser
    {
        protected override PopulationAnalysisType GetPopulationStatus()
        {
            return PopulationAnalysisType.lewisLUMO;
        }

        internal static void GetPopulation(List<string> fileLines, Molecule molecule)
        {
            LewisLUMOPopulationAnalysisParser parser = new LewisLUMOPopulationAnalysisParser();
            parser.Parse(fileLines, molecule);
        }
    }
}
