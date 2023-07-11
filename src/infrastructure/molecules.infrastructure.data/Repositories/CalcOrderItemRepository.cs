using molecule.infrastructure.data.interfaces.DbEntities;
using molecule.infrastructure.data.interfaces.MoleculesException;
using molecule.infrastructure.data.interfaces.Repositories;

namespace molecules.infrastructure.data.Repositories
{
    public class CalcOrderItemRepository : ICalcOrderItemRepository
    {
        private readonly MoleculesDbContext _context;

        public CalcOrderItemRepository(MoleculesDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<CalcOrderItemDbEntity> CreateAsync(CalcOrderItemDbEntity entity)
        {
            await _context.CalcOrderItems.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var result = await _context.CalcOrderItems.FindAsync(id);
            if ( result != null)
            {
                _context.CalcOrderItems.Remove(result);
            }
            else
            {
                throw new MoleculesResourceNotFoundException($"Resource {nameof(CalcOrderItemDbEntity)} with Id {id} was not found");
            }
        }
    }
}
