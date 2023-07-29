namespace molecules.core.factories.GmsParsers
{
    internal class LewisHOMOPopulationAnalysisParser : UHFPopulationAnalysisParser
    {
        protected override PopulationAnalysisType GetPopulationStatus()
        {
            return PopulationAnalysisType.lewisHOMO;
        }
    }
}
