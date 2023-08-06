namespace molecules.core.valueobjects.MoleculeReport
{
    public class MoleculeAtomsPopulationReport
    {
        public string MoleculeName { get; set; } = "";

        public string AtomID { get; set; } = "";

        public decimal? MullikenPopulation { get; set; }

        public decimal? MullikenPopulationHOMO { get; set; }

        public decimal? MullikenPopulationLUMO { get; set; }

        public decimal? LowdinPopulation { get; set; }

        public decimal? LowdinPopulationHOMO { get; set; }

        public decimal? LowdinPopulationLUMO { get; set; }


    }
}
