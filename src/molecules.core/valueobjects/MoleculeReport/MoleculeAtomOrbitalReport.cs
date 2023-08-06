namespace molecules.core.valueobjects.MoleculeReport
{
    public class MoleculeAtomOrbitalReport
    {
        public string MoleculeName { get; set; } = ""; 

        public string AtomID { get; set; } = "";

        public decimal? MullikenPopulation { get; set; }

        public List<AtomOrbitalReport> OrbitalReport { get; set; } = new List<AtomOrbitalReport>();
    }
}
