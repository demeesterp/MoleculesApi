namespace molecules.core.valueobjects.MoleculeReport
{
    public class AtomOrbitalReport
    {

        private decimal? _populationFraction;
        private decimal? _populationFractionHOMO;
        private decimal? _populationFractionLUMO;



        public string MoleculeName { get; set; } = "";
        public string AtomID { get; set; } = "";
        public int OrbitalPosition { get; set; } = 0;
        public string OrbitalSymbol { get; set; } = "";
        public decimal? PopulationFraction 
        {
            get
            {
                return _populationFraction;
            }
            set
            {
                _populationFraction = value.HasValue ? Math.Round(value.Value, 6) : null;
            }
        }
        public decimal? PopulationFractionHOMO 
        { 
            get
            {
                return _populationFractionHOMO;
            }
            set
            {
                _populationFractionHOMO = value.HasValue ? Math.Round(value.Value, 6) : null;
            }
        }
        public decimal? PopulationFractionLUMO 
        { 
            get
            {
                return _populationFractionLUMO;
            }
            set
            {
                _populationFractionLUMO = value.HasValue ? Math.Round(value.Value, 6) : null;
            }
        }
    }
}
