using System.Text.Json.Serialization;

namespace molecules.core.valueobjects.Molecules
{
    public class Bond
    {
        public int Atom1Position { get; set; }

        public int Atom2Position { get; set; }

        public decimal? Distance { get; set; }

        public decimal? BondOrder { get; set; }
        
        public decimal? BondOrderMinus1  { get;  set; }

        public decimal? BondOrderPlus1 { get; set; }

        public decimal? OverlapPopulation { get; set; }

        public decimal? OverlapPopulationMinus1 { get; set; }

        public decimal? OverlapPopulationPlus1 { get; set; }

        [JsonIgnore]
        public decimal? BondOrderHOMO => BondOrder - BondOrderMinus1;

        [JsonIgnore]
        public decimal? BondOrderLUMO => BondOrderPlus1 - BondOrder;

        [JsonIgnore]
        public decimal? OverlapPopulationHOMO => OverlapPopulation - OverlapPopulationMinus1;

        [JsonIgnore]
        public decimal? OverlapPopulationLUMO => OverlapPopulationPlus1 - OverlapPopulation;
    }
}
