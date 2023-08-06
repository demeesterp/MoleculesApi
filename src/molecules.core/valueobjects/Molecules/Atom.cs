using molecules.core.valueobjects.AtomProperty;
using System.Text.Json.Serialization;

namespace molecules.core.valueobjects.Molecules
{
    public class Atom
    {
        public int Position { get; set; }

        public int Number { get; set; }

        public string Symbol { get; set; } = string.Empty;

        public int? AtomicWeight { get; set; }

        public AtomProperties? Info { get; set; }

        public decimal? PosX { get; set; }

        public decimal? PosY { get; set; }

        public decimal? PosZ { get; set; }

        public decimal? Radius { get; set; }

        public decimal? MullikenPopulation { get; set; }

        public decimal? MullikenPopulationMinus1 { get; set; }

        public decimal? MullikenPopulationPlus1 { get; set; }

        [JsonIgnore]
        public decimal? MullikenPopulationLUMO => MullikenPopulationPlus1 - MullikenPopulation;

        [JsonIgnore]
        public decimal? MullikenPopulationHOMO => MullikenPopulation - MullikenPopulationMinus1;


        public decimal? LowdinPopulation { get; set; }

        public decimal? LowdinPopulationMinus1 { get; set; }

        public decimal? LowdinPopulationPlus1 { get; set; }

        [JsonIgnore]
        public decimal? LowdinPopulationLUMO => LowdinPopulationPlus1 - LowdinPopulation;

        [JsonIgnore]
        public decimal? LowdinPopulationHOMO => LowdinPopulation - LowdinPopulationMinus1;

        public decimal? CHelpGCharge { get; set; }

        public decimal? ConnollyCharge { get; set; }

        public decimal? GeoDiscCharge { get; set; }

        public List<AtomOrbital> Orbitals { get; set; } = new List<AtomOrbital>();

        public List<Bond> Bonds { get; set; } = new List<Bond>();
    }
}
