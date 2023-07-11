using molecule.infrastructure.data.interfaces.DbEntities;

namespace molecule.infrastructure.data.interfaces.Repositories
{
    public interface ICalcOrderItemRepository
    {
        Task SaveChangesAsync();

        Task<CalcOrderItemDbEntity> CreateAsync(CalcOrderItemDbEntity entity);

        Task DeleteAsync(int id);

    }
}
