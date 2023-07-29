using molecule.infrastructure.data.interfaces.DbEntities;
using molecules.core.aggregates;

namespace molecules.core.factories
{
    public interface ICalcMoleculeFactory
    {
        CalcMolecule BuildMolecule(MoleculeDbEntity moleculeDbEntity);
    }
}
