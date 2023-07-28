using System.Text;
using System.Text.Json;

namespace molecules.console.MoleculesLegacy
{

    public enum MoleculeErrorStatus
    {
        NoInfo = 0,
        Ok = 1,
        Error = 2
    }
    public class Molecule
    {
        public Molecule()
        {
            Bonds = new List<MoleculeBond>();
            Atoms = new List<MoleculeAtom>();
            ElPot = new List<MoleculeElpot>();
        }

        public int Id { get; set; }

        public string NameInfo { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Comment { get; set; } = string.Empty;

        public MoleculeErrorStatus Status { get; set; }

        public List<MoleculeBond> Bonds { get; set; }

        public List<MoleculeAtom> Atoms { get; set; }

        public List<MoleculeElpot> ElPot { get; set; }

        public int? Charge { get; set; }

        public decimal? DftEnergy { get; set; }

        public decimal? HFEnergy { get; set; }

        public decimal? ElectronAffinity { get; set; }

        public decimal? Hardness { get; set; }


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
                    retval.AppendLine($"{ln.Symbol} {ln.PosX} {ln.PosY} {ln.PosZ}");
                }
            }
            return retval.ToString();
        }

    }
}
