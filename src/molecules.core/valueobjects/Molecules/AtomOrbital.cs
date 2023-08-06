using System.Text.Json.Serialization;

namespace molecules.core.valueobjects.Molecules
{
    public class AtomOrbital
    {
        public int Position { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public decimal? MullikenPopulation { get; set; }
        public decimal? MullikenPopulationMinus1 { get; set; }
        public decimal? MullikenPopulationPlus1 { get; set; }
        public decimal? LowdinPopulation { get; set; }
        public decimal? LowdinPopulationMinus1 { get; set; }
        public decimal? LowdinPopulationPlus1 { get; set; }

        [JsonIgnore]
        public decimal? MullikenPopulationLumo => MullikenPopulationPlus1 - MullikenPopulation;
        [JsonIgnore]
        public decimal? MullikenPopulationHomo => MullikenPopulation - MullikenPopulationMinus1;
        [JsonIgnore]
        public decimal? LowdinPopulationLumo => LowdinPopulationPlus1 - LowdinPopulation;
        [JsonIgnore]
        public decimal? LowdinPopulationHomo => LowdinPopulation - LowdinPopulationMinus1;
    }
}
