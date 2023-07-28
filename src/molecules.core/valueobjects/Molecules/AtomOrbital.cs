using System.Text.Json.Serialization;

namespace molecules.core.valueobjects.Molecules
{
    public class AtomOrbital
    {
        public int Position { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public decimal? MullikenPopulation { get; set; }
        public decimal? MullikenPopulationHOMO { get; set; }
        public decimal? MullikenPopulationLUMO { get; set; }
        public decimal? LowdinPopulation { get; set; }
        public decimal? LowdinPopulationHOMO { get; set; }
        public decimal? LowdinPopulationLUMO { get; set; }

        [JsonIgnore]
        public decimal? MullikenPopulationAcid => MullikenPopulationLUMO - MullikenPopulation;
        [JsonIgnore]
        public decimal? MullikenPopulationBase => MullikenPopulation - MullikenPopulationHOMO;
        [JsonIgnore]
        public decimal? LowdinPopulationAcid => LowdinPopulationLUMO - LowdinPopulation;
        [JsonIgnore]
        public decimal? LowdinPopulationBase => LowdinPopulation - LowdinPopulationHOMO;
    }
}
