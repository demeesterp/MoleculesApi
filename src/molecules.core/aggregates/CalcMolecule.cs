using molecules.core.valueobjects.Molecules;

namespace molecules.core.aggregates
{
    public class CalcMolecule
    {
        public int Id { get; }

        public int CalcOrderItemId { get; }

        public string MoleculeName { get; }

        public Molecule? Molecule { get; set; }

        public CalcMolecule(int id,
                                int calcOrderItemId,
                                    string moleculeName)
        {
            Id = id;
            CalcOrderItemId = calcOrderItemId;
            MoleculeName = moleculeName;
        }

        public CalcMolecule(int calcOrderItemId,
                                    string moleculeName)
        {
            CalcOrderItemId = calcOrderItemId;
            MoleculeName = moleculeName;
        }
    }
}
