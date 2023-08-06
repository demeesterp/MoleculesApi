namespace molecules.core.valueobjects.MoleculeReport
{
    public class AtomOrbitalReport
    {
        public string MoleculeName { get; set; } = "";
        public string AtomID { get; set; } = "";
        public string OrbitalID { get; set; } = "";
        public decimal? PopulationFraction { get; set; }
        public decimal? PopulationFractionHOMO { get; set; }
        public decimal? PopulationFractionLUMO { get; set; }
    }
}
