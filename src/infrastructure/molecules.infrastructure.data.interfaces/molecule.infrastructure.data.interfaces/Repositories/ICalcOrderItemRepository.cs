using molecule.infrastructure.data.interfaces.DbEntities;

namespace molecule.infrastructure.data.interfaces.Repositories
{
    public interface ICalcOrderItemRepository
    {
        Task SaveChangesAsync();

        Task<CalcOrderItemDbEntity> CreateAsync(CalcOrderItemDbEntity entity);


        Task<CalcOrderItemDbEntity> UpdateAsync(int id, int charge, string calcType, string basisSetCode, string xyz);

        Task DeleteAsync(int id);

    }
}
