using molecules.core.valueobjects.Molecules;

namespace molecules.core.factories.GmsParsers
{
    internal class LewisHOMOPopulationAnalysisParser : UHFPopulationAnalysisParser
    {
        protected override PopulationAnalysisType GetPopulationStatus()
        {
            return PopulationAnalysisType.lewisHOMO;
        }

        internal static void GetPopulation(List<string> fileLines, Molecule molecule)
        {
            LewisHOMOPopulationAnalysisParser parser = new LewisHOMOPopulationAnalysisParser();
            parser.Parse(fileLines, molecule);
        }
    }
}
