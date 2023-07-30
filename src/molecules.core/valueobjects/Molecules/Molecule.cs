using System.Text.Json;
using System.Text;
using System.Text.Json.Serialization;
using molecules.core.valueobjects.AtomProperty;
using molecules.shared;

namespace molecules.core.valueobjects.Molecules
{
    public class Molecule
    {

        public Molecule()
        {

        }

        public Molecule(CalcDetails calcDetails)
        {
            int counter = 1;
            foreach (var atom in calcDetails.ParseXyz())
            {
                Atoms.Add(new Atom()
                {
                    Position = counter++,
                    Symbol = atom.symbol,
                    Number = AtomPropertiesTable.GetAtomProperties(atom.symbol)?.AtomNumber??0,
                    PosX = atom.x,
                    PosY = atom.y,
                    PosZ = atom.z
                });
            }
        }


        public string Name { get; set; } = string.Empty;

        public List<Bond> Bonds { get; set; } = new List<Bond>();

        public List<Atom> Atoms { get; set; } = new List<Atom>();

        public List<ElectronicPotential> ElPot { get; set; } = new List<ElectronicPotential>();

        public int? Charge { get; set; }

        public decimal? DftEnergy { get; set; }

        public decimal? HFEnergy { get; set; }

        public decimal? HFEnergyHOMO { get; set; }

        public decimal? HFEnergyLUMO { get; set; }

        [JsonIgnore]
        public decimal? IonisationEnergy => HFEnergyHOMO - HFEnergy;

        [JsonIgnore]
        public decimal? ElectronAffinitiy => HFEnergy - HFEnergyLUMO;

        [JsonIgnore]
        public decimal? ChemicalPotential => 0.5M * (IonisationEnergy + ElectronAffinitiy);

        [JsonIgnore]
        public decimal? Hardness => 0.5M * (IonisationEnergy - ElectronAffinitiy);

        public static Molecule? DeserializeFromJsonString(string jsonString)
        {
            return JsonSerializer.Deserialize<Molecule>(jsonString);
        }

        public static string SerializeToJsonString(Molecule molecule)
        {
            return JsonSerializer.Serialize(molecule);
        }

        public static string GetXyzFileData(Molecule molecule)
        {
            StringBuilder retval = new StringBuilder();
            if (molecule.Atoms.Count > 1)
            {
                retval.AppendLine($"{molecule.Atoms.Count}");
                retval.AppendLine();
                foreach (var ln in molecule.Atoms)
                {
                    retval.AppendLine($"{ln.Symbol}" +
                        $" {StringConversion.ToString(ln.PosX)}" +
                        $" {StringConversion.ToString(ln.PosY)}" +
                        $" {StringConversion.ToString(ln.PosZ)}");
                }
            }
            return retval.ToString();
        }
    }
}
