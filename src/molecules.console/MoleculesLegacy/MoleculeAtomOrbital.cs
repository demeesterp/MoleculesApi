﻿namespace molecules.console.MoleculesLegacy
{
    public class MoleculeAtomOrbital
    {
        public int Id { get; set; }
        public int Position { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public decimal? MullikenPopulation { get; set; }
        public decimal? MullikenPopulationAcid { get; set; }
        public decimal? MullikenPopulationBase { get; set; }
        public decimal? LowdinPopulation { get; set; }
        public decimal? LowdinPopulationAcid { get; set; }
        public decimal? LowdinPopulationBase { get; set; }
    }
}
