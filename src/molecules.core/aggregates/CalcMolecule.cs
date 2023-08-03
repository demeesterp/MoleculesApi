using molecules.core.valueobjects.Molecules;

namespace molecules.core.aggregates
{
    public class CalcMolecule
    {
        public int Id { get; }

        public string OrderName { get; }

        public string BasisSet { get; }

        public string MoleculeName { get; }

        public Molecule? Molecule { get; set; }

        public CalcMolecule(int id, string orderName, string basisset, string moleculeName)
        {
            Id              = id;
            OrderName       = orderName;
            BasisSet        = basisset;
            MoleculeName    = moleculeName;
        }

        public CalcMolecule(string orderName,
                                  string basisSet,
                                  string moleculeName)
        {
            OrderName       = orderName;
            BasisSet        = basisSet;
            MoleculeName    = moleculeName;
        }
    }
}
