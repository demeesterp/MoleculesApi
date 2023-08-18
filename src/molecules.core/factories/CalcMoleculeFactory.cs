using molecule.infrastructure.data.interfaces.DbEntities;
using molecules.core.aggregates;
using molecules.core.valueobjects.Molecules;
using molecules.shared;

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


            retval.Molecule = StringConversion.FromJsonString<Molecule>(moleculeDbEntity.Molecule);
            return retval;
        }

        public CalcMolecule BuildMolecule(MoleculeNameInfoDbEntity moleculeNameInfoDb)
        {
            return new CalcMolecule(moleculeNameInfoDb.Id,
                                    moleculeNameInfoDb.OrderName,
                                        moleculeNameInfoDb.BasisSet,
                                            moleculeNameInfoDb.MoleculeName);
        }
    }
}
