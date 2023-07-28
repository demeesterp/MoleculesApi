namespace molecules.core.valueobjects.Molecules
{
    public class ElectronicPotential
    {
        public string? Type { get; set; }
        public decimal? PosX { get; set; }
        public decimal? PosY { get; set; }
        public decimal? PosZ { get; set; }
        public decimal? Nuclear { get; set; }
        public decimal? Electronic { get; set; }
        public decimal? Total { get; set; }
    }
}
