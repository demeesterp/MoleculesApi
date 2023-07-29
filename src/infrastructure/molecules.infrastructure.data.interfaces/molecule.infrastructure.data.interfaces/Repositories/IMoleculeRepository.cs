using molecule.infrastructure.data.interfaces.DbEntities;

namespace molecule.infrastructure.data.interfaces.Repositories
{
    public interface IMoleculeRepository
    {
        Task SaveChangesAsync();

        Task<MoleculeDbEntity> CreateAsync(MoleculeDbEntity entity);

        Task<MoleculeDbEntity> UpdateAsync(int id, string moleculeName, string molecule);

        Task DeleteAsync(int id);

        Task<MoleculeDbEntity> GetByIdAsync(int id);

        Task<MoleculeDbEntity> GetByOrdeItemIdAsync(int orderItemId);
    }
}
