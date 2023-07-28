namespace molecules.core.valueobjects.Molecules
{
    public class Molecule
    {
        public string Name { get; set; } = string.Empty;

        public List<Bond> Bonds { get; set; } = new List<Bond>();

        public List<Atom> Atoms { get; set; } = new List<Atom>();

        public List<ElectronicPotential> ElPot { get; set; } = new List<ElectronicPotential>();

        public int? Charge { get; set; }

        public decimal? DftEnergy { get; set; }

        public decimal? HFEnergy { get; set; }

        public decimal? HFEnergyHOMO { get; set; }

        public decimal? HFEnergyLUMO { get; set; }

        public decimal? IonisationEnergy => HFEnergyHOMO - HFEnergy;

        public decimal? ElectronAffinitiy => HFEnergy - HFEnergyLUMO;

        public decimal? ChemicalPotential => 0.5M * (IonisationEnergy + ElectronAffinitiy);

        public decimal? Hardness => 0.5M * (IonisationEnergy - ElectronAffinitiy);
    }
}
