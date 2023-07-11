using Microsoft.EntityFrameworkCore;
using molecule.infrastructure.data.interfaces.DbEntities;
using molecule.infrastructure.data.interfaces.MoleculesException;
using molecule.infrastructure.data.interfaces.Repositories;

namespace molecules.infrastructure.data.Repositories
{
    public class CalcOrderRepository : ICalcOrderRepository
    {

        private readonly MoleculesDbContext _context;

        public CalcOrderRepository(MoleculesDbContext context)
        {
            _context = context;
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<CalcOrderDbEntity> CreateAsync(CalcOrderDbEntity entity)
        {
            await _context.CalcOrders.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
           var result = await _context.CalcOrders.FindAsync(id);
           if ( result != null)
           {
                _context.CalcOrders.Remove(result);
           }
            else
            {
                throw new MoleculesResourceNotFoundException($"Resource {nameof(CalcOrderDbEntity)} with Id {id} was not found");
            }
        }

        public async Task<CalcOrderDbEntity> UpdateAsync(int id, string name, string description)
        {
            var result = await _context.CalcOrders.FindAsync(id);
            if (result != null)
            {
                result.Name =name;
                result.Description = description;
                return result;
            } else
            {
                throw new MoleculesResourceNotFoundException($"Resource {nameof(CalcOrderDbEntity)} with Id {id} was not found");
            }
        }

        public async Task<List<CalcOrderDbEntity>> GetAllAsync()
        {
            return await (from i in _context.CalcOrders select i).ToListAsync();
        }

        public async Task<CalcOrderDbEntity> GetByIdAsync(int id)
        {
            var result = await _context.CalcOrders.Include(o => o.CalcOrderItems).FirstOrDefaultAsync(i => i.Id == id);
            if ( result != null)
            {
                return result;
            }
            else
            {
                throw new MoleculesResourceNotFoundException($"Resource {nameof(CalcOrderDbEntity)} with Id {id} was not found");
            }
        }

        public async Task<List<CalcOrderDbEntity>> GetByNameAsync(string name)
        {
           return await _context.CalcOrders.Include(o => o.CalcOrderItems).Where(i => i.Name == name).ToListAsync();
        }


    }
}
