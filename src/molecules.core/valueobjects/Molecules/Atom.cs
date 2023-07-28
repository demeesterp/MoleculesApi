using molecules.core.valueobjects.AtomProperty;
using System.Text.Json.Serialization;

namespace molecules.core.valueobjects.Molecules
{
    public class Atom
    {
        public int Position { get; set; }

        public int Number { get; set; }

        public string? Symbol { get; set; }

        public int? AtomicWeight { get; set; }

        public AtomProperties? Info { get; set; }

        public decimal? PosX { get; set; }

        public decimal? PosY { get; set; }

        public decimal? PosZ { get; set; }

        public decimal? Radius { get; set; }

        public decimal? MullikenPopulation { get; set; }

        public decimal? MullikenPopulationHOMO { get; set; }

        public decimal? MullikenPopulationLUMO { get; set; }

        [JsonIgnore]
        public decimal? MullikenPopulationAcid => MullikenPopulationLUMO - MullikenPopulation;

        [JsonIgnore]
        public decimal? MullikenPopulationBase => MullikenPopulation - MullikenPopulationHOMO;


        public decimal? LowdinPopulation { get; set; }

        public decimal? LowdinPopulationHOMO { get; set; }

        public decimal? LowdinPopulationLUMO { get; set; }

        [JsonIgnore]
        public decimal? LowdinPopulationAcid => LowdinPopulationLUMO - LowdinPopulation;

        [JsonIgnore]
        public decimal? LowdinPopulationBase => LowdinPopulation - LowdinPopulationHOMO;

        public decimal? CHelpGCharge { get; set; }

        public decimal? ConnollyCharge { get; set; }

        public decimal? GeoDiscCharge { get; set; }

        public List<AtomOrbital> Orbitals { get; set; } = new List<AtomOrbital>();

        public List<Bond> Bonds { get; set; } = new List<Bond>();
    }
}
