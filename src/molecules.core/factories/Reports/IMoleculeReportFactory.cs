using molecules.core.valueobjects.MoleculeReport;
using molecules.core.valueobjects.Molecules;

namespace molecules.core.factories.Reports
{
    public interface IMoleculeReportFactory
    {

        List<MoleculeAtomsPopulationReport> GetMoleculePopulationReport(Molecule? molecule);

        List<MoleculeAtomsChargeReport> GetMoleculeAtomsChargeReport(Molecule? molecule);

        List<MoleculeBondsReport> GetMoleculeBondsReports(Molecule? molecule);

        List<MoleculeAtomOrbitalReport> GetMoleculeAtomOrbitalReport(Molecule? molecule);


    }
}
