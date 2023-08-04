using Microsoft.EntityFrameworkCore;
using molecule.infrastructure.data.interfaces.DbEntities;
using molecule.infrastructure.data.interfaces.MoleculesException;
using molecule.infrastructure.data.interfaces.Repositories;

namespace molecules.infrastructure.data.Repositories
{
    public class MoleculeRepository : IMoleculeRepository
    {
        private readonly MoleculesDbContext _context;

        public MoleculeRepository(MoleculesDbContext context)
        {
            _context = context;
        }

        public async Task<MoleculeDbEntity> CreateAsync(MoleculeDbEntity entity)
        {
            await _context.Molecule.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var molecule = await _context.Molecule.FindAsync(id);
            if ( molecule != null)
            {
                _context.Molecule.Remove(molecule);
            }
            else
            {
                throw new MoleculesResourceNotFoundException($"Resource {nameof(MoleculeDbEntity)} with Id {id} was not found");
            }
        }

        public async Task<MoleculeDbEntity> GetByIdAsync(int id)
        {
            var result = await _context.Molecule.FirstOrDefaultAsync(i => i.Id == id);
            if (result != null)
            {
                return result;
            }
            else
            {
                throw new MoleculesResourceNotFoundException($"Resource {nameof(MoleculeDbEntity)} with Id {id} was not found");
            }
        }

        public async Task<MoleculeDbEntity?> FindAsync(string orderName, string basisSet, string moleculeName)
        {
            return await _context.Molecule.FirstOrDefaultAsync(i => i.OrderName == orderName 
                                                                        && i.BasisSet == basisSet 
                                                                            && i.MoleculeName == moleculeName);
        }

        public async Task<List<MoleculeNameInfoDbEntity>> FindAllByNameAsync(string moleculeName)
        {
            return await (from mol in _context.Molecule
             where  EF.Functions.Like(mol.MoleculeName.ToLower(), $"{moleculeName.ToLower()}%")
             select new MoleculeNameInfoDbEntity()
             {
                 Id = mol.Id,
                 MoleculeName = mol.MoleculeName,
                 OrderName = mol.OrderName,
                 BasisSet = mol.BasisSet
             }).AsNoTracking().ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<MoleculeDbEntity> UpdateAsync(int id, string moleculeName, string molecule)
        {
            var result = await _context.Molecule.FindAsync(id);
            if (result != null)
            {
                result.MoleculeName = moleculeName;
                result.Molecule = molecule;
                return result;
            }
            else
            {
                throw new MoleculesResourceNotFoundException($"Resource {nameof(MoleculeDbEntity)} with Id {id} was not found");
            }
        }

        
    }
}
