using molecule.infrastructure.data.interfaces.DbEntities;

namespace molecule.infrastructure.data.interfaces.Repositories
{
    public interface ICalcOrderRepository
    {
        Task SaveChangesAsync();

        Task<CalcOrderDbEntity> CreateAsync(CalcOrderDbEntity entity);

        Task<CalcOrderDbEntity> UpdateAsync(int id, string name, string description);

        Task DeleteAsync(int id);

        Task<CalcOrderDbEntity> GetByIdAsync(int id);

        Task<List<CalcOrderDbEntity>> GetAllAsync();

        Task<List<CalcOrderDbEntity>> GetByNameAsync(string name);

    }
}
