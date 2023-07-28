using System.Text.Json.Serialization;

namespace molecules.core.valueobjects.Molecules
{
    public class Bond
    {
        public int Atom1Position { get; set; }

        public int Atom2Position { get; set; }

        public decimal? Distance { get; set; }

        public decimal? BondOrder { get; set; }

        public decimal? BondOrderHOMO  { get;  set; }

        public decimal? BondOrderLUMO { get; set; }

        public decimal? OverlapPopulation { get; set; }

        public decimal? OverlapPopulationHOMO { get; set; }

        public decimal? OverlapPopulationLUMO { get; set; }

        [JsonIgnore]
        public decimal? BondOrderBase => BondOrder - BondOrderHOMO;

        [JsonIgnore]
        public decimal? BondOrderAcid => BondOrderLUMO - BondOrder;

        [JsonIgnore]
        public decimal? OverlapPopulationBase => OverlapPopulation - OverlapPopulationHOMO;

        [JsonIgnore]
        public decimal? OverlapPopulationAcid => OverlapPopulationLUMO - OverlapPopulation;
    }
}
