using molecules.core.aggregates;
using molecules.core.valueobjects.Molecules;

namespace molecules.core.services
{
    public interface ICalcMoleculeService
    {
        Task<CalcMolecule> GetAsync(int id);

        Task<CalcMolecule> GetForOrderItemIdAsync(int id);

        Task<CalcMolecule> CreateAsync(CalcMolecule molecule);

        Task<CalcMolecule> UpdateAsync(int id, Molecule molecule);

        Task DeleteAsync(int id);


    }
}
