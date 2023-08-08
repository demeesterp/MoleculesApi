using molecules.shared;
using System.Text;
using System.Text.Json.Serialization;

namespace molecules.core.valueobjects.MoleculeReport
{
    public class MoleculeAtomOrbitalReport
    {
        public string MoleculeName { get; set; } = ""; 

        public string AtomID { get; set; } = "";

        public decimal? MullikenPopulation { get; set; }

        public string ElectronConfiguration 
        { 
            get
            {
                StringBuilder sb = new StringBuilder();
                OrbitalReport.ForEach(orbital => sb.Append($"{orbital.OrbitalSymbol}({StringConversion.ToString(orbital.PopulationFraction,"0.00")})"));
                return sb.ToString();
            }
        }

        public string ElectronConfigurationHOMO
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                OrbitalReport.ForEach(orbital => sb.Append($"{orbital.OrbitalSymbol}({StringConversion.ToString(orbital.PopulationFractionHOMO, "0.00")})"));
                return sb.ToString();
            }
        }

        public string ElectronConfigurationLUMO
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                OrbitalReport.ForEach(orbital => sb.Append($"{orbital.OrbitalSymbol}({StringConversion.ToString(orbital.PopulationFractionLUMO, "0.00")})"));
                return sb.ToString();
            }
        }

        [JsonIgnore]
        public List<AtomOrbitalReport> OrbitalReport { get; set; } = new List<AtomOrbitalReport>();
    }
}
