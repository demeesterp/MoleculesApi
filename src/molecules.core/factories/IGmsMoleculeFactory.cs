using molecules.core.valueobjects.Molecules;

namespace molecules.core.factories
{
    public interface IGmsMoleculeFactory
    {

        bool TryCompleteMolecule(string fileName, List<string> fileLines, Molecule molecule);

    }
}
