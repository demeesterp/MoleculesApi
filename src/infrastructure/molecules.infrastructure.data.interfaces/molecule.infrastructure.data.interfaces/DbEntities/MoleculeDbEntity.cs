namespace molecule.infrastructure.data.interfaces.DbEntities
{
    public class MoleculeDbEntity
    {
        public int Id { get; set; } = 0;

        public int OrderItemId { get; set; } = 0;

        public string MoleculeName { get; set; } = "";

        public string Molecule { get; set; } = "";
    }
}
