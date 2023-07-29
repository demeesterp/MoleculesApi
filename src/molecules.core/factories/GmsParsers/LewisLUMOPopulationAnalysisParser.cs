namespace molecules.core.factories.GmsParsers
{
    internal class LewisLUMOPopulationAnalysisParser : UHFPopulationAnalysisParser
    {
        protected override PopulationAnalysisType GetPopulationStatus()
        {
            return PopulationAnalysisType.lewisLUMO;
        }
    }
}
