namespace molecule.infrastructure.data.interfaces.DbEntities
{
    public class MoleculeDbEntity
    {
        public int Id { get; set; } = 0;

        public string  OrderName { get; set; } = "";

        public string BasisSet { get; set; } = "";

        public string MoleculeName { get; set; } = "";

        public string Molecule { get; set; } = "";
    }
}
