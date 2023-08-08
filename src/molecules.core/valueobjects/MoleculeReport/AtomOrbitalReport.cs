namespace molecules.core.valueobjects.MoleculeReport
{
    public class AtomOrbitalReport
    {
        public string MoleculeName { get; set; } = "";
        public string AtomID { get; set; } = "";
        public int OrbitalPosition { get; set; } = 0;
        public string OrbitalSymbol { get; set; } = "";
        public decimal? PopulationFraction { get; set; }
        public decimal? PopulationFractionHOMO { get; set; }
        public decimal? PopulationFractionLUMO { get; set; }
    }
}
