using molecule.infrastructure.data.interfaces.DbEntities;
using molecules.core.aggregates;
using molecules.core.valueobjects.Molecules;

namespace molecules.core.factories
{
    public class CalcMoleculeFactory : ICalcMoleculeFactory
    {
        public CalcMolecule BuildMolecule(MoleculeDbEntity moleculeDbEntity)
        {
            CalcMolecule retval = new CalcMolecule(moleculeDbEntity.Id,
                                    moleculeDbEntity.OrderName,
                                        moleculeDbEntity.BasisSet, 
                                            moleculeDbEntity.MoleculeName);


            retval.Molecule = Molecule.DeserializeFromJsonString(moleculeDbEntity.Molecule);
            return retval;
        }
    }
}
