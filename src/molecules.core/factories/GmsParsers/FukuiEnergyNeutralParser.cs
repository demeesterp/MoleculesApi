namespace molecules.core.factories.GmsParsers
{
    internal class FukuiEnergyNeutralParser : FukuiEnergyParser
    {
        #region tags


        private const string StartTag = "     PROPERTY VALUES FOR THE RHF   SELF-CONSISTENT FIELD WAVEFUNCTION";


        private const string EnergyTag = "TOTAL ENERGY";

        #endregion


        protected override string GetEnergyTag()
        {
            return StartTag;
        }

        protected override string GetStartTag()
        {
            return EnergyTag;
        }


        internal static decimal GetEnergy(List<string> fileLines)
        {
            FukuiEnergyNeutralParser parser = new FukuiEnergyNeutralParser();
            return parser.Parse(fileLines);
        }
    }
}
