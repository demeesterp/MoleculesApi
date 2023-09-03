using molecules.core.valueobjects.Molecules;

namespace molecules.core.factories
{
    public interface IMoleculeFromGmsFactory
    {

        bool TryCompleteMolecule(string fileName, List<string> fileLines, Molecule? molecule);

    }
}
