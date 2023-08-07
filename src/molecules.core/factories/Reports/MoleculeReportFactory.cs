using molecules.core.valueobjects.MoleculeReport;
using molecules.core.valueobjects.Molecules;

namespace molecules.core.factories.Reports
{
    public class MoleculeReportFactory : IMoleculeReportFactory
    {
        public List<MoleculeAtomOrbitalReport> GetMoleculeAtomOrbitalReport(Molecule? molecule)
        {
            List<MoleculeAtomOrbitalReport> report = new List<MoleculeAtomOrbitalReport>();
            if(molecule == null) return report; 
            foreach(var atom in molecule.Atoms)
            {
                var toAdd = new MoleculeAtomOrbitalReport()
                {
                    MoleculeName = molecule.Name,
                    AtomID = $"{atom.Symbol}{atom.Position}",
                    MullikenPopulation = atom.MullikenPopulation.HasValue ? Math.Round(atom.MullikenPopulation.Value, 6) : null,
                };

                foreach (var orbital in atom.Orbitals)
                {
                    var toadd = new AtomOrbitalReport()
                    {
                        AtomID = $"{atom.Symbol}{atom.Position}",
                        MoleculeName = molecule.Name,
                        OrbitalID = $"{orbital.Symbol}{orbital.Position}",
                        PopulationFraction = 100 * orbital.MullikenPopulation / atom.MullikenPopulation,
                        PopulationFractionHOMO = 100 * orbital.MullikenPopulationHomo / atom.MullikenPopulationHOMO,
                        PopulationFractionLUMO = 100 * orbital.MullikenPopulationLumo / atom.MullikenPopulationLUMO
                    };
                    toadd.PopulationFraction = toadd.PopulationFraction.HasValue ? Math.Round(toadd.PopulationFraction.Value, 6): null;
                    toadd.PopulationFractionHOMO = toadd.PopulationFractionHOMO.HasValue ? Math.Round(toadd.PopulationFractionHOMO.Value, 6) : null;
                    toadd.PopulationFractionLUMO = toadd.PopulationFractionLUMO.HasValue ? Math.Round(toadd.PopulationFractionLUMO.Value, 6) : null;
                    toAdd.OrbitalReport.Add(toadd);
                }
                report.Add(toAdd);

            }
            return report;
        }

        public List<MoleculeAtomsChargeReport> GetMoleculeAtomsChargeReport(Molecule? molecule)
        {
            List <MoleculeAtomsChargeReport> report = new List<MoleculeAtomsChargeReport>();
            if (molecule == null) return report;
            foreach (var atom in molecule.Atoms)
            {
                report.Add(new MoleculeAtomsChargeReport()
                {
                    MoleculeName = molecule.Name,
                    AtomID = $"{atom.Symbol}{atom.Position}",
                    MullikenCharge = atom.Number - atom.MullikenPopulation,
                    CHelpGHCharge = atom.CHelpGCharge,
                    GeoDiscCharge = atom.GeoDiscCharge,
                    LowdinCharge = atom.Number - atom.LowdinPopulation,
                });
            }
            return report;
        }

        public List<MoleculeBondsReport> GetMoleculeBondsReports(Molecule? molecule)
        {
            List<MoleculeBondsReport> report = new List<MoleculeBondsReport>();
            if (molecule == null) return report;
            foreach (var bond in molecule.Bonds)
            {

                Atom? atom1 = molecule.Atoms.Find(a => a.Position == bond.Atom1Position);
                Atom? atom2 = molecule.Atoms.Find(a => a.Position == bond.Atom2Position);

                report.Add(new MoleculeBondsReport()
                {
                    MoleculeName = molecule.Name,
                    BondID = $"{atom1?.Symbol}{atom1?.Position}-{atom2?.Symbol}{atom2?.Position}",
                    Distance = bond.Distance,
                    BondOrder = bond.BondOrder,
                    OverlapPopulation = bond.OverlapPopulation,
                    OverlapPopulationHOMO = bond.OverlapPopulationHOMO,
                    OverlapPopulationLUMO = bond.OverlapPopulationLUMO
                });
            }

            return report;
        }

        public List<MoleculeAtomsPopulationReport> GetMoleculePopulationReport(Molecule? molecule)
        {
            List <MoleculeAtomsPopulationReport> report = new List<MoleculeAtomsPopulationReport>();
            if (molecule == null) return report;
            foreach (var atom in molecule.Atoms)
            {
                report.Add(new MoleculeAtomsPopulationReport()
                {
                    MoleculeName = molecule.Name,
                    AtomID = $"{atom.Symbol}{atom.Position}",
                    MullikenPopulation = atom.MullikenPopulation,
                    MullikenPopulationHOMO = atom.MullikenPopulationHOMO,
                    MullikenPopulationLUMO = atom.MullikenPopulationLUMO,
                    LowdinPopulation = atom.LowdinPopulation,
                    LowdinPopulationHOMO = atom.LowdinPopulationHOMO,
                    LowdinPopulationLUMO = atom.LowdinPopulationLUMO
                });
            }        
            return report;
        }
    }
}
